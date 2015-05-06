using System;
using System.Collections;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     A warpper class around a <see cref="System.Collections.IEnumerable" /> instance.
    /// </summary>
    internal class CloseEnumerable : IEnumerable
    {
        private readonly IEnumerable enumerable;

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntelliSun.Collections.CloseEnumerable" /> class with a source
        ///     enumeration.
        /// </summary>
        /// <param name="enumerable">The source enumeration to wrap around.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="enumerable" /> is null.
        /// </exception>
        public CloseEnumerable(IEnumerable enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            this.enumerable = enumerable;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.enumerable.GetEnumerator();
        }
    }

    /// <summary>
    ///     A warpper class around a <see cref="T:System.Collections.Generic.IEnumerable`1" /> instance.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to enumerate. This type parameter is covariant. That is,
    ///     you can use either the type you specified or any type that is more derived.
    ///     For more information about covariance and contravariance, see Covariance
    ///     and Contravariance in Generics.
    /// </typeparam>
    internal class CloseEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:IntelliSun.Collections.CloseEnumerable`1" /> class with a source
        ///     enumeration.
        /// </summary>
        /// <param name="enumerable">The source enumeration to wrap around.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="enumerable" /> is null.
        /// </exception>
        public CloseEnumerable(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            this.enumerable = enumerable;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.enumerable.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}