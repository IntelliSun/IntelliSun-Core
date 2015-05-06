using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Text
{
    public sealed class TextIterator : IEnumerator<char>
    {
        private int currentIndex;
        private readonly string sourceString;

        private readonly int maxIndex;

        internal TextIterator(string sourceString)
        {
            this.currentIndex = 0;
            this.sourceString = sourceString;

            var maxLength = sourceString.Length;
            this.maxIndex = maxLength - 1;
        }

        public char Current
        {
            get { return this[currentIndex]; }
        }

        public bool MoveNext()
        {
            if (!this.HasNext())
                return false;

            this.currentIndex++;
            return true;
        }

        public bool HasNext()
        {
            return (this.currentIndex < maxIndex);
        }

        public bool IsNext(char value)
        {
            if (!this.HasNext())
                return false;

            return this.GetNext().Equals(value);
        }

        public bool IsPrevious(char value)
        {
            if (this.IsFirst())
                return false;

            return this.GetPrevious().Equals(value);
        }

        public bool IsFirst()
        {
            return (this.currentIndex == 0);
        }

        public char? GetNext()
        {
            if (!this.HasNext())
                return null;

            return this.GetNext(1);
        }

        public char GetNext(int count)
        {
            var index = this.currentIndex + count;

            if (index > maxIndex)
                throw new ArgumentOutOfRangeException("count");

            return this[index];
        }

        public char? GetPrevious()
        {
            if (this.IsFirst())
                return null;

            return this.GetPrevious(1);
        }

        public char GetPrevious(int count)
        {
            var index = this.currentIndex - count;

            if (index < 0)
                throw new ArgumentOutOfRangeException("count");

            return this[index];
        }

        public char Get(int index)
        {
            if (index > maxIndex)
                throw new ArgumentOutOfRangeException("index");

            return this.sourceString[index];
        }

        public string Select(int shift, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            var index = Math.Min(Math.Max(0, currentIndex + shift), maxIndex);

            if (count + index > maxIndex)
                throw new ArgumentOutOfRangeException("count");

            return this.sourceString.Substring(index, count);
        }

        public void Reset()
        {
            this.currentIndex = 0;
        }

        public void Dispose()
        {

        }

        public IEnumerable<char> GetEnumerable()
        {
            return this.sourceString.AsEnumerable();
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public string SourceString
        {
            get { return this.sourceString; }
        }

        public char this[int index]
        {
            get { return this.Get(index); }
        }
    }
}
