using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IntelliSun.Helpers
{
    internal class ComplexSequentialExpression : SequentialExpressionBase
    {
        private const string PatternTemplate = @"\?([{-keys}])\[(.*)\]";

        private static readonly Regex _pattern;
        private static readonly Dictionary<ExpressionKey, Func<int, int, bool>> _predicates;

        public static readonly Regex PredicateRegex = new Regex(@"\?([a-zA-Z])\[(.*)\]", RegexOptions.Singleline);

        private readonly ExpressionKey key;
        private readonly string content;

        static ComplexSequentialExpression()
        {
            ComplexSequentialExpression._predicates = new Dictionary<ExpressionKey, Func<int, int, bool>> {
                { ExpressionKey.First, (index, length) => index == 0 },
                { ExpressionKey.General, (index, length) => index != length - 1 },
                { ExpressionKey.Internal, (index, length) => index != 0 },
                { ExpressionKey.Last, (index, length) => index == length - 1 },
                { ExpressionKey.Middle, (index, length) => index != 0 && index != length - 1 },
                { ExpressionKey.Prefix, (index, length) => length != 1 && index == 0 }
            };

            var pattern = ComplexSequentialExpression.PatternTemplate.Replace("{-keys}",
                String.Concat(Enum.GetValues(typeof(ExpressionKey))
                                  .Cast<int>().Select(x => (char)x)));

            ComplexSequentialExpression._pattern = new Regex(pattern, RegexOptions.Singleline);
        }

        public ComplexSequentialExpression(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (!ComplexSequentialExpression.PredicateRegex.IsMatch(expression))
                throw new FormatException();

            var match = ComplexSequentialExpression._pattern.Match(expression);
            if (!match.Success)
                throw new ArgumentException("${Resources.UnknownExpressionKey}", "expression");

            var keyChar = match.Groups[1].Value[0];
            this.key = (ExpressionKey)keyChar;
            this.content = match.Groups[2].Value;
        }

        public override string Resolve(object[] args, int index)
        {
            return !ComplexSequentialExpression._predicates[this.key](index, args.Length)
                ? String.Empty
                : this.content;
        }

        public static Func<string, ISequentialExpression> Factory
        {
            get { return s => new ComplexSequentialExpression(s); }
        }
    }
}