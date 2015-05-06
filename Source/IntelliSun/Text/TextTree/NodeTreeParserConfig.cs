using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public sealed class NodeTreeParserConfig
    {
        private readonly List<IFolder> folders;
        private readonly List<IDelimiter> delimiters;
        private readonly List<ITranslator> translators;

        private readonly List<LinkedTextLocator> locators; 

        public NodeTreeParserConfig()
        {
            this.folders = new List<IFolder>();
            this.delimiters = new List<IDelimiter>();
            this.translators = new List<ITranslator>();

            this.locators = new List<LinkedTextLocator>();
        }

        public NodeTreeParserConfig With(IFolder folder)
        {
            this.folders.Add(folder);
            this.locators.Add(new LinkedTextLocator(folder));

            return this;
        }

        public NodeTreeParserConfig With(IDelimiter delimiter)
        {
            this.delimiters.Add(delimiter);
            this.locators.Add(new LinkedTextLocator(delimiter));

            return this;
        }

        public NodeTreeParserConfig With(ITranslator translator)
        {
            this.translators.Add(translator);
            this.locators.Add(new LinkedTextLocator(translator));

            return this;
        }

        public IFolder[] Folders
        {
            get { return this.folders.ToArray(); }
        }

        public IDelimiter[] Delimiters
        {
            get { return this.delimiters.ToArray(); }
        }

        public ITranslator[] Translators
        {
            get { return this.translators.ToArray(); }
        }

        internal List<LinkedTextLocator> Locators
        {
            get { return this.locators; }
        }
    }

    internal class LinkedTextLocator : ITextLocator
    {
        private readonly IFolder folder;
        private readonly IDelimiter delimiter;
        private readonly ITranslator translator;
        private readonly LocatorType type;

        private readonly ITextLocator locator;

        public LinkedTextLocator(IFolder folder)
            : this((ISymbolLocator)folder)
        {
            if (folder == null)
                throw new ArgumentNullException("folder");

            this.folder = folder;
            this.type = LocatorType.Folder;
        }

        public LinkedTextLocator(IDelimiter delimiter)
            : this((ITextLocator)delimiter)
        {
            if (delimiter == null)
                throw new ArgumentNullException("delimiter");

            this.delimiter = delimiter;
            this.type = LocatorType.Delimiter;
        }

        public LinkedTextLocator(ITranslator translator)
            : this((ISymbolLocator)translator)
        {
            if (translator == null)
                throw new ArgumentNullException("translator");

            this.translator = translator;
            this.type = LocatorType.Translator;
        }

        private LinkedTextLocator(ISymbolLocator locator)
            : this(new SymbolTextLocator(locator))
        {
            
        }

        private LinkedTextLocator(ITextLocator locator)
        {
            this.locator = locator;
        }

        public TextLocatorResult Check(char current)
        {
            return this.locator.Check(current);
        }

        public IFolder Folder
        {
            get { return this.folder; }
        }

        public IDelimiter Delimiter
        {
            get { return this.delimiter; }
        }

        public ITranslator Translator
        {
            get { return this.translator; }
        }

        public LocatorType Type
        {
            get { return this.type; }
        }

        public enum LocatorType
        {
            Folder,
            Delimiter,
            Translator
        }
    }
}