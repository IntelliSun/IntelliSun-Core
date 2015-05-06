using System;
using System.Linq;
using System.Text;
using IntelliSun.Helpers;

namespace IntelliSun.Text
{
    internal class TextPart
    {
        private readonly string[] parts;
        private readonly string conditionalText;
        private readonly bool isParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        private TextPart(string part, bool isParameter)
        {
            this.parts = new [] { part };
            this.isParameter = isParameter;
        }

        private TextPart(string[] parts, bool isParameter, string conditionalText)
        {
            this.parts = parts;
            this.isParameter = isParameter;
            this.conditionalText = conditionalText;
        }

        public string Format(string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            return String.Concat(value, this.conditionalText);
        }

        public string[] Parts
        {
            get { return this.parts; }
        }

        public bool IsParameter
        {
            get { return this.isParameter; }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.isParameter)
                sb.AppendFormat("Parameter{0}: ", this.parts.Length > 1 ? "s" : String.Empty);

            sb.Append(StringHelper.ConcatWith("{0}{?g[, ]}", this.parts));
            return sb.ToString();
        }

        public static TextPart FromString(string value, bool asParameter)
        {
            var sectors = new [] { value };
            if (String.IsNullOrEmpty(value))
                return new TextPart(value ?? String.Empty, false);
                
            if(asParameter)
                sectors = value.Split('?');

            var partString = sectors[0].Replace("\\|", "\u9999");
            var parts = partString.Split('|').Select(part => part.Replace('\u9999', '|'));
            return new TextPart(parts.ToArray(), asParameter, sectors.Length > 1 ? sectors[1] : String.Empty);
        }
    }
}