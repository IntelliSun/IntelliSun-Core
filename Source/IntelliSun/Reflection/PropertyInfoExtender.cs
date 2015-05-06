using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class PropertyInfoExtender : MemberInfoExtender
    {
        private readonly PropertyInfo propertyInfo;

        public PropertyInfoExtender(PropertyInfo propertyInfo)
            : this(propertyInfo, true)
        {
        }

        public PropertyInfoExtender(PropertyInfo propertyInfo, bool loadInheritedData)
            : base(propertyInfo, loadInheritedData)
        {
            this.propertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo
        {
            get { return this.propertyInfo; }
        }
    }
}