using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public interface IStringCompositionProvider
    {
        string GetComposition(string name);
        IEnumerable<string> GetCompositionPart(string key);
    }
}