using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public static class ReflectionHelper
    {
        #region Property Fetch

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl<TAttr>(
                type, BindingFlags.Public | BindingFlags.Instance, t => true, true);

            return query.ToArray();
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl<TAttr>(type, BindingFlags.Public | BindingFlags.Instance,
                t => true, inherit);

            return query.ToArray();
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type,
            Predicate<PropertyAttributeInfo<TAttr>> predicate)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl(type, BindingFlags.Public | BindingFlags.Instance, predicate, true);

            return query.ToArray();
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type,
            Predicate<PropertyAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl(type, BindingFlags.Public | BindingFlags.Instance, predicate, inherit);

            return query.ToArray();
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type, BindingFlags binding)
            where TAttr : Attribute
        {
            return GetFlaggedProperties<TAttr>(type, binding, _ => true);
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type, BindingFlags binding,
            Predicate<PropertyAttributeInfo<TAttr>> predicate)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl(type, binding, predicate, true);

            return query.ToArray();
        }

        public static PropertyAttributeInfo<TAttr>[] GetFlaggedProperties<TAttr>(this Type type, BindingFlags binding,
            Predicate<PropertyAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedPropertiesImpl(type, binding, predicate, inherit);

            return query.ToArray();
        }

        private static IEnumerable<PropertyAttributeInfo<TAttr>> GetFlaggedPropertiesImpl<TAttr>(
            this Type type, BindingFlags binding, Predicate<PropertyAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = from prop in type.GetProperties(binding)
                        let attrs = prop.GetCustomAttributes(typeof(TAttr), inherit).Cast<TAttr>()
                        where attrs.Any()
                        let pai = new PropertyAttributeInfo<TAttr> {
                            PropertyInfo = prop,
                            Attributes = attrs.ToArray()
                        }
                        where predicate(pai)
                        select pai;

            return query;
        }

        #endregion

        #region Method Fetch

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl<TAttr>(
                type, BindingFlags.Public | BindingFlags.Instance, t => true, true);

            return query.ToArray();
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl<TAttr>(type, BindingFlags.Public | BindingFlags.Instance,
                t => true, inherit);

            return query.ToArray();
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type,
            Predicate<MethodAttributeInfo<TAttr>> predicate)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl(type, BindingFlags.Public | BindingFlags.Instance, predicate, true);

            return query.ToArray();
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type,
            Predicate<MethodAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl(type, BindingFlags.Public | BindingFlags.Instance, predicate, inherit);

            return query.ToArray();
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type, BindingFlags binding)
            where TAttr : Attribute
        {
            return GetFlaggedMethods<TAttr>(type, binding, _ => true);
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type, BindingFlags binding,
            Predicate<MethodAttributeInfo<TAttr>> predicate)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl(type, binding, predicate, true);

            return query.ToArray();
        }

        public static MethodAttributeInfo<TAttr>[] GetFlaggedMethods<TAttr>(this Type type, BindingFlags binding,
            Predicate<MethodAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = GetFlaggedMethodsImpl(type, binding, predicate, inherit);

            return query.ToArray();
        }

        private static IEnumerable<MethodAttributeInfo<TAttr>> GetFlaggedMethodsImpl<TAttr>(
            this Type type, BindingFlags binding, Predicate<MethodAttributeInfo<TAttr>> predicate, bool inherit)
            where TAttr : Attribute
        {
            var query = from prop in type.GetMethods(binding)
                        let attrs = prop.GetCustomAttributes(typeof(TAttr), inherit).Cast<TAttr>()
                        where attrs.Any()
                        let pai = new MethodAttributeInfo<TAttr>
                        {
                            MethodInfo = prop,
                            Attributes = attrs.ToArray()
                        }
                        where predicate(pai)
                        select pai;

            return query;
        }

        #endregion

        public static bool IsFlagged<TAttr>(this ICustomAttributeProvider member)
            where TAttr : Attribute
        {
            return member.IsFlagged(typeof(TAttr));
        }

        public static bool IsFlagged<TAttr>(this ICustomAttributeProvider member, bool inherit)
            where TAttr : Attribute
        {
            return member.IsFlagged(typeof(TAttr), inherit);
        }

        public static bool IsFlagged(this ICustomAttributeProvider member, Type attributeType)
        {
            return member.IsFlagged(attributeType, false);
        }

        public static bool IsFlagged(this ICustomAttributeProvider member, Type attributeType, bool inherit)
        {
            if (!attributeType.IsImplementingType(typeof(Attribute)))
                return false;

            return member.IsDefined(attributeType, inherit);
        }

        public static bool IsDrrivedTree(this Type source, Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type == typeof(object))
                return true;

            if (type == source)
                return true;

            if (source.BaseType == type)
                return true;

            Type drvType = source.BaseType;
            while (drvType != null)
            {
                if ((drvType = drvType.BaseType) == type)
                    return true;
            }

            return false;
        }

        public static int GetInheritanceComplexity(this Type type, Type targetType)
        {
            return targetType.IsInterface
                ? GetInterfaceComplexity(type, targetType, 0)
                : GetClassComplexity(type, targetType, 0);
        }

        public static int GetClassComplexity(Type type, Type targetType, int complexityBase)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            while (true)
            {
                var baseType = type.BaseType;
                if (baseType == typeof(object) || baseType == null)
                    return -1;

                if (baseType == targetType)
                    return complexityBase + 1;

                complexityBase++;
                type = baseType;
            }
        }

        private static int GetInterfaceComplexity(Type type, Type baseType, int complexityBase)
        {
            if (type == baseType)
                return 0;

            var interfaces = type.GetInterfaces();
            if (interfaces.Length == 0)
                return -1;

            if (!interfaces.Contains(baseType))
                return -1;

            var query = interfaces.Select(t => GetInterfaceComplexity(t, baseType, complexityBase));
            return complexityBase + query.Max() + 1;
        }

        public static bool IsImplementingInterface<T>(this Type type)
        {
            return type.IsImplementingInterface(typeof(T));
        }

        public static bool IsImplementingInterface(this Type type, Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            return type.IsImplementingType(interfaceType);
        }

        public static bool IsImplementingType(this Type type, Type otherType)
        {
            if (otherType == null)
                throw new ArgumentNullException("otherType");

            return otherType.IsAssignableFrom(type);
        }

        public static string GetFriendlyName(this Type source)
        {
            return source.Name.Split('.').Last();
        }

        public static bool IsReadWrite(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite || !propertyInfo.CanRead)
                return false;

            var getMethod = propertyInfo.GetMethod;
            var setMethod = propertyInfo.SetMethod;

            return getMethod != null && getMethod.IsPublic &&
                   setMethod != null && setMethod.IsPublic;
        }

        public static bool IsWriteOnly(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite || propertyInfo.CanRead)
                return false;

            var setMethod = propertyInfo.SetMethod;
            return setMethod != null && setMethod.IsPublic;
        }

        public static bool IsReadOnly(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead || propertyInfo.CanWrite)
                return false;

            var getMethod = propertyInfo.GetMethod;
            return getMethod != null && getMethod.IsPublic;
        }

        public static MemberAttributesPair<T> WithFlag<T>(this MemberInfo member) 
            where T : Attribute
        {
            var attributes = member.GetCustomAttributes<T>();
            return new MemberAttributesPair<T>(member, attributes.ToArray());
        }

        public static IEnumerable<MemberAttributesPair<T>> WhereFlagged<T>(this IEnumerable<MemberInfo> members)
            where T : Attribute
        {
            return members.Select(info => info.WithFlag<T>()).Where(pair => pair.HasAttributes);
        }

        public static object GetDefaultValue(this ParameterInfo parameterInfo)
        {
            var valueDefault = parameterInfo.DefaultValue;
            return valueDefault == DBNull.Value
                ? parameterInfo.ParameterType.GetDefaultValue()
                : valueDefault;
        }
    }

    public class MethodAttributeInfo<T> : IMethodAttributeInfo<T>
        where T : Attribute
    {
        public MethodAttributeInfo()
        {

        }

        public MethodAttributeInfo(MethodInfo methodInfo, T[] attributes)
        {
            this.Attributes = attributes;
            this.MethodInfo = methodInfo;
        }

        public T[] Attributes { get; set; }
        public MethodInfo MethodInfo { get; set; }

        Attribute[] IMethodAttributeInfo.Attributes
        {
            get { return this.Attributes.Cast<Attribute>().ToArray(); }
        }
    }

    public interface IMethodAttributeInfo<out T> : IMethodAttributeInfo
        where T : Attribute
    {
        new T[] Attributes { get; }
    }

    public interface IMethodAttributeInfo
    {
        MethodInfo MethodInfo { get; }
        Attribute[] Attributes { get; }
    }

    public class MethodAttributeInfo : IMethodAttributeInfo
    {
        public MethodAttributeInfo()
        {

        }

        public MethodAttributeInfo(MethodInfo methodInfo, Attribute[] attributes)
        {
            this.Attributes = attributes;
            this.MethodInfo = methodInfo;
        }

        public Attribute[] Attributes { get; set; }
        public MethodInfo MethodInfo { get; set; }
    }
}
