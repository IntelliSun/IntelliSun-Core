using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public interface IPairEnumerable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
    }
}
