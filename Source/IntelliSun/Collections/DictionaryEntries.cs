using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     An implementation of <see cref="IntelliSun.Collections.IMapEntries{TKey,TValue}" /> that provide an access to the
    ///     underlaying <see cref="System.Collections.Generic.IDictionary{TKey,TValue}" />.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public class DictionaryEntries<TKey, TValue> : IMapEntries<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntelliSun.Collections.DictionaryEntries{TKey,TValue}" /> class with
        ///     an underlaying <see cref="System.Collections.Generic.IDictionary{TKey,TValue}" />.
        /// </summary>
        /// <param name="dictionary">
        ///     The underlaying <see cref="System.Collections.Generic.IDictionary{TKey,TValue}" /> whoes
        ///     elements are accessed through this instance..
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="dictionary" /> is null.
        /// </exception>
        public DictionaryEntries(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            this.dictionary = dictionary;
        }

        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        /// <exception cref="ArgumentNullException" accessor="get"><paramref name="key" /> is null.</exception>
        /// <exception cref="KeyNotFoundException" accessor="get">
        ///     The property is retrieved and <paramref name="key" /> is not
        ///     found.
        /// </exception>
        /// <exception cref="NotSupportedException" accessor="set">
        ///     The property is set and the
        ///     <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public TValue this[TKey key]
        {
            get { return this.dictionary[key]; }
            set { this.dictionary[key] = value; }
        }
    }
}