using System;

namespace IntelliSun.Text
{
    public interface INodeBuildManager
    {
        INodeBuilderContext ChildContext(INodeBuilder child);
        INodeBuilderContext WrapContext(INodeBuilder wrapper);
        INodeBuilderContext SiblingContext(INodeBuilder sibling);
    }
}