using System;

namespace IntelliSun.Text
{
    internal class SingleTextNode : TextNode
    {
        private static readonly TextNode[] _emptyArray = new TextNode[0];

        public SingleTextNode(string content)
            : base(content)
        {
        }

        public SingleTextNode(string content, TextNodeType type)
            : base(content, type)
        {
        }

        public override TextNode[] ChildNodes
        {
            get { return _emptyArray; }
        }
    }
}