using System;

namespace IntelliSun.Text
{
    public class FolderTextNode : TextNode
    {
        private readonly TextNode[] childNodes;

        public FolderTextNode(string content, params TextNode[] childNodes)
            : this(content, TextNodeTypes.Folder, childNodes)
        {
        }

        public FolderTextNode(string content, TextNodeType type, params TextNode[] childNodes)
            : base(content, type)
        {
            this.childNodes = childNodes;
        }

        public override TextNode[] ChildNodes
        {
            get { return this.childNodes; }
        }
    }
}