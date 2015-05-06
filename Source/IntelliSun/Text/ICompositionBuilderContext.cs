using System;

namespace IntelliSun.Text
{
    public interface ICompositionBuilderContext
    {
        ICompositionBuilderContext WithComposition(string name, string value);
        ICompositionBuilderContext WithParts(string key, params string[] parts);
    }
}