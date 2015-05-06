using System;
using System.Linq.Expressions;
using System.Reflection;

namespace IntelliSun.Expressions
{
    public static class ExpressionHelper
    {
        public static FieldInfo GetFieldInfo<TField>(this Expression<Func<TField>> fieldExpression)
        {
            var member = GetExpressionMember(fieldExpression);
            var fieldInfo = member as FieldInfo;
            if (fieldInfo == null)
                throw new ArgumentException("${Resources.ExpressionNotReferringToField}");

            return fieldInfo;
        }

        public static MethodInfo GetMethodInfo<T>(this Expression<Func<T>> methodExpression)
        {
            return GetMethodInfo((LambdaExpression)methodExpression);
        }

        public static MethodInfo GetMethodInfo<TIn, T>(this Expression<Func<TIn, T>> methodExpression)
        {
            return GetMethodInfo((LambdaExpression)methodExpression);
        }

        public static MethodInfo GetMethodInfo(this Expression<Action> methodExpression)
        {
            return GetMethodInfo((LambdaExpression)methodExpression);
        }

        public static MethodInfo GetMethodInfo<TIn>(this Expression<Action<TIn>> methodExpression)
        {
            return GetMethodInfo((LambdaExpression)methodExpression);
        }

        public static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            return expression.IsMemberExpression()
                ? GetExpressionMethod(expression)
                : GetCallExpressionMethod(expression);
        }

        public static PropertyInfo GetPropertyInfo<TProperty>(this Expression<Func<TProperty>> propertyExpression)
        {
            var member = GetExpressionMember(propertyExpression);
            var propertyInfo = member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("${Resources.ExpressionNotReferringToProperty}");

            return propertyInfo;
        }

        public static PropertyInfo GetPropertyInfo<TClass, TProperty>(this Expression<Func<TClass, TProperty>> propertyExpression)
        {
            var member = GetExpressionMember(propertyExpression);
            var propertyInfo = member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("${Resources.ExpressionNotReferringToProperty}");

            return propertyInfo;
        }

        public static MemberInfo GetMemberInfo<T>(this Expression<Func<T>> expression)
        {
            return GetExpressionMember(expression);
        }

        public static bool IsMemberExpression(this LambdaExpression expression)
        {
            return expression.Body is MemberExpression;
        }

        private static MemberInfo GetExpressionMember(LambdaExpression expression)
        {
            var expressionBody = expression.Body;
            var memberExpression = expressionBody as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("${Resources.NotAMemberExpression}", "expression");

            return memberExpression.Member;
        }

        private static MethodInfo GetExpressionMethod(LambdaExpression expression)
        {
            var member = GetExpressionMember(expression);
            var methodInfo = member as MethodInfo;
            if (methodInfo == null)
                throw new ArgumentException("${Resources.ExpressionNotReferringToMethod}");

            return methodInfo;
        }

        private static MethodInfo GetCallExpressionMethod(LambdaExpression expression)
        {
            var expressionBody = expression.Body;
            var callExpression = expressionBody as MethodCallExpression;
            if (callExpression == null)
                throw new ArgumentException("${Resources.NotAMethodExpression}", "expression");

            return callExpression.Method;
        }
    }
}
