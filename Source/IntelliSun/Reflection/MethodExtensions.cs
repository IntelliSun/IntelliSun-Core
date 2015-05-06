using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IntelliSun.Reflection
{
    public static class MethodExtensions
    {
        public static bool IsAsync(this MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(AsyncStateMachineAttribute), true);
        }

        public static Type[] GetParametersTypes(this MethodBase method)
        {
            return method.GetParameters().Select(pi => pi.ParameterType).ToArray();
        }
    }
}
