using System;

namespace IntelliSun.Text
{
    public class TokenFormatter : IFormatter
    {
        private readonly string format;

        public TokenFormatter(string format)
        {
            this.format = format;
        }

        public string Format(object obj)
        {
            return String.Format(this.format, obj);
        }
    }
}
