using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace IntelliSun.Collections
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class IntelliSun_ReadonlyCollectionDebugView<T>
    {
        private readonly IReadOnlyCollection<T> collection;

        public IntelliSun_ReadonlyCollectionDebugView(IReadOnlyCollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            this.collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get { return this.collection.CloneArray(); }
        }
    }
}