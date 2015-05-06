using System;
using System.Collections;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public static class Indexer
    {
        public static IIndexer<TKey, TValue> Empty<TKey, TValue>()
        {
            return new EmptyIndexer<TKey, TValue>();
        }

        public static IIndexer<TKey, TValue> New<TKey, TValue>(IDictionary<TKey, TValue> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new InstanceIndexer<TKey, TValue>(source);
        }

        private class EmptyIndexer<TKey, TValue> : IIndexer<TKey, TValue>
        {
            public TValue this[TKey key]
            {
                get { return default(TValue); }
            }
        }
    }
}