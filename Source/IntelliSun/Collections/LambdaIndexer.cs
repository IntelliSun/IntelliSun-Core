using System;

namespace IntelliSun.Collections
{
    public class LambdaIndexer<TKey, TValue> : IIndexer<TKey, TValue>
    {
        private readonly Func<TKey, TValue> getter;

        public LambdaIndexer(Func<TKey, TValue> getter)
        {
            this.getter = getter;
        }

        public TValue this[TKey key]
        {
            get { return this.getter(key); }
        }
    }
}