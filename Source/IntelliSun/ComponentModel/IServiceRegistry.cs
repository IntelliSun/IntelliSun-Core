using System;

namespace IntelliSun.ComponentModel
{
    public interface IServiceRegistry : IServiceProvider
    {
        event EventHandler<ServiceEventArgs> ServiceAdded;
        event EventHandler<ServiceEventArgs> ServiceRemoved;

        void AddService(Type type, object service);
        bool RemoveService(Type type);
        bool HasService(Type type);
    }
}
