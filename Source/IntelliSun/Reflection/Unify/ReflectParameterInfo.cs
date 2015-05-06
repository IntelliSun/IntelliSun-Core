using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal class ReflectParameterInfo : ReflectInfo
    {
        private readonly ParameterInfo parameterInfo;

        public ReflectParameterInfo(ParameterInfo parameterInfo)
            : base(parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }

        public override string Name
        {
            get { return this.parameterInfo.Name; }
        }

        public override Type ResourceType
        {
            get { return this.parameterInfo.ParameterType; }
        }

        public override object ReflectionObject
        {
            get { return this.parameterInfo; }
        }
    }
}