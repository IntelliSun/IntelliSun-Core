using System;

namespace IntelliSun.Text
{
    internal class SymbolTextLocator : ITextLocator
    {
        private readonly ISymbolLocator symbolLocator;

        public SymbolTextLocator(ISymbolLocator symbolLocator)
        {
            this.symbolLocator = symbolLocator;
        }

        public TextLocatorResult Check(char current)
        {
            return this.symbolLocator.Match(current)
                       ? TextLocatorResult.Match
                       : TextLocatorResult.Invalid;
        }
    }
}