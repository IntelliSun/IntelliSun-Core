using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public class InstanceIndexer<TKey, TValue> : IIndexer<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> collection;

        public InstanceIndexer(IDictionary<TKey, TValue> collection)
        {
            this.collection = collection;
        }

        public TValue this[TKey key]
        {
            get { return this.collection.GetValueOrDefault(key); }
        }
    }
}