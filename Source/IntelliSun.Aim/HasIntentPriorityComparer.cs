using System;
using System.Collections.Generic;
using IntelliSun.Reflection;

namespace IntelliSun.Aim
{
    internal class HasIntentPriorityComparer : IComparer<IHasIntentPriority>
    {
        /// <summary>
        ///     Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <returns>
        ///     A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in
        ///     the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero
        ///     <paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than
        ///     <paramref name="y" />.
        /// </returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public int Compare(IHasIntentPriority x, IHasIntentPriority y)
        {
            var isXSystemIntegral = IsSystemIntegral(x);
            var isYSystemIntegral = IsSystemIntegral(y);

            if (!(isXSystemIntegral || isYSystemIntegral))
                return x.Priority.CompareTo(y.Priority);

            if (isXSystemIntegral && isYSystemIntegral)
                return 0;

            return isXSystemIntegral ? 1 : 0;
        }

        private static bool IsSystemIntegral(IHasIntentPriority obj)
        {
            return obj.GetType().IsFlagged<SystemIntegralPriorityAttribute>();
        }
    }
}