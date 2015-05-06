using System;

namespace IntelliSun.Collections
{
    public interface IIndexer<in TKey, out TValue>
    {
        TValue this[TKey key] { get; }
    }

    public interface IIndexer<out TValue>
    {
        TValue this[int index] { get; }
    }
}