using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal class ReflectMethodInfo : ReflectMemberInfo
    {
        private readonly MethodInfo methodInfo;

        public ReflectMethodInfo(MethodInfo methodInfo)
            : base(methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        public override Type ResourceType
        {
            get { return this.methodInfo.ReturnType; }
        }
    }
}