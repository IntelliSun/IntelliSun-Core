using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public class CharsRange : ICharsInserter
    {
        private readonly Random random;

        public CharsRange(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.random = new Random();
        }

        public CharsRange(char min, char max)
            : this((int)min, max)
        {
            
        }

        public CharsRange(char min, int length)
            : this((int)min, min + length)
        {
            
        }

        public IEnumerable<char> GetChars()
        {
            var val = this.random.Next(this.MinValue, this.MaxValue);
            yield return (char)val;
        }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public char MinChar
        {
            get { return (char)this.MinValue; }
        }

        public char MaxChar
        {
            get { return (char)this.MaxValue; }
        }
    }
}