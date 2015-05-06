using System;

namespace IntelliSun.ComponentModel
{
    public interface IComponentContainer : IComponentRegistry, IDisposable
    {
        TComponent GetComponent<TComponent>()
            where TComponent : class;

        TComponent[] GetComponents<TComponent>()
            where TComponent : class;

        object GetComponent(Type componentType);
        object[] GetComponents(Type componentsType);
    }
}