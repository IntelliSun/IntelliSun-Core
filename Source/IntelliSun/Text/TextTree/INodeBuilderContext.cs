using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public interface INodeBuilderContext
    {
        INodeBuildManager Manager { get; }

        INodeTree Tree { get; }
        INodeBuilder Parent { get; }
        IEnumerable<INodeBuilder> ChildNodes { get; }
    }
}