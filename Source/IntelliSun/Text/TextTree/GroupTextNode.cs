using System;

namespace IntelliSun.Text
{
    public class GroupTextNode : FolderTextNode
    {
        public GroupTextNode(string content, params TextNode[] childNodes)
            : this(content, TextNodeTypes.Group, childNodes)
        {
        }

        public GroupTextNode(string content, TextNodeType type, params TextNode[] childNodes)
            : base(content, type, childNodes)
        {
        }
    }
}