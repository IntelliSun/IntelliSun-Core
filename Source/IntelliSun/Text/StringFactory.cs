using System;
using System.Linq;

namespace IntelliSun.Text
{
    public static class StringFactory
    {
        public static string Generate(params ICharsInserter[] inserts)
        {
            return String.Concat(inserts.SelectMany(i => i.GetChars()));
        }
    }
}
