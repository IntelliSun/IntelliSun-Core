using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Helpers
{
    internal class SequentialFormatter
    {
        private const char ExpressionIn = '{';
        private const char ExpressionOut = '}';
        private const char EscapeInOut = '\\';

        //private const string FoldingIndic = "##@";

        //private static readonly Regex _regexA = new Regex(@"\{\{([^\}\{]+)\}\}");
        //private static readonly Regex _regexC = new Regex(@"\{([^\}\{]+)\}");

        private static readonly SequentialFormatter _singleton;

        static SequentialFormatter()
        {
            SequentialFormatter._singleton = new SequentialFormatter();
        }

        public string Format<T>(string format, IEnumerable<T> args)
        {
            var expressions = BuildExpressions(format);

            var objects = args as object[];
            objects = objects ?? args.Cast<object>().ToArray();

            var iterations = objects.Select((o, i) =>
            {
                var resls = expressions.Select(e => e.Resolve(objects, i));
                return String.Concat(resls);
            });

            return String.Concat(iterations);
        }

        public string FormatOnce<T>(string format, IEnumerable<T> args, int index)
        {
            var expressions = BuildExpressions(format);

            var objects = args as object[];
            objects = objects ?? args.Cast<object>().ToArray();

            var resls = expressions.Select(e => e.Resolve(objects, index));
            return String.Concat(resls);
        }

        private static IEnumerable<ISequentialExpression> BuildExpressions(string format)
        {
            var splits = SplitBase(format).ToArray();
            return splits.Select(token =>
                !token.IsExpression
                    ? new StaticSequentialExpression(token.Content)
                    : SequentialExpressionBase.GetExpression(token.Content));
        }

        private static IEnumerable<FormatToken> SplitBase(string format)
        {
            var index = 0;
            var length = format.Length;
            var backlog = String.Empty;
            var isExpression = false;

            while (true)
            {
                while (index < length)
                {
                    var current = format[index++];
                    var insert = current;

                    if (current == EscapeInOut && index < length &&
                        (format[index] == ExpressionIn || format[index] == ExpressionOut) &&
                        isExpression)
                        insert = format[index++];

                    if (current == ExpressionOut)
                    {
                        if ((index < length && format[index] == ExpressionOut) || isExpression) // Treat as escape character for }}
                        {
                            if (isExpression)
                                break;

                            index++;
                        }
                        else
                            FormatError();
                    }

                    if (current == ExpressionIn)
                    {
                        if (index < length && format[index] == ExpressionIn) // Treat as escape character for {{
                            index++;
                        else
                        {
                            index--;
                            break;
                        }
                    }

                    backlog += (insert);
                }

                if (backlog.Length > 0)
                    yield return Value(backlog, isExpression);

                backlog = String.Empty;
                isExpression = !isExpression;

                if ((index += isExpression ? 1 : 0) >= length)
                    break;
            }
        }

        private static void FormatError()
        {
            throw new FormatException();
        }

        private static FormatToken Value(string input, bool isExpreesion)
        {
            return new FormatToken(input, isExpreesion);
        }

        #region OLD

        //private static IEnumerable<FormatToken> SplitBase(string format)
        //{
        //    var input = format;
        //    while (SequentialFormatter._regexA.IsMatch(input))
        //        input = SequentialFormatter._regexA.Replace(input, SequentialFormatter.FoldingIndic + "$1");

        //    var lastIndex = 0;
        //    foreach (var result in SequentialFormatter._regexC.Matches(input).Cast<Match>())
        //    {
        //        if (result.Index - lastIndex > 0)
        //            yield return Value(input.Substring(lastIndex, result.Index - lastIndex), false);

        //        yield return Value(result.Groups[1].Value, true);
        //        lastIndex = result.Index + result.Length;
        //    }

        //    if (input.Length > lastIndex)
        //        yield return Value(input.Substring(lastIndex), false);
        //}

        //private static string Refold(string input)
        //{
        //    var match = Regex.Match(input, String.Format("({0}*)(.*)", SequentialFormatter.FoldingIndic));
        //    var count = match.Groups[1].Length / SequentialFormatter.FoldingIndic.Length;
        //    return count == 0
        //        ? input
        //        : String.Concat(
        //            String.Concat(Enumerable.Repeat(SequentialFormatter.ExpressionIn, count)),
        //            match.Groups[2].Value,
        //            String.Concat(Enumerable.Repeat(SequentialFormatter.ExpressionOut, count)));
        //}

        #endregion

        public static SequentialFormatter Instance
        {
            get { return SequentialFormatter._singleton; }
        }

        private struct FormatToken
        {
            private readonly string content;
            private readonly bool isExpression;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public FormatToken(string content, bool isExpression)
            {
                this.content = content;
                this.isExpression = isExpression;
            }

            public string Content
            {
                get { return this.content; }
            }

            public bool IsExpression
            {
                get { return this.isExpression; }
            }
        }
    }
}