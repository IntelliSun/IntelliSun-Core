using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Collections
{
    public static class EnumerableHelper
    {
        public static TResult FetchAcross<T, TResult>(IEnumerable<T> enumeration, Func<T, TResult> selector,
            Func<TResult, bool> predicate)
        {
            return FetchAcross(new[] { enumeration }, selector, predicate);
        }

        public static TResult FetchAcross<T, TResult>(IEnumerable<IEnumerable<T>> enumerations, Func<T, TResult> selector,
            Func<TResult, bool> predicate)
        {
            foreach (var value in from enumeration in enumerations
                                  from obj in enumeration
                                  select selector(obj)
                                  into value
                                  where predicate(value)
                                  select value)
                return value;

            return default(TResult);
        }
    }
}
