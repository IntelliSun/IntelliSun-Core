using System;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class PropertyAttributeInfo<T> : IPropertyAttributeInfo<T>
        where T : Attribute
    {
        public PropertyAttributeInfo()
        {

        }

        public PropertyAttributeInfo(PropertyInfo propertyInfo, T[] attributes)
        {
            this.Attributes = attributes;
            this.PropertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo { get; set; }
        public T[] Attributes { get; set; }

        Attribute[] IPropertyAttributeInfo.Attributes
        {
            get { return this.Attributes.Cast<Attribute>().ToArray(); }
        }
    }

    public class PropertyAttributeInfo : IPropertyAttributeInfo
    {
        public PropertyAttributeInfo()
        {
            
        }

        public PropertyAttributeInfo(PropertyInfo propertyInfo, Attribute[] attributes)
        {
            this.Attributes = attributes;
            this.PropertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo { get; set; }
        public Attribute[] Attributes { get; set; }
    }
}