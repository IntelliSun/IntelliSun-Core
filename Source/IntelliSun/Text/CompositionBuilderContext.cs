using System;

namespace IntelliSun.Text
{
    internal class CompositionBuilderContext : ICompositionBuilderContext
    {
        private readonly StringCompositionProvider compositionProvider;

        public CompositionBuilderContext(StringCompositionProvider compositionProvider)
        {
            this.compositionProvider = compositionProvider;
        }

        public ICompositionBuilderContext WithComposition(string name, string value)
        {
            this.compositionProvider.AddComposition(name, value);

            return this;
        }

        public ICompositionBuilderContext WithParts(string key, params string[] parts)
        {
            this.compositionProvider.AddPartsSet(key, parts);

            return this;
        }
    }
}