using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntelliSun.Text
{
    public class NodeTreeParser : ITextTreeParser
    {
        private readonly List<LinkedBuilderContext> contexts;
        private LinkedBuilderContext activeContext;

        public NodeTreeParser()
        {
            this.contexts = new List<LinkedBuilderContext>();
        }

        public TextNode Parse(string text)
        {
            if (this.Config == null)
                return new SingleTextNode(text);

            this.AddActiveContext(null);
            var session = new LocatorSession(Config.Locators);
            var collect = String.Empty;
            var collectText = String.Empty;

            foreach (var @char in text)
            {
                collect += @char;

                var result = session.Fetch(@char);
                if (result.Type == FetchResultType.Text)
                    collectText += collect;

                if (result.Type != FetchResultType.Collect && result.Type != FetchResultType.Text)
                {
                    this.activeContext.SiblingContext(new BasicNodeBuilder(collectText));
                    collectText = String.Empty;
                }

                if (result.Type == FetchResultType.Delimiter)
                {
                    var delimiter = result.Result.Delimiter;
                    delimiter.Parse(new NodeDelimiterContext(this.activeContext, collect));
                }

                if (result.Type != FetchResultType.Collect)
                    collect = String.Empty;
            }

            this.ActiveContext.SiblingContext(new BasicNodeBuilder(collectText));
            return new GroupTextNode(String.Empty, this.contexts.Select(context => context.Render()).ToArray());
        }

        internal void SetActiveContext(LinkedBuilderContext context)
        {
            SetActiveContext(context, false);
        }

        internal void SetActiveContext(LinkedBuilderContext context, bool contextless)
        {
            this.activeContext =
                context ??
                (contextless ? this.AddNewContext() : this.NewContext());
        }

        internal void AddActiveContext(LinkedBuilderContext context)
        {
            lock (contexts)
            {
                this.SetActiveContext(context);
                this.contexts.Add(activeContext);
            }
        }

        internal void DropContext(LinkedBuilderContext context)
        {
            this.contexts.Remove(context);
        }

        private LinkedBuilderContext NewContext()
        {
            return new LinkedBuilderContext(this, null);
        }

        private LinkedBuilderContext AddNewContext()
        {
            var context = this.NewContext();
            this.contexts.Add(context);

            return context;
        }

        internal LinkedBuilderContext ActiveContext
        {
            get { return this.activeContext; }
        }

        public NodeTreeParserConfig Config { get; set; }

        private class LocatorSession
        {
            private readonly LinkedTextLocator[] locators;
            private readonly Queue<LinkedTextLocator> statedLocators;

            private bool isNewSession;

            public LocatorSession(IEnumerable<LinkedTextLocator> locators)
            {
                this.locators = locators.ToArray();
                this.statedLocators = new Queue<LinkedTextLocator>();

                this.isNewSession = true;
            }

            public FetchResult Fetch(char character)
            {
                return this.isNewSession
                           ? this.FetchNew(character)
                           : this.FetchNext(character);
            }

            private FetchResult FetchNew(char character)
            {
                foreach (var linkedTextLocator in locators)
                {
                    var result = linkedTextLocator.Check(character);
                    if (result == TextLocatorResult.Match)
                        return this.CloseSession(linkedTextLocator);

                    if (result == TextLocatorResult.Valid)
                        statedLocators.Enqueue(linkedTextLocator);
                }

                return statedLocators.Count > 0
                           ? FetchResult.Collect
                           : this.Reset();
            }

            private FetchResult FetchNext(char character)
            {
                var fetchQueue = new Queue<LinkedTextLocator>(statedLocators);
                statedLocators.Clear();

                while (fetchQueue.Count > 0)
                {
                    var locator = fetchQueue.Dequeue();
                    var result = locator.Check(character);
                    if (result == TextLocatorResult.Match)
                        return this.CloseSession(locator);

                    if (result == TextLocatorResult.Valid)
                        statedLocators.Enqueue(locator);
                }

                return statedLocators.Count > 0
                           ? FetchResult.Collect
                           : this.Reset();
            }

            private FetchResult CloseSession(LinkedTextLocator textLocator)
            {
                this.Reset();
                return new FetchResult(textLocator);
            }

            private FetchResult Reset()
            {
                this.statedLocators.Clear();
                this.isNewSession = true;

                return FetchResult.Text;
            }
        }

        private class FetchResult
        {
            private readonly FetchResultType type;
            private readonly LinkedTextLocator result;

            private static readonly FetchResult _text = new FetchResult(FetchResultType.Text);
            private static readonly FetchResult _collect = new FetchResult(FetchResultType.Collect);

            public FetchResult(LinkedTextLocator result)
            {
                this.result = result;
                this.type = TypeFromLocator(result);
            }

            private FetchResult(FetchResultType type)
            {
                this.type = type;
            }

            private static FetchResultType TypeFromLocator(LinkedTextLocator locator)
            {
                switch (locator.Type)
                {
                    case LinkedTextLocator.LocatorType.Folder:
                        return FetchResultType.Folder;
                    case LinkedTextLocator.LocatorType.Delimiter:
                        return FetchResultType.Delimiter;
                    case LinkedTextLocator.LocatorType.Translator:
                        return FetchResultType.Translator;
                    default:
                        return FetchResultType.Text;
                }
            }

            public static FetchResult Text
            {
                get { return _text; }
            }

            public static FetchResult Collect
            {
                get { return _collect; }
            }

            public FetchResultType Type
            {
                get { return this.type; }
            }

            public LinkedTextLocator Result
            {
                get { return this.result; }
            }
        }

        private enum FetchResultType
        {
            Text,
            Collect,
            Folder,
            Delimiter,
            Translator
        }
    }

    internal class DynamicTextNode : TextNode
    {
        private readonly StringWriter writer;

        public DynamicTextNode()
            : base(null)
        {
            this.writer = new StringWriter();
        }

        public StringWriter Writer
        {
            get { return this.writer; }
        }

        public override string Text
        {
            get { return this.writer.ToString(); }
        }

        public override TextNode[] ChildNodes
        {
            get { return new TextNode[0];}
        }
    }

    internal class NodeSymbolContext : ISymbolContext
    {
        private readonly char symbol;

        public NodeSymbolContext(char symbol)
        {
            this.symbol = symbol;
        }

        public char Symbol
        {
            get { return this.symbol; }
        }
    }

    internal class NodeTranslatorContext : NodeSymbolContext, ITranslatorContext
    {
        private readonly NodeTreeParser parser;

        public NodeTranslatorContext(NodeTreeParser parser, char symbol)
            : base(symbol)
        {
            this.parser = parser;
        }

        public void TranslateUp()
        {
            this.parser.ActiveContext.TranslateUp();
        }

        public void TranslateDown()
        {
            this.parser.ActiveContext.TranslateDown();
        }

        public void GoTop()
        {
            this.parser.ActiveContext.GoTop();
        }

        public void GoButtom()
        {
            this.parser.ActiveContext.GoButtom();
        }

        public void TranslateIn(INodeBuilder node)
        {
            this.parser.ActiveContext.TranslateIn(node);
        }

        public void TranslateOut()
        {
            this.parser.ActiveContext.TranslateOut();
        }
    }

    internal class NodeDelimiterContext : IDelimiterContext
    {
        private readonly string elementText;
        private readonly INodeBuilderContext context;

        public NodeDelimiterContext(INodeBuilderContext context, string elementText)
        {
            this.context = context;
            this.elementText = elementText;
        }

        public string ElementText
        {
            get { return this.elementText; }
        }

        public INodeBuilderContext NodeContext
        {
            get { return this.context; }
        }
    }

    internal class LinkedBuilderContext : INodeBuilderContext, INodeBuildManager
    {
        private static int _lastId;

        private readonly INodeBuilder builder;

        private LinkedBuilderContext parent;
        private readonly NodeTreeParser parser;
        private readonly LinkedList<LinkedBuilderContext> childs;

        private InsertCommand? insertCommand;
        private LinkedListNode<LinkedBuilderContext> preSiblingPointer;

        private readonly int contextId;

        public LinkedBuilderContext(NodeTreeParser parser, INodeBuilder builder)
            : this(parser, null, builder)
        {
        }

        public LinkedBuilderContext(NodeTreeParser parser, LinkedBuilderContext parent, INodeBuilder builder)
        {
            this.parser = parser;
            this.parent = parent;
            this.builder = builder;
            this.childs = new LinkedList<LinkedBuilderContext>();

            this.insertCommand = InsertCommand.Last;

            this.contextId = _lastId++;
        }

        public INodeBuilderContext ChildContext(INodeBuilder child)
        {
            var context = new LinkedBuilderContext(parser, this, child);
            this.parser.SetActiveContext(context);

            return this.Add(context);
        }

        public INodeBuilderContext WrapContext(INodeBuilder wrapper)
        {
            this.TranslateOut();
            var preParent = this.parser.ActiveContext;
            var wrapperContext = new LinkedBuilderContext(parser, preParent, wrapper);
            preParent.Add(wrapperContext);

            return this.SwapParent(wrapperContext);
        }

        public INodeBuilderContext SiblingContext(INodeBuilder sibling)
        {
            var context = new LinkedBuilderContext(parser, this.parent, sibling);
            if (this.parent != null)
                this.parent.Add(context);
            else
                this.parser.AddActiveContext(context);

            return context;
        }

        public TextNode Render()
        {
            if (this.builder == null)
                return new SingleTextNode(String.Empty);

            return this.builder.Build(this);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            var asContext = obj as LinkedBuilderContext;
            if (asContext != null)
                return asContext.contextId == this.contextId;

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.contextId;
        }

        private LinkedBuilderContext SwapParent(LinkedBuilderContext newParent)
        {
            if (this.parent == null)
                this.parser.DropContext(this);
            else
                this.parent.childs.Remove(this);

            this.parent = newParent;
            newParent.Add(this);

            return newParent;
        }

        private LinkedBuilderContext Add(LinkedBuilderContext context)
        {
            this.preSiblingPointer =
                this.insertCommand.HasValue
                    ? this.AddByCommand(this.insertCommand.Value, context)
                    : this.AddByNode(preSiblingPointer, context);

            return context;
        }

        private LinkedListNode<LinkedBuilderContext> AddByCommand(InsertCommand command, 
            LinkedBuilderContext context)
        {
            return command == InsertCommand.First ? this.childs.AddFirst(context) : this.childs.AddLast(context);
        }

        private LinkedListNode<LinkedBuilderContext> AddByNode(LinkedListNode<LinkedBuilderContext> node,
            LinkedBuilderContext context)
        {
            return this.childs.AddAfter(node, context);
        }

        public INodeBuildManager Manager
        {
            get { return this; }
        }

        public INodeTree Tree
        {
            get { throw new NotImplementedException(); }
        }

        public INodeBuilder Parent
        {
            get { return this.parent == null ? null : this.parent.builder; }
        }

        public IEnumerable<INodeBuilder> ChildNodes
        {
            get { return this.childs.Select(context => context.builder); }
        }

        public void TranslateUp()
        {
            this.insertCommand = null;
            this.preSiblingPointer = preSiblingPointer.Previous;
        }

        public void TranslateDown()
        {
            this.insertCommand = null;
            this.preSiblingPointer = preSiblingPointer.Next ?? preSiblingPointer;
        }

        public void GoTop()
        {
            this.insertCommand = InsertCommand.First;
        }

        public void GoButtom()
        {
            this.insertCommand = InsertCommand.Last;
        }

        public void TranslateIn(INodeBuilder node)
        {
            var child = this.ChildContext(node);
            this.parser.SetActiveContext((LinkedBuilderContext)child);
        }

        public void TranslateOut()
        {
            this.parser.SetActiveContext(parent, true);
        }
    }

    internal class ChildBuilder : INodeBuilder
    {
        public TextNode Build(INodeBuilderContext context)
        {
            var renders = context.ChildNodes
                                 .Select(n => n.Build(context)).
                                  ToArray();

            return new GroupTextNode(String.Empty, renders);
        }
    }

    internal enum InsertCommand
    {
        First,
        Last,
    }
}