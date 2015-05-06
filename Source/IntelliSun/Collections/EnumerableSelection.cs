using System;

namespace IntelliSun.Collections
{
    public struct EnumerableSelection<T>
    {
        private readonly T value;
        private readonly bool isEmpty;

        private EnumerableSelection(bool isEmpty)
            : this()
        {
            this.isEmpty = isEmpty;
        }

        public EnumerableSelection(T value)
        {
            this.value = value;
            this.isEmpty = false;
        }

        public static EnumerableSelection<T> Empty
        {
            get { return new EnumerableSelection<T>(true); }
        }

        public static implicit operator EnumerableSelection<T>(T value)
        {
            return new EnumerableSelection<T>(value);
        }

        public T Value
        {
            get { return this.value; }
        }

        public bool IsEmpty
        {
            get { return this.isEmpty; }
        }
    }
}