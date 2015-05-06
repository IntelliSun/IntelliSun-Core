using System;

namespace IntelliSun.Collections
{
    public interface IReadonlyMapEntries<in TKey, out TValue>
    {
        TValue this[TKey key] { get; }
    }
}