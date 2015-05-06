using System;
using System.Collections.Generic;

namespace IntelliSun
{
    public sealed class StringEqualityComparer : IEqualityComparer<string>
    {
        private readonly StringComparison comparison;
        public const StringComparison DefaultComparison = StringComparison.Ordinal;

        public StringEqualityComparer()
            : this(DefaultComparison)
        {
            
        }

        public StringEqualityComparer(StringComparison comparison)
        {
            this.comparison = comparison;
        }

        public bool Equals(string x, string y)
        {
            return String.Equals(x, y, comparison);
        }

        public int GetHashCode(string obj)
        {
            return this.Clean(obj).GetHashCode();
        }

        private string Clean(string text)
        {
            if (((int)this.comparison & 1) != 1)
                return text;

            return this.comparison == StringComparison.InvariantCultureIgnoreCase
                ? text.ToLowerInvariant()
                : text.ToLower();
        }
    }
}
