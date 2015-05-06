using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliSun.Text
{
    public abstract class StringSelector
    {
        public abstract StringSelectionResult Select(string text);

        public StringSelectionResult Select(string text, int startIndex)
        {
            return this.Select(text.Substring(startIndex));
        }
    }
}
