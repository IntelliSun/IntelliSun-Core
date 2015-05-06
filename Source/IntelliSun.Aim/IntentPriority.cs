using System;

namespace IntelliSun.Aim
{
    public struct IntentPriority : IComparable<IntentPriority>
    {
        private readonly uint value;

        public IntentPriority(uint value)
        {
            this.value = value;
        }

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has the following
        ///     meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This
        ///     object is equal to <paramref name="other" />. Greater than zero This object is greater than
        ///     <paramref name="other" />.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IntentPriority other)
        {
            return this.value > other.value ? 1 : this.value < other.value ? -1 : 0;
        }

        public uint Value
        {
            get { return this.value; }
        }
    }
}