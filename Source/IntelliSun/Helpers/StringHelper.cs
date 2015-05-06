using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IntelliSun.Collections;

namespace IntelliSun.Helpers
{
    public static class StringHelper
    {
        public static string Truncate(this string value, int length)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return value.Length <= length ? value : value.Substring(0, length);
        }

        public static string SelectBetween(this string i, char first, char last)
        {
            return SelectBetween(i, first, last, 0);
        }

        public static string SelectBetween(this string i, char first, char last, int startIndex)
        {
            var idx1 = i.IndexOf(first, startIndex) + 1;
            var idx2 = i.IndexOf(last, idx1);

            if (idx1 == 0 || idx2 == -1)
                return null;

            return i.Substring(idx1, idx2 - idx1);
        }

        public static string TokenFormat(string format, IDictionary<char, string> tokens)
        {
            return TokenFormat(format, tokens, "-{0}");
        }

        public static string TokenFormat(string format, IDictionary<char, string> tokens, string template)
        {
            return TokenFormat(format, tokens.ToDictionary(
                x => x.Key.ToString(CultureInfo.InvariantCulture), x => x.Value), template);
        }

        public static string TokenFormat(string format, IDictionary<string, string> tokens)
        {
            return TokenFormat(format, tokens, "-{0}");
        }

        public static string TokenFormat(string format, IDictionary<string, string> tokens, string template)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;

