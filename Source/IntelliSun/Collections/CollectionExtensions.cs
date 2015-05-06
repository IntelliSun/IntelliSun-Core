using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     Provides extension methods for collection based types.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Creates an enumeration (<see cref="System.Collections.Generic.IEnumerable{T}" />) of key value pairs of strings
        ///     from a <see cref="System.Collections.Specialized.NameValueCollection" />.
        /// </summary>
        /// <param name="collection">
        ///     A <see cref="System.Collections.Specialized.NameValueCollection" /> to create an array from.
        /// </param>
        /// <returns>
        ///     An enumeration (<see cref="System.Collections.Generic.IEnumerable{T}" />) that contains the elements from the input
        ///     collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="collection" /> is null.
        /// </exception>
        [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional",
            Justification = "Collection elements are never being set")]
        public static IEnumerable<KeyValuePair<string, string>> ToPairs(this NameValueCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            return collection
                .OfType<string>()
                .Select(key => new KeyValuePair<string, string>(key, collection[key]));
        }

        /// <summary>
        ///     Creates a lookup (<see cref="System.Linq.ILookup{TKey,TElement}" />) of strings
        ///     from a <see cref="System.Collections.Specialized.NameValueCollection" />.
        /// </summary>
        /// <param name="collection">
        ///     A <see cref="System.Collections.Specialized.NameValueCollection" /> to create an array from.
        /// </param>
        /// <returns>
        ///     A lookup (<see cref="System.Linq.ILookup{TKey,TElement}" />) that contains the elements from the input
        ///     collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="collection" /> is null.
        /// </exception>
        public static ILookup<string, string> ToLookup(this NameValueCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            var pairs = collection.ToPairs();
            return pairs.ToLookup(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        ///     Creates a dictionary (<see cref="System.Collections.Generic.IDictionary{TKey,TValue}" />) of strings
        ///     from a <see cref="System.Collections.Specialized.NameValueCollection" />.
        /// </summary>
        /// <param name="collection">
        ///     A <see cref="System.Collections.Specialized.NameValueCollection" /> to create an array from.
        /// </param>
        /// <returns>
        ///     A dictionary (<see cref="System.Collections.Generic.IDictionary{TKey,TValue}" />) that contains the elements from
        ///     the input
        ///     collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="collection" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="collection" /> produces duplicate keys for two elements.
        /// </exception>
        public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            return collection.ToPairs().ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        ///     Adds a sequence of items to the specified <see cref="System.Collections.Generic.ICollection{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="collection">The source collection to append the sequence to.</param>
        /// <param name="items">
        ///     The sequence whose elements should be added to the
        ///     <see cref="System.Collections.Generic.ICollection{T}" />. The sequence itself cannot be null, but it can contain
        ///     elements that are null, if type <typeparamref name="T" /> is a reference type.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     Either <paramref name="collection" /> or <paramref name="items" /> is null.
        /// </exception>
        /// <exception cref="NotSupportedException">The <paramref name="collection" /> is read-only.</exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        ///     Returns a generic <see cref="System.Collections.Generic.IEnumerator{T}" /> that iterates through the array.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="array">The array to get its <see cref="System.Collections.Generic.IEnumerator{T}" />.</param>
        /// <returns>A generic <see cref="System.Collections.Generic.IEnumerator{T}" /> that iterates through the input array.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="array" /> is null.
        /// </exception>
        public static IEnumerator<T> GetGenericEnumerator<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            var enumerable = array.AsEnumerable();
            return enumerable.GetEnumerator();
        }

        public static T[] CloneArray<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            var result = new T[array.Length];
            array.CopyTo(result, 0);

            return result;
        }

        public static T[] CloneArray<T>(this ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            var array = new T[collection.Count];
            collection.CopyTo(array, 0);

            return array;
        }

        public static T[] CloneArray<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray();
        }
    }
}