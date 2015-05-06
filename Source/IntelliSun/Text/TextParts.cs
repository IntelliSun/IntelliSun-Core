using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace IntelliSun.Text
{
    [DebuggerDisplay("Count = {Count}")]
    internal class TextParts
    {
        private const string RegexExpression = @"\[([a-zA-Z\$\|]*(?:\?[^\[\]]+)?)\]";
        private static readonly Regex _regex = new Regex(RegexExpression);

        private readonly IEnumerable<TextPart> parts;

        public TextParts(string text)
        {
            this.parts = ExtractParts(ModifyString(text));
        }

        private static string ModifyString(string value)
        {
            var matches = _regex.Matches(value).Cast<Match>();
            value = matches.Aggregate(value,
                (current, match) => current.Replace(
                    String.Format("[{0}]", match.Groups[1].Value),
                    String.Format("%${0}%", match.Groups[1])));

            return value;
        }

        private static IEnumerable<TextPart> ExtractParts(string value)
        {
            var split = value.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            return split.Select(s => {
                var hasConditionPart = s.Length > 1 && s[1] == '?';
                var isParameter = s[0] == '$' && !hasConditionPart;
                var text = isParameter ? s.Substring(1) : (hasConditionPart ? s.Substring(2) : s);
                return TextPart.FromString(text, isParameter);
            });
        }

        public IEnumerable<TextPart> Parts
        {
            get { return this.parts; }
        }

        // ReSharper disable once UnusedMember.Local
        private int Count
        {
            get { return this.parts.Count(); }
        }
    }
}