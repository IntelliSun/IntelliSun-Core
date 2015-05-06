using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public class LiveIndexer<TKey, TValue> : IIndexer<TKey, TValue>
    {
        private readonly Func<IDictionary<TKey, TValue>> collection;

        public LiveIndexer(Func<IDictionary<TKey, TValue>> collection)
        {
            this.collection = collection;
        }

        public TValue this[TKey key]
        {
            get { return this.collection()[key]; }
        }
    }
}