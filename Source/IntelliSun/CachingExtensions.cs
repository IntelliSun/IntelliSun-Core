using System;

namespace IntelliSun
{
    public static class CachingExtensions
    {
        public static TValue GetCachedDataOrValue<TKey, TValue>(this CacheProvider<TKey> provider, TKey key,
            Func<TValue> value, bool keep = true)
        {
            if (provider.IsDataCached(key))
                return (TValue)provider[key];

            var result = value();
            if (keep)
                provider.CacheData(key, result);

            return result;
        }

        public static TValue GetCachedDataOrValue<TKey, TValue>(this CacheProvider<TKey> provider, TKey key,
            Func<TKey, TValue> value, bool keep = true)
        {
            if (provider.IsDataCached(key))
                return (TValue)provider[key];

            var result = value(key);
            if (keep)
                provider.CacheData(key, result);

            return result;
        }
    }
}
