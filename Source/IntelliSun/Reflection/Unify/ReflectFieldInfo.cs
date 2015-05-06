using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal class ReflectFieldInfo : ReflectMemberInfo
    {
        private readonly FieldInfo fieldInfo;

        public ReflectFieldInfo(FieldInfo fieldInfo)
            : base(fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }

        public override Type ResourceType
        {
            get { return this.fieldInfo.FieldType; }
        }
    }
}