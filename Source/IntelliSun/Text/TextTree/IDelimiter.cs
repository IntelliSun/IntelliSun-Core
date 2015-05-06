using System;

namespace IntelliSun.Text
{
    public interface IDelimiter : ITextLocator
    {
        void Parse(IDelimiterContext context);
    }
}