using System;

namespace IntelliSun.Text
{
    public interface IDelimiterContext : ITextLocatorContext, IBuilderContext
    {
    }

    public interface IBuilderContext
    {
        INodeBuilderContext NodeContext { get; }
    }
}