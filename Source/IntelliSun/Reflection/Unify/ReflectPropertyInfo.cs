using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal class ReflectPropertyInfo : ReflectMemberInfo
    {
        private readonly PropertyInfo propertyInfo;

        public ReflectPropertyInfo(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        public override Type ResourceType
        {
            get { return this.propertyInfo.PropertyType; }
        }
    }
}