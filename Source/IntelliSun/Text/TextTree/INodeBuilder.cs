using System;

namespace IntelliSun.Text
{
    public interface INodeBuilder
    {
        TextNode Build(INodeBuilderContext context);
    }
}