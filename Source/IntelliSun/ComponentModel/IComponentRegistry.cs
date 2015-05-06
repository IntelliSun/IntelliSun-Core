using System;

namespace IntelliSun.ComponentModel
{
    public interface IComponentRegistry
    {
        void AddComponent<TComponent>(TComponent component)
            where TComponent : class;
    }
}
