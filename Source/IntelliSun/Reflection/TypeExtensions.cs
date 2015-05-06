using System;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsEnum)
                return GetEnumDefaultValue(type);

            if (type.IsInterface || type.IsAbstract)
                return null;

            if (!type.IsClass || type.BaseType == typeof(ValueType))
                return Activator.CreateInstance(type);

            return null;
        }

        private static object GetEnumDefaultValue(Type enumType)
        {
            object res;
            try
            {
                res = Activator.CreateInstance(enumType);
            }
            catch
            {
                res = enumType.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .First()
                    .GetValue(null);
            }

            return res;
        }

        public static bool HasDefaultConstructor(this Type type)
        {
            var ctor = type.GetConstructor(Type.EmptyTypes);
            return ctor != null;
        }

        public static bool IsNullable(this Type type)
        {
            if (type.IsClass)
                return true;

            if (!type.IsGenericType)
                return false;

            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetAbsoluteType(this Type type)
        {
            if (!type.IsClass && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return type.GetGenericArguments()[0];

            return type;
        }
    }
}
