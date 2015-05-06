using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    /// <summary>
    ///     A comparer that compare objects using an indexing delegate.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public class DynamicIndexComparer<T> : IComparer<T>
    {
        private readonly Func<T, int> indexSelector;

        /// <summary>
        ///     Initializes a new instance of <see cref="DynamicIndexComparer{T}" /> with a specified index selector.
        /// </summary>
        /// <param name="indexSelector">The index selector that is used to retrieve object's index.</param>
        public DynamicIndexComparer(Func<T, int> indexSelector)
        {
            this.indexSelector = indexSelector;
        }

        /// <summary>
        ///     Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other
        ///     based on their indexing.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        ///     A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in
        ///     the following table. Value Meaning Less than zero <paramref name="x" /> is less than <paramref name="y" />. Zero
        ///     <paramref name="x" /> equals <paramref name="y" />. Greater than zero <paramref name="x" /> is greater than
        ///     <paramref name="y" />.
        /// </returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        public int Compare(T x, T y)
        {
            var xValue = this.indexSelector(x);
            var yValue = this.indexSelector(y);
            var factor = this.Direction == IndexedComparison.Decreasing ? -1 : 1;

            return DynamicIndexComparerHelper.Comparer.Compare(xValue * factor, yValue * factor);
        }

        /// <summary>
        ///     The direction indicating which object is considered less or more base on its index.
        /// </summary>
        /// <returns>
        ///     A value of <see cref="IndexedComparison" /> that indicates the indexing direction for the comparison.
        /// </returns>
        /// <seealso cref="IndexedComparison" />
        public IndexedComparison Direction { get; set; }
    }

    internal static class DynamicIndexComparerHelper
    {
        private static readonly IComparer<int> _comparer;

        static DynamicIndexComparerHelper()
        {
            _comparer = Comparer<int>.Default;
        }

        public static IComparer<int> Comparer
        {
            get { return _comparer; }
        }
    }
}