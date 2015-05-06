using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     Provides extension methods for <see cref="IDictionary{TKey,TValue}" />.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Returns the element with the specified key, or a default value if the key is not presented in the
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </summary>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}" /> to return the element of.</param>
        /// <param name="key">The key of the element to get.</param>
        /// <typeparam name="TKey">The type of the keys of <paramref name="dictionary" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="dictionary" />.</typeparam>
        /// <returns>
        ///     The element for the specified key in the input dictionary, or
        ///     <code>default(<typeparamref name="TValue" />)</code> if <paramref name="key" /> is not presented in the dictionary.
        /// </returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="dictionary" /> or <paramref name="key" /> is null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (key == null)
                throw new ArgumentNullException("key");

            return GetValueOrDefault(dictionary, key, default(TValue));
        }

        /// <summary>
        ///     Returns the element with the specified key, or a specified value if the key is not presented in the
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </summary>
        /// <param name="dictionary">An <see cref="IDictionary{TKey,TValue}" /> to return the element of.</param>
        /// <param name="key">The key of the element to get.</param>
        /// <param name="defaultValue">The value to return if <paramref name="key" /> is not presented in the dictionary.</param>
        /// <typeparam name="TKey">The type of the keys of <paramref name="dictionary" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="dictionary" />.</typeparam>
        /// <returns>
        ///     The element for the specified key in the input dictionary, or <paramref name="defaultValue" /> if
        ///     <paramref name="key" /> is not presented in the dictionary.
        /// </returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="dictionary" /> or <paramref name="key" /> is null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }

        public static TValue GetOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> factory)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (key == null)
                throw new ArgumentNullException("key");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, factory());

            return dictionary[key];
        }

        public static TValue GetOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TKey, TValue> factory)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (key == null)
                throw new ArgumentNullException("key");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, factory(key));

            return dictionary[key];
        }

        /// <summary>
        ///     Creates an <see cref="IDictionary{TKey,TValue}" /> from a sequence of <see cref="KeyValuePair{TKey,TValue}" />s.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     The source sequence of key-value-pairs to create the dictionary from. he sequence itself cannot be
        ///     null, but it can contain elements that are null, if type <typeparamref name="TValue" /> is a reference type.
        /// </param>
        /// <returns>
        ///     A <see cref="IDictionary{TKey,TValue}" /> that contains values of type <typeparamref name="TValue" /> selected
        ///     from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="source" /> produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return source.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" /> or <paramref name="dest" /> is null.
        /// </exception>
        public static void Transfer<TKey, TValue>(this IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> dest)
        {
            source.TransferTo(dest);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" /> or <paramref name="dest" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        public static void TransferFrom<TKey, TValue>(this IDictionary<TKey, TValue> dest,
            IDictionary<TKey, TValue> source)
        {
            source.TransferTo(dest);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" /> or <paramref name="dest" /> is null.
        /// </exception>
        public static void TransferTo<TKey, TValue>(this IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> dest)
        {
            source.TransferTo(dest, value => value);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TSValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <typeparam name="TTValue">The type of the transformed values returned from <paramref name="valueTransformer" />.</typeparam>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <param name="valueTransformer">
        ///     A selector that transforms all source elements before appending them to
        ///     <paramref name="dest" />.
        /// </param>
        /// <exception cref="Exception"><paramref name="valueTransformer" /> callback throws an exception.</exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" />, <paramref name="dest" />, or <paramref name="valueTransformer" /> is null.
        /// </exception>
        public static void Transfer<TKey, TSValue, TTValue>(this IDictionary<TKey, TSValue> source,
            IDictionary<TKey, TTValue> dest, Func<TSValue, TTValue> valueTransformer)
        {
            source.TransferTo(dest, valueTransformer);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TSValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <typeparam name="TTValue">The type of the transformed values returned from <paramref name="valueTransformer" />.</typeparam>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <param name="valueTransformer">
        ///     A selector that transforms all source elements before appending them to
        ///     <paramref name="dest" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" />, <paramref name="dest" />, or <paramref name="valueTransformer" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        /// <exception cref="Exception"><paramref name="valueTransformer" /> callback throws an exception.</exception>
        public static void TransferFrom<TKey, TSValue, TTValue>(this IDictionary<TKey, TTValue> dest,
            IDictionary<TKey, TSValue> source, Func<TSValue, TTValue> valueTransformer)
        {
            source.TransferTo(dest, valueTransformer);
        }

        /// <summary>
        ///     Copies one <see cref="IDictionary{TKey,TValue}" /> elements to another dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys of <paramref name="source" />.</typeparam>
        /// <typeparam name="TSValue">The type of the values of <paramref name="source" />.</typeparam>
        /// <typeparam name="TTValue">The type of the transformed values returned from <paramref name="valueTransformer" />.</typeparam>
        /// <param name="source">
        ///     The source <see cref="IDictionary{TKey,TValue}" /> whoes elements are copied to
        ///     <paramref name="dest" />.
        /// </param>
        /// <param name="dest">The destination <see cref="IDictionary{TKey,TValue}" /> to copy elements to.</param>
        /// <param name="valueTransformer">
        ///     A selector that transforms all source elements before appending them to
        ///     <paramref name="dest" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="source" />, <paramref name="dest" />, or <paramref name="valueTransformer" /> is null.
        /// </exception>
        /// <exception cref="Exception"><paramref name="valueTransformer" /> callback throws an exception.</exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </exception>
        /// <exception cref="NotSupportedException">The <see cref="IDictionary{TKey,TValue}" /> is read-only.</exception>
        public static void TransferTo<TKey, TSValue, TTValue>(this IDictionary<TKey, TSValue> source,
            IDictionary<TKey, TTValue> dest, Func<TSValue, TTValue> valueTransformer)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (dest == null)
                throw new ArgumentNullException("dest");

            if (valueTransformer == null)
                throw new ArgumentNullException("valueTransformer");

            foreach (var value in source)
                dest.Add(value.Key, valueTransformer(value.Value));
        }

        public static void Map<TSource, TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, IEnumerable<TSource> enumerable,
            Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            if (keySelector == null)
                throw new ArgumentNullException("keySelector");

            if (valueSelector == null)
                throw new ArgumentNullException("valueSelector");

            foreach (var value in enumerable)
                dictionary.Add(keySelector(value), valueSelector(value));
        }

        public static void Map<TKeyIn, TKeyOut, TValueIn, TValueOut>(
            this IDictionary<TKeyOut, TValueOut> dictionary, IDictionary<TKeyIn, TValueIn> source,
            Func<TKeyIn, TKeyOut> keySelector, Func<TValueIn, TValueOut> valueSelector)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (source == null)
                throw new ArgumentNullException("source");

            if (keySelector == null)
                throw new ArgumentNullException("keySelector");

            if (valueSelector == null)
                throw new ArgumentNullException("valueSelector");

            foreach (var value in source)
                dictionary.Add(keySelector(value.Key), valueSelector(value.Value));
        }
    }
}