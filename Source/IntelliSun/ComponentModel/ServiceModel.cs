using System;
using IntelliSun.Reflection;

namespace IntelliSun.ComponentModel
{
    public static class ServiceModel
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            var serviceType = typeof(TService);
            var service = serviceProvider.GetService(serviceType);

            return (TService)service;
        }

        public static void GetService<TService>(this IServiceProvider serviceProvider, out TService result)
        {
            result = GetService<TService>(serviceProvider);
        }

        public static void AddService(this IServiceRegistry serviceRegistry, object service)
        {
            var serviceType = service.GetType();
            var info = new TypeInfoExtender(serviceType);
            if (info.HasAttribute<ServiceAttribute>())
                serviceType = info.GetAttributeValue((ServiceAttribute a) => a.ServiceType);

            serviceRegistry.AddService(serviceType, service);
        }
    }
}