            return tokens.Count == 0
                ? format
                : tokens.Aggregate(format, (current, t) => current.Replace(String.Format(template, t.Key), t.Value));
        }

        public static bool StartsWithAny(this string self, params string[] value)
        {
            return value.Any(self.StartsWith);
        }

        public static bool StartsWithAny(this string self, StringComparison comparisonType, params string[] value)
        {
            return value.Any(x => self.StartsWith(x, comparisonType));
        }

        public static bool StartsWithAny(this string self, bool ignoreCase, CultureInfo culture, params string[] value)
        {
            return value.Any(x => self.StartsWith(x, ignoreCase, culture));
        }

        public static string InteractiveFormat(string format, Func<string, string> accessor)
        {
            return InteractiveFormat(format, accessor, "-{0}");
        }

        public static string InteractiveFormat(string format, Func<string, string> accessor, string template)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;

            if (accessor == null)
                return format;

            if (String.IsNullOrEmpty(template))
                template = "-{0}";

            if (template.Count(x => x.Equals('0')) != 1)
                throw new FormatException("Format template must contain one content symbol");

            var templateParts = template.Split('0');
            var templateStart = templateParts[0];
            var templateEnd = templateParts[1];
            var templateLevel = 0;

            var contentCollector = new StringBuilder();
            var stringBuilder = new StringBuilder();
            var pendingCollector = new StringBuilder();
            var idx = 0;
            while (idx < format.Length)
            {
                var cast = format[idx++];
                if (templateLevel != templateStart.Length)
                {
                    if (cast == templateStart[templateLevel])
                        pendingCollector.Append(templateStart[templateLevel++]);
                    else
                    {
                        if (templateLevel > 0)
                        {
                            templateLevel = 0;
                            stringBuilder.Append(pendingCollector);
                            pendingCollector.Clear();
                        }
                        stringBuilder.Append(cast);
                    }
                }
                else
                {
                    if (cast == templateEnd[0])
                    {
                        templateLevel = 0;
                        var val = contentCollector.ToString();
                        stringBuilder.Append(accessor(val));
                        contentCollector.Clear();
                        continue;
                    }

                    contentCollector.Append(cast);
                }
            }

            return stringBuilder.ToString();
        }

        public static string InteractiveFormat(string format, InteractiveFormatParams parameters)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;

            if (parameters.Accessor == null)
                return format;

            if (String.IsNullOrEmpty(parameters.Template))
                parameters.Template = "-{0}";

            if (parameters.Template.Count(x => x.Equals('0')) != 1)
                throw new FormatException("Format template must contain only one content symbol");

            var templateParts = parameters.Template.Split('0');
            var templateStart = templateParts[0];
            var templateEnd = templateParts[1];
            var templateLevel = 0;

            var contentCollector = new StringBuilder();
            var stringBuilder = new StringBuilder();
            var pendingCollector = new StringBuilder();
            var idx = 0;
            while (idx < format.Length)
            {
                var cast = format[idx++];
                if (templateLevel != templateStart.Length)
                {
                    if (cast == templateStart[templateLevel])
                        pendingCollector.Append(templateStart[templateLevel++]);
                    else
                    {
                        if (templateLevel > 0)
                        {
                            templateLevel = 0;
                            stringBuilder.Append(pendingCollector);
                            pendingCollector.Clear();
                        }
                        stringBuilder.Append(cast);
                    }
                }
                else
                {
                    if (cast == templateEnd[0])
                    {
                        templateLevel = 0;
                        var val = contentCollector.ToString();
                        var par = parameters.ArgsPreset.FormKey(val);
                        stringBuilder.Append(parameters.Accessor(par));
                        contentCollector.Clear();
                        continue;
                    }

                    contentCollector.Append(cast);
                }
            }

            return stringBuilder.ToString();
        }

        public static string AdvancedFormat(string format, Dictionary<char, Func<int, string, string>> tokens)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;
            if (tokens.Count == 0)
                return format;

            var res = format;
            var selections = new Regex(@"\\").Matches(format);

            foreach (var selection in selections)
            {
                var rep = selection.ToString();
                var ch = rep[1];
                if (!tokens.ContainsKey(ch))
                    continue;

                var integer = rep.Substring(3, rep.Length - 4);

                var endSelection = res.Truncate(res.IndexOf(rep, StringComparison.Ordinal));
                res = res.Replace(rep, tokens[ch].Invoke(Convert.ToInt32(integer), endSelection));
            }

            return res;
        }

        public static string SequentialFormat(string format, Dictionary<char, Func<char, int, string>> tokens)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;
            if (tokens.Count == 0)
                return format;

            var res = new StringBuilder(format);
            var selections = new Regex(@"![a-z0-9]").Matches(format);

            var accumulator = 0;

            var idx = 0;
            foreach (Match selection in selections)
            {
                var rep = selection.ToString();
                var ch = rep[1];
                if (!tokens.ContainsKey(ch))
                    continue;

                var newText = tokens[ch](ch, idx);
                res = res.Replace(rep, newText, selection.Index + accumulator, selection.Length);

                accumulator += newText.Length - 2;

                idx++;
            }

            return res.ToString();
        }

        public static string CharFormat(string format, string originalString)
        {
            if (String.IsNullOrEmpty(format))
                return String.Empty;

            var matches = Regex.Matches(format, "{[0-9]*}");
            foreach (var m in matches)
            {
                var ms = m.ToString();
                var k = Int32.Parse(ms.Substring(1, ms.Length - 2));
                format = format.Replace(ms, originalString[k].ToString(CultureInfo.InvariantCulture));
            }

            return format;
        }

        public static bool EndsWith(this string text, IEnumerable<string> strings)
        {
            return strings.Any(text.EndsWith);
        }

        public static bool EndsWith(this string text, IEnumerable<string> strings, bool ignoreCase)
        {
            return strings.Any(s => text.EndsWith(s, ignoreCase, CultureInfo.InvariantCulture));
        }

        public static bool EqualsAny(this string text, params string[] strings)
        {
            return text.EqualsAny(strings, false);
        }

        public static bool EqualsAny(this string text, string[] strings, bool ignoreCase)
        {
            return text.EqualsAny((IEnumerable<string>)strings, false);
        }

        public static bool EqualsAny(this string text, IEnumerable<string> strings)
        {
            return text.EqualsAny(strings, false);
        }

        public static bool EqualsAny(this string text, IEnumerable<string> strings, bool ignoreCase)
        {
            var comparison = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            return strings.Any(s => text.Equals(s, comparison));
        }

        public static bool EqualsTo(this string source, string value, bool ignoreCase)
        {
            var comparison = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            return source.Equals(value, comparison);
        }

        public static string RemoveLast(this string i)
        {
            return RemoveLast(i, 1);
        }

        public static string RemoveLast(this string i, int count)
        {
            return i.Remove(i.Length - count);
        }

        public static string RemoveSuffix(this string text, string suffix)
        {
            return text.EndsWith(suffix) ? text.RemoveLast(suffix.Length) : text;
        }

        public static string RemoveAnySuffix(this string text, params string[] suffixes)
        {
            return RemoveAnySuffix(text, (IEnumerable<string>)suffixes);
        }

        public static string RemoveAnySuffix(this string text, IEnumerable<string> suffixes)
        {
            var suffix = suffixes.SingleOrDefault(text.EndsWith);
            return suffix == null ? text : RemoveSuffix(text, suffix);
        }

        public static bool IsInteger(this string value)
        {
            long _;
            return Int64.TryParse(value, out _);
        }

        public static bool IsDecimal(this string value)
        {
            double _;
            return Double.TryParse(value, out _);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i)
        {
            int _;
            return Int32.TryParse(i, out _);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i, bool acceptDecimal)
        {
            return i.IsNumber(true, acceptDecimal);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i, bool acceptDecimal, bool acceptComma)
        {
            decimal rn;

            if (acceptComma)
                i = i.Replace(",", "");

            return Decimal.TryParse(i, out rn);
        }

        public static string[] SplitWords(this string text)
        {
            return SplitWordsCore(text).ToArray();
        }

        private static IEnumerable<string> SplitWordsCore(this string text)
        {
            var builder = new StringBuilder();
            foreach (var c in text)
            {
                if (Char.IsUpper(c) || c == '-' || c == ' ' || c == '_')
                {
                    var word = builder.ToString();
                    builder.Clear();

                    if (!String.IsNullOrEmpty(word))
                        yield return word;
                }

                builder.Append(c);
            }

            var lastWord = builder.ToString();
            if (!String.IsNullOrEmpty(lastWord))
                yield return lastWord;
        }

        [Obsolete("Use split words instead")]
        public static string[] SplitCasing(this string text)
        {
            return InternalSplitCasing(text).ToArray();
        }

        private static IEnumerable<string> InternalSplitCasing(string text)
        {
            var wordCast = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsUpper(text[i]) && i != 0)
                {
                    yield return wordCast.ToString();
                    wordCast.Clear();
                }

                wordCast.Append(text[i]);
            }

            if (wordCast.Length > 0)
                yield return wordCast.ToString();
        }

        public static string Cut(this string i, int fromEdges)
        {
            return i.Cut(fromEdges, fromEdges);
        }

        public static string Cut(this string i, int fromStart, int fromEnd)
        {
            if (fromStart < 0 || fromStart >= i.Length)
                throw new ArgumentOutOfRangeException("fromStart");
            if (fromEnd < 0 || fromEnd >= i.Length)
                throw new ArgumentOutOfRangeException("fromEnd");

            return i.Substring(fromStart, i.Length - fromEnd - fromStart);
        }

        public static string Wrap(this string i, char c)
        {
            return i.Wrap(c, c);
        }

        public static string Wrap(this string i, WrappingSet set)
        {
            var data = "\0\0";
            switch (set)
            {
                case WrappingSet.Parentheses:
                    data = "()";
                    break;
                case WrappingSet.SquareBrackets:
                    data = "[]";
                    break;
                case WrappingSet.Bracket:
                    data = "{}";
                    break;
                case WrappingSet.DoubleQuotes:
                    data = "\"\"";
                    break;
                case WrappingSet.SingleQuotes:
                    data = "''";
                    break;
            }

            return String.Concat(data[0], i, data[1]);
        }

        public static string Wrap(this string i, char cFirst, char cLast)
        {
            return String.Concat(cFirst, i, cLast);
        }

        public static bool Contains(this string i, IEnumerable<char> value)
        {
            return value.Any(i.Contains);
        }

        public static bool Contains(this string i, IEnumerable<string> value)
        {
            return value.Any(i.Contains);
        }

        public static string ConcatWith(string format, string str1)
        {
            return ConcatWith(format, new object[] { str1 });
        }

        public static string ConcatWith(string format, object arg1, object arg2)
        {
            return ConcatWith(format, new[] { arg1, arg2 });
        }

        public static string ConcatWith(string format, string str1, string str2)
        {
            return ConcatWith(format, new object[] { str1, str2 });
        }

        public static string ConcatWith(string format, IEnumerable<string> values)
        {
            return ConcatWith(format, values.Cast<object>());
        }

        public static string ConcatWith<T>(string format, IEnumerable<T> values)
        {
            return ConcatWith(format, values.Cast<object>());
        }

        public static string ConcatWith(string format, params string[] args)
        {
            return ConcatWith(format, args.Cast<object>());
        }

        public static string ConcatWith(string format, IEnumerable<object> args)
        {
            return ConcatWith(format, args.ToArray());
        }

        public static string ConcatWith(string format, params object[] args)
        {
            return SequentialFormatter.Instance.Format(format, args);
        }

        public static string FirstAs(this string value, CaseOption option)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            var ch = option == CaseOption.Lower
                         ? Char.ToLower(value[0])
                         : Char.ToUpper(value[0]);

            return String.Concat(ch, value.Substring(1));
        }

        public static string ReformatWords(this string value, CasingOption option)
        {
            return value.ReformatWords(option, ' ');
        }

        public static string ReformatWords(this string value, CasingOption option, char splitter)
        {
            return ReformatWords(value.Split(splitter), option);
        }

        public static string ReformatWords(this string value, CasingOption option, string splitter)
        {
            return ReformatWords(value.Split(new[] { splitter }, StringSplitOptions.None), option);
        }

        public static string ReformatWords(string[] words, CasingOption option)
        {
            if(words == null || words.IsEmpty())
                return String.Empty;

            var firstOption = option == CasingOption.AllUpper ||
                              option == CasingOption.FirstUpper ||
                              option == CasingOption.UpperCamelCase
                                  ? CaseOption.Upper
                                  : CaseOption.Lower;

            var otherOption = option == CasingOption.AllUpper ||
                              option == CasingOption.LowerCamelCase ||
                              option == CasingOption.UpperCamelCase
                                  ? CaseOption.Upper
                                  : CaseOption.Lower;

            var separator = (int)option < 10 ? "_" : "";
            var builder = new StringBuilder(words[0].FirstAs(firstOption));
            foreach (var word in words.Skip(1))
                builder.Append(String.Concat(separator, word.FirstAs(otherOption)));

            return builder.ToString();
        }

        public static string[] Split(string value, Func<char, bool> splitter)
        {
            return SplitInternal(value, splitter).ToArray();
        }

        private static IEnumerable<string> SplitInternal(string value, Func<char, bool> splitter)
        {
            if (String.IsNullOrEmpty(value))
                yield break;

            var splitIndex = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!splitter(value[i]))
                    continue;

                yield return value.Substring(splitIndex, i - splitIndex);
                splitIndex = i + 1;
            }

            yield return value.Substring(splitIndex);
        }

        public enum WrappingSet
        {
            Parentheses,
            SquareBrackets,
            Bracket,
            DoubleQuotes,
            SingleQuotes
        }
    }
}
