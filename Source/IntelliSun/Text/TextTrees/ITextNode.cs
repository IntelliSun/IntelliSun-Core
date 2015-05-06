using System;

namespace IntelliSun.Text
{
    public interface ITextNode
    {
        ITextNode Parent { get; }
        ITextNode[] Children { get; }

        string GetContent();
    }

    public interface ITextLocator
    {
        
    }
}
