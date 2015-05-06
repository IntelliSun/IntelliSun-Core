using System;
using System.Text.RegularExpressions;

namespace IntelliSun.Helpers
{
    internal class CalcSequentialExpression : SequentialExpressionBase
    {
        public static readonly Regex PredicateRegex = new Regex(@"^i([+-]?)([0-9]*)$");

        private readonly int offset;

        public CalcSequentialExpression(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var match = CalcSequentialExpression.PredicateRegex.Match(expression);
            if (!match.Success)
                throw new FormatException();

            var factorValue = match.Groups[1].Value;
            var factor = factorValue.Length == 0 ? 1 : factorValue[0] == '+' ? 1 : -1;
            this.offset = factor * Int32.Parse(match.Groups[2].Value);
        }

        public override string Resolve(object[] args, int index)
        {
            return (index + this.offset).ToString();
        }

        public static Func<string, ISequentialExpression> Factory
        {
            get { return s => new CalcSequentialExpression(s); }
        }
    }
}