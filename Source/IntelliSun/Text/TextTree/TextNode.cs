using System;

namespace IntelliSun.Text
{
    public abstract class TextNode
    {
        private readonly string content;
        private readonly TextNodeType type;

        protected TextNode(string content)
            : this(content, TextNodeTypes.Default)
        {
            
        }

        protected TextNode(string content, TextNodeType type)
        {
            this.content = content;
            this.type = type;
        }

        public virtual string Text
        {
            get { return this.content; }
        }

        public virtual TextNodeType Type
        {
            get { return this.type; }
        }

        public abstract TextNode[] ChildNodes { get; }
    }
}