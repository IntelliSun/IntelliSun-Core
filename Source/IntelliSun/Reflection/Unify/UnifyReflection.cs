using System;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    public static class UnifyReflection
    {
        private static readonly Type[] _unifiableTypes = {
            typeof(PropertyInfo), typeof(MethodInfo), typeof(FieldInfo)
        };

        public static IReflectInfo Unify(this ParameterInfo parameterInfo)
        {
            return new ReflectParameterInfo(parameterInfo);
        }

        public static IReflectInfo Unify(this MethodInfo methodInfo)
        {
            return new ReflectMethodInfo(methodInfo);
        }

        public static IReflectInfo Unify(this PropertyInfo propertyInfo)
        {
            return new ReflectPropertyInfo(propertyInfo);
        }

        public static IReflectInfo Unify(this FieldInfo fieldInfo)
        {
            return new ReflectFieldInfo(fieldInfo);
        }

        public static IReflectInfo Unify(this MemberInfo memberInfo)
        {
            var asProperty = memberInfo as PropertyInfo;
            if (asProperty != null)
                return asProperty.Unify();

            var asMethod = memberInfo as MethodInfo;
            if (asMethod != null)
                return asMethod.Unify();

            var asField = memberInfo as FieldInfo;
            if (asField != null)
                return asField.Unify();

            throw new ArgumentException("${Resources.UnunifiableMemberInfo}", "memberInfo");
        }

        public static bool IsUnifiable(this MemberInfo memberInfo)
        {
            return _unifiableTypes.Any(type => type.IsInstanceOfType(memberInfo));
        }
    }
}