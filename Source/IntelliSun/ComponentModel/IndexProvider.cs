using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.ComponentModel
{
    public partial class IndexProvider
    {
        private static readonly IndexProvider _defaultProvider;
        private static readonly Dictionary<object, IndexProvider> _providers;

        static IndexProvider()
        {
            _defaultProvider = new IndexProvider();
            _providers = new Dictionary<object, IndexProvider>();
        }

        public static int GetNewIndex()
        {
            return _defaultProvider.GetIndex();
        }

        public static int GetNewIndex(object obj)
        {
            return GetProvider(obj).GetIndex();
        }

        public static IndexProvider GetProvider()
        {
            return GetProvider(null);
        }

        public static IndexProvider GetProvider(object obj)
        {
            if (obj == null)
                return new IndexProvider();

            if (_providers.ContainsKey(obj))
                return _providers[obj];

            return AddProvider(obj);
        }

        private static IndexProvider AddProvider(object obj)
        {
            var provider = new IndexProvider();
            _providers.Add(obj, provider);
            return provider;
        }
    }

    public partial class IndexProvider
    {
        private int lastIndex;
        private readonly List<int> freedIndexes; 

        private IndexProvider()
        {
            this.lastIndex = 0;
            this.freedIndexes = new List<int>();
        }

        public int GetIndex()
        {
            if (!this.freedIndexes.Any())
                return this.lastIndex++;

            var value = this.freedIndexes.First();
            this.freedIndexes.RemoveAt(0);
            return value;
        }

        public void FreeIndex(int index)
        {
            if(index <= lastIndex)
                this.freedIndexes.Add(index);
        }

        public void Reset()
        {
            this.lastIndex = -1;
            this.freedIndexes.Clear();
        }
    }
}
