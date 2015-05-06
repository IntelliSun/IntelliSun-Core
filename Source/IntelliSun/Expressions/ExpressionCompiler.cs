using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace IntelliSun.Expressions
{
    internal static class ExpressionCompiler<TIn, TOut>
    {
        private static Func<TIn, TOut> _identityFunc;

        private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> _simpleMemberAccessDict =
            new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();

        private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> _constMemberAccessDict =
            new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();

        public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
        {
            return CompileFromIdentityFunc(expr)
                   ?? CompileFromConstLookup(expr)
                   ?? CompileFromMemberAccess(expr)
                   ?? CompileSlow(expr);
        }

        private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
        {
            var constExpr = expr.Body as ConstantExpression;
            if (constExpr == null)
                return null;

            return _ => (TOut)constExpr.Value;
        }

        private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
        {
            if (expr.Body == expr.Parameters[0])
                return _identityFunc ?? (_identityFunc = expr.Compile());

            return null;
        }

        private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
        {
            var memberExpr = expr.Body as MemberExpression;
            if (memberExpr == null)
                return null;

            if (memberExpr.Expression == expr.Parameters[0] || memberExpr.Expression == null)
                return _simpleMemberAccessDict.GetOrAdd(memberExpr.Member, _ => expr.Compile());

            if(memberExpr.Member.DeclaringType == null)
                return null;

            var constExpr = memberExpr.Expression as ConstantExpression;
            if (constExpr == null)
                return null;

            var @delegate = _constMemberAccessDict.GetOrAdd(memberExpr.Member, _ => {
                var constParamExpr = Expression.Parameter(typeof(object), "capturedLocal");
                var constCastExpr = Expression.Convert(constParamExpr, memberExpr.Member.DeclaringType);
                var newMemberAccessExpr = memberExpr.Update(constCastExpr);
                var newLambdaExpr = Expression.Lambda<Func<object, TOut>>(newMemberAccessExpr, constParamExpr);
                return newLambdaExpr.Compile();
            });

            var capturedLocal = constExpr.Value;
            return _ => @delegate(capturedLocal);
        }

        private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
        {
            return expr.Compile();
        }
    }
}