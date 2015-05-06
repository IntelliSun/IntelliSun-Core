using System;

namespace IntelliSun.Text
{
    public interface ITextLocator
    {
        TextLocatorResult Check(char current);
    }
}