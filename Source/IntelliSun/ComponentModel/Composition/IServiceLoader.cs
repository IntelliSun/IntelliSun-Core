using System;

namespace IntelliSun.ComponentModel.Composition
{
    public interface IServiceLoader
    {
        void Load<T>(T service);
        void Load(Type serviceType, object service);
    }
}
