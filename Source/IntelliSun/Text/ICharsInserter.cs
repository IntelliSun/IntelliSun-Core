using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public interface ICharsInserter
    {
        IEnumerable<char> GetChars();
    }
}