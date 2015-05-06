using System;
using System.Collections.Generic;

namespace IntelliSun.Text
{
    public class CharRepeater : ICharsInserter
    {
        private readonly Random random;
        public CharRepeater(CharsRange range, int times)
        {
            this.Range = range;
            this.Times = times;

            this.random = new Random();
        }

        public IEnumerable<char> GetChars()
        {
            for (int i = 0; i < this.Times; i++)
            {
                var val = this.random.Next(this.Range.MinValue, this.Range.MaxValue);
                yield return (char)val;
            }
        }

        public int Times { get; set; }
        public CharsRange Range { get; set; }
    }
}