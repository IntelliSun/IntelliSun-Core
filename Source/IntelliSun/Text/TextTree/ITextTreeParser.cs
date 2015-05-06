using System;

namespace IntelliSun.Text
{
    public interface ITextTreeParser
    {
        TextNode Parse(string text);
    }
}