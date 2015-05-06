using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class MethodInfoExtender : MemberInfoExtender
    {
        private readonly MethodInfo methodInfo;

        public MethodInfoExtender(MethodInfo methodInfo)
            : this(methodInfo, true)
        {
        }

        public MethodInfoExtender(MethodInfo methodInfo, bool loadInheritedData)
            : base(methodInfo, loadInheritedData)
        {
            this.methodInfo = methodInfo;
        }

        public MethodInfo MethodInfo
        {
            get { return this.methodInfo; }
        }
    }
}