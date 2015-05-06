using System;
using System.Collections;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    public class VirtualEnumerable<T> : IEnumerable<T>
    {
        private readonly Func<IEnumerator<T>> enumeratorProvider;

        public VirtualEnumerable(Func<IEnumerator<T>> enumeratorProvider)
        {
            this.enumeratorProvider = enumeratorProvider;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.enumeratorProvider();
        }
    }
}
