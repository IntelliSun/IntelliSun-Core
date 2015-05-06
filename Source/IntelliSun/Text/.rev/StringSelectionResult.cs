using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliSun.Text
{
    public struct StringSelectionResult
    {
        public string Text { get; set; }

        public bool Success { get; set; }

        public StringSelectionResult(string text, bool success)
            : this()
        {
            this.Text = text;
            this.Success = success;
        }
    }
}
