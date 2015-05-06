using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public static class PropertyExtensions
    {
        public static bool CanPublicWrite(this PropertyInfo propertyInfo)
        {
            return propertyInfo.CanWrite &&
                   propertyInfo.SetMethod.IsPublic;
        }
    }
}