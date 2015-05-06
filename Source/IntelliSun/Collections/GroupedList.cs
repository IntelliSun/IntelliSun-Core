using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace IntelliSun.Collections
{
    [Serializable]
    public class GroupedList<TKey, TValue> : Dictionary<TKey, IList<TValue>>
    {
        public GroupedList()
        {
        }

        public GroupedList(int capacity)
            : base(capacity)
        {
        }

        public GroupedList(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public GroupedList(int capacity, IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }

        public GroupedList(IDictionary<TKey, IList<TValue>> dictionary)
            : base(dictionary)
        {
        }

        public GroupedList(IDictionary<TKey, IList<TValue>> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        public GroupedList(IEnumerable<IGrouping<TKey, TValue>> grouping)
            : this(AsDictionary(grouping))
        {
        }

        public GroupedList(IEnumerable<IGrouping<TKey, TValue>> grouping, IEqualityComparer<TKey> comparer)
            : base(AsDictionary(grouping), comparer)
        {
        }

        protected GroupedList(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IList<TValue> Add(TKey key)
        {
            if (!this.ContainsKey(key))
                this.Add(key, new List<TValue>());

            return this[key];
        }

        public void Add(TKey key, TValue value)
        {
            this.Add(key).Add(value);
        }

        private static IDictionary<TKey, IList<TValue>> AsDictionary(IEnumerable<IGrouping<TKey, TValue>> grouping)
        {
            var groups = grouping.Where(g => !ReferenceEquals(g.Key, null));
            return groups.ToDictionary(values => values.Key, values => values.ToList() as IList<TValue>);
        }
    }
}