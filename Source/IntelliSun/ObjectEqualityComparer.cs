using System;
using System.Collections.Generic;

namespace IntelliSun
{
    public static class ObjectEqualityComparer
    {
        public static IEqualityComparer<T> New<T>()
        {
            return new ObjectEqualityComparer<T>();
        }
    }

    public sealed class ObjectEqualityComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
        public bool Equals(T x, T y)
        {
            return Object.Equals(x, y);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}