using System;
using System.Text.RegularExpressions;

namespace IntelliSun.Helpers
{
    internal class IndexSequentialExpression : SequentialExpressionBase
    {
        public static readonly Regex PredicateRegex = new Regex(@"^([+-]?)([0-9]*)$");

        private readonly int offset;

        public IndexSequentialExpression(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var match = IndexSequentialExpression.PredicateRegex.Match(expression);
            if (!match.Success)
                throw new FormatException();

            var factorValue = match.Groups[1].Value;
            var factor = factorValue.Length == 0 ? 1 : factorValue[0] == '+' ? 1 : -1;
            this.offset = factor * Int32.Parse(match.Groups[2].Value);
        }

        public override string Resolve(object[] args, int index)
        {
            return args[Wrap(index + this.offset, 0, args.Length - 1)].ToString();
        }

        private static int Wrap(int value, int min, int max)
        {
            var range = max - min + 1;
            if (value < min)
                value += range * ((min - value) / range + 1);

            return min + (value - min) % range;
        }

        public static Func<string, ISequentialExpression> Factory
        {
            get { return s => new IndexSequentialExpression(s); }
        }
    }
}