using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Collections
{
    public static class EnumerableUtils
    {
        public static IEnumerable<T> ValueOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }
    }
}
