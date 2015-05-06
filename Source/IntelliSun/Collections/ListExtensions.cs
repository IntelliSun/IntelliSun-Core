using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public static class ListExtensions
    {
        public static int LastIndex<T>(this IList<T> list)
        {
            if (list.Count > 0)
                return list.Count - 1;

            throw new IndexOutOfRangeException();
        }

        public static bool IsInRange<T>(this IList<T> list, int index)
        {
            return (list.Count > index);
        }

        public static bool IsOutOfRange<T>(this IList<T> list, int index)
        {
            return (list.Count <= index);
        }

        public static void Set<T>(this IList<T> list, IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }
    }
}
