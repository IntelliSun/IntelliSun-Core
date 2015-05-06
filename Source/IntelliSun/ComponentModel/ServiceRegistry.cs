using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IntelliSun.ComponentModel
{
    public sealed class ServiceRegistry : IServiceRegistry
    {
        private readonly Dictionary<Type, object> registry;

        public ServiceRegistry()
        {
            this.registry = new Dictionary<Type, object>();
        }

        public T GetService<T>()
        {
            return this.GetService<T>(typeof(T));
        }

        public T GetService<T>(Type serviceType)
        {
            var service = this.GetService(serviceType);
            if (service == null)
                return default(T);

            if (!(service is T))
                return default(T);

            var cast = (T)service;
            return cast;
        }

        public object GetService(Type serviceType)
        {
            if (this.registry.ContainsKey(serviceType))
                return registry[serviceType];
            
            return null;
        }

        public object SetService(string key)
        {
            return this.GetServiceByKey(key).Value;
        }

        private KeyValuePair<Type, object> GetServiceByKey(string key)
        {
            var services = this.GetServicesByKey(key);
            return (services.Length == 1) ? services[0] : 
                new KeyValuePair<Type, object>(null, null);
        }

        private KeyValuePair<Type, object>[] GetServicesByKey(string key)
        {
            var query = from data in registry
                        let service = data.Value
                        let isService = service is IIdentifiableService
                        where (isService) ? ((IIdentifiableService)service)
                            .ServiceKey.Equals(key,
                            StringComparison.OrdinalIgnoreCase) :
                            service.ToString().Equals(key,
                            StringComparison.OrdinalIgnoreCase)
                        select data;

            var arr = query.ToArray();
            return arr;
        }

        void IServiceRegistry.AddService(Type type, object service)
        {
            this.RegisterService(type, service);
        }

        public bool RegisterService(object service)
        {
            return this.RegisterService(service.GetType(), service);
        }

        public bool RegisterService(IIdentifiableService service)
        {
            return this.RegisterService(service.GetType(), service);
        }

        public bool RegisterService<T>(T instance)
        {
            return this.RegisterService(typeof(T), instance);
        }

        public bool RegisterService(Type serviceType, object service)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType", "${Resources.ServiceTypeCannotBeNull}");

            if (service == null)
                throw new ArgumentNullException("service", "${Resources.ServiceProviderCannotBeNull}");

            if (this.registry.ContainsKey(serviceType))
                throw new ArgumentException("${Resources.ServiceAlreadyPresent}" + serviceType, "service");

            if (serviceType.IsInstanceOfType(service))
            {
                this.registry.Add(serviceType, service);
                this.OnServiceAdded(new ServiceEventArgs(serviceType, service));
            } else
                //${Resources.ServiceMustBeAssignable}
                throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "{0}: {1}",
                    service.GetType().FullName, serviceType.FullName));

            return true;
        }

        public bool RemoveService(Type type)
        {
            if (!this.registry.ContainsKey(type))
                return false;

            var res = this.registry.Remove(type);
            if (res)
                this.OnServiceRemoved(new ServiceEventArgs(type, type));

            return res;
        }

        public bool HasService(Type type)
        {
            return this.registry.ContainsKey(type) && this.registry[type] != null;
        }

        public bool HasService<T>()
        {
            return this.HasService(typeof(T));
        }

        public bool RemoveService(string key)
        {
            var service = this.GetServiceByKey(key);
            return service.Value != null && this.RemoveService(service.Key);
        }

        public bool RemoveService<T>()
        {
            return this.RemoveService(typeof(T));
        }

        private void OnServiceAdded(ServiceEventArgs serviceEventArgs)
        {
            if (this.ServiceAdded != null)
                this.ServiceAdded(this, serviceEventArgs);
        }

        private void OnServiceRemoved(ServiceEventArgs serviceEventArgs)
        {
            if (this.ServiceRemoved != null)
                this.ServiceRemoved(this, serviceEventArgs);
        }

        public event EventHandler<ServiceEventArgs> ServiceAdded;
        public event EventHandler<ServiceEventArgs> ServiceRemoved;
    }
}
