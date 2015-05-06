using System;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public static class ExpressionEvaluator
    {
        private static readonly ParameterExpression _dummyParameterExpression =
            Expression.Parameter(typeof(object), "_unused");

        public static object Evaluate(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var @delegate = CompileToDelegate(expression);
            return @delegate(null);
        }

        private static Func<object, object> CompileToDelegate(Expression expression)
        {
            var convertedIn = Expression.Convert(expression, typeof(Object));
            var delegateExpr = Expression.Lambda<Func<object, object>>(convertedIn, _dummyParameterExpression);
            return ExpressionCompiler<object, object>.Compile(delegateExpr);
        }
    }
}
