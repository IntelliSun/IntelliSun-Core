using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    public interface IReflectInfo : ICustomAttributeProvider
    {
        string Name { get; }
        Type ResourceType { get; }

        object ReflectionObject { get; }
    }
}
