using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IntelliSun.Helpers
{
    internal abstract class SequentialExpressionBase : ISequentialExpression
    {
        private static readonly Dictionary<Regex, Func<string, ISequentialExpression>> _expressions;

        static SequentialExpressionBase()
        {
            _expressions = new Dictionary<Regex, Func<string, ISequentialExpression>> {
                { ComplexSequentialExpression.PredicateRegex, ComplexSequentialExpression.Factory },
                { IndexSequentialExpression.PredicateRegex, IndexSequentialExpression.Factory },
                { CalcSequentialExpression.PredicateRegex, CalcSequentialExpression.Factory },
            };
        }

        public abstract string Resolve(object[] args, int index);

        public static ISequentialExpression GetExpression(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            foreach (var entry in _expressions)
            {
                if (entry.Key.IsMatch(expression))
                    return entry.Value(expression);
            }

            throw new FormatException();
        }
    }
}
