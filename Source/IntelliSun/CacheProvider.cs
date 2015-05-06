using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using IntelliSun.Collections;

namespace IntelliSun
{
    public class CacheProvider<TKey>
    {
        private readonly ConcurrentDictionary<TKey, object> cache;

        public CacheProvider()
        {
            this.cache = new ConcurrentDictionary<TKey, object>();
        }

        public CacheProvider(IEqualityComparer<TKey> keyComparer)
        {
            this.cache = new ConcurrentDictionary<TKey, object>(keyComparer);
        }

        public bool IsDataCached(TKey key)
        {
            return this.cache.ContainsKey(key);
        }

        protected object GetCacheData(TKey key)
        {
            return this.IsDataCached(key) ? this.cache[key] : null;
        }

        public void CacheData(TKey key, object value)
        {
            this.cache[key] = value;
        }

        public void ClearData(TKey key)
        {
            object _;
            if (this.IsDataCached(key))
                this.cache.TryRemove(key, out _);
        }

        public void ClearCache()
        {
            this.cache.Clear();
        }

        public object this[TKey key]
        {
            get { return this.GetCacheData(key); }
            set { this.CacheData(key, value); }
        }


    }

    [Obsolete]
    public class CacheProvider<TKey, TEKey>
    {
        private readonly GroupedDictionary<TKey, TEKey, object> cache;

        public CacheProvider()
        {
            this.cache = new GroupedDictionary<TKey, TEKey, object>();
        }

        public bool IsDataCached(TKey key, TEKey entryKey)
        {
            return this.cache.ContainsKey(key) &&
                   this.cache[key].ContainsKey(entryKey);
        }

        protected object GetCacheData(TKey key, TEKey entryKey)
        {
            return this.IsDataCached(key, entryKey)
                ? this.cache[key][entryKey]
                : null;
        }

        public void CacheData(TKey key, TEKey entryKey, object value)
        {
            if (!this.cache.ContainsKey(key))
                this.cache.Add(key, new Dictionary<TEKey, object>());

            this.cache[key][entryKey] = value;
        }

        public void ClearData(TKey key)
        {
            if (this.cache.ContainsKey(key))
                this.cache.Remove(key);
        }

        public void ClearData(TKey key, TEKey entryKey)
        {
            if (this.cache.ContainsKey(key) &&
                this.cache[key].ContainsKey(entryKey))
                this.cache[key].Remove(entryKey);
        }

        public void ClearCache()
        {
            this.cache.Clear();
        }

        public IMapEntries<TEKey, object> this[TKey key]
        {
            get
            {
                return this.cache.ContainsKey(key)
                    ? new DictionaryEntries<TEKey, object>(this.cache[key])
                    : null;
            }
        }
    }
}
