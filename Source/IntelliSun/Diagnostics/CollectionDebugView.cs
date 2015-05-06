using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IntelliSun.Diagnostics
{
    public class CollectionDebugView<T>
    {
        private readonly ICollection<T> collection;

        public CollectionDebugView(ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var array = new T[this.collection.Count];
                this.collection.CopyTo(array, 0);

                return array;
            }
        }
    }
}
