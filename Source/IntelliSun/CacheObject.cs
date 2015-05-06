using System;
using System.Linq.Expressions;
using System.Reflection;

namespace IntelliSun
{
    public abstract class CacheObject
    {
        private readonly CacheProvider cache;

        protected CacheObject()
        {
            this.cache = new CacheProvider();
        }

        protected string GetCacheKeyByMember(MethodInfo method)
        {
            return method.Name.Replace("Get", "");
        }

        protected string GetCacheKeyByMember(PropertyInfo property)
        {
            return property.Name;
        }

        protected string GetCacheKeyByMember<TMember>(Expression<Func<TMember>> member)
        {
            var memberInfo = GetMemberInfo(member);
            if ((memberInfo.MemberType & MemberTypes.Method) != 0)
                return this.GetCacheKeyByMember((MethodInfo)memberInfo);

            return memberInfo.Name;
        }

        protected T GetCachedDataOrValue<T>(string key, Func<T> value, bool keep = true)
        {
            return this.cache.GetCachedDataOrValue(key, value, keep);
        }

        protected CacheProvider GetCacheProxy()
        {
            //TODO: Implement real proxy model
            return this.cache;
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            var asLambda = expression as LambdaExpression;
            if (asLambda == null)
                return null;

            var memberExpression = GetMemberExpression(asLambda.Body);
            return memberExpression.Member;
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            var unaryExpression = expression as UnaryExpression;
            var source = unaryExpression != null
                ? (MemberExpression)unaryExpression.Operand
                : (MemberExpression)expression;

            return source;
        }
    }
}