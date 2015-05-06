using System;

namespace IntelliSun.Reflection
{
    public struct RuntimePropertySetter
    {
        private readonly string property;
        private readonly object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public RuntimePropertySetter(string property, object value)
        {
            this.property = property;
            this.value = value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return this.property.GetHashCode() ^ this.value.GetHashCode();
        }

        public string Property
        {
            get { return this.property; }
        }

        public object Value
        {
            get { return this.value; }
        }
    }
}