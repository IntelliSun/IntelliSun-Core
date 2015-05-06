using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public static class EnumerationBuilder
    {
        public static IEnumerable<T> Build<T>(int size, Func<int, T> selector)
        {
            for (int i = 0; i < size; i++)
                yield return selector(i);
        }

        public static IEnumerable<T> RepeatUnique<T>(Func<T> factory, int count)
        {
            return RepeatUnique(i => factory(), count);
        }

        public static IEnumerable<T> RepeatUnique<T>(Func<int, T> factory, int count)
        {
            for (var i = 0; i < count; i++)
                yield return factory(i);
        }
    }
}
