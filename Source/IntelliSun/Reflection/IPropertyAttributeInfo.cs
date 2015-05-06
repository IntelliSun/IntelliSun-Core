using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public interface IPropertyAttributeInfo<out T> : IPropertyAttributeInfo
        where T : Attribute
    {
        new T[] Attributes { get; }
    }

    public interface IPropertyAttributeInfo
    {
        PropertyInfo PropertyInfo { get; }
        Attribute[] Attributes { get; }
    }
}