using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntelliSun.Collections
{
    [Serializable]
    public class GroupedDictionary<TGroup, TKey, TValue> : Dictionary<TGroup, Dictionary<TKey, TValue>>
    {
        public GroupedDictionary()
        {
        }

        public GroupedDictionary(int capacity)
            : base(capacity)
        {
        }

        public GroupedDictionary(IEqualityComparer<TGroup> comparer)
            : base(comparer)
        {
        }

        public GroupedDictionary(int capacity, IEqualityComparer<TGroup> comparer)
            : base(capacity, comparer)
        {
        }

        public GroupedDictionary(IDictionary<TGroup, Dictionary<TKey, TValue>> dictionary)
            : base(dictionary)
        {
        }

        public GroupedDictionary(IDictionary<TGroup, Dictionary<TKey, TValue>> dictionary, IEqualityComparer<TGroup> comparer)
            : base(dictionary, comparer)
        {
        }

        protected GroupedDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Dictionary<TKey, TValue> Add(TGroup group)
        {
            this.Add(group, new Dictionary<TKey, TValue>());

            return this[group];
        }
    }
}