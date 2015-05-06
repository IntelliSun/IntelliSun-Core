using System;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     Defines a mechanism for accessing elements inside a map collection structure, with read/write access.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the map.</typeparam>
    /// <typeparam name="TValue">The type of values in the map.</typeparam>
    public interface IMapEntries<in TKey, TValue>
    {
        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        TValue this[TKey key] { get; set; }
    }
}