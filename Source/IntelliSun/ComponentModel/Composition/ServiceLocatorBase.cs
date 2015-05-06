using System;
using System.Collections.Generic;
using System.Globalization;
using IntelliSun.Collections;

namespace IntelliSun.ComponentModel.Composition
{
    public abstract class ServiceLocatorBase : IServiceLocator
    {
        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        public object GetService(Type serviceType)
        {
            return this.GetService(serviceType, null);
        }

        public object GetService(Type serviceType, string key)
        {
            try
            {
                return this.GetServiceImpl(serviceType, key);
            } catch (Exception ex)
            {
                var message = this.ServiceLocationException(serviceType, key);
                throw new ServiceLocationException(message, ex);
            }
        }

        public IEnumerable<TService> GetServices<TService>()
        {
            return this.GetServices(typeof(TService)).CastOrSkip<TService>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.GetServicesImpl(serviceType);
            } catch (Exception ex)
            {
                var message = this.ServicesLocationException(serviceType);
                throw new ServiceLocationException(message, ex);
            }
        }

        public TService GetService<TService>()
        {
            return this.GetService<TService>(null);
        }

        public TService GetService<TService>(string key)
        {
            return (TService)this.GetService(typeof(TService), key);
        }

        protected string ServicesLocationException(Type serviceType)
        {
            //${Resources.ServicesLocationException}
            const string message = @"ServicesLocationException: {0}";

            var cultureInfo = CultureInfo.CurrentCulture;
            return String.Format(cultureInfo, message, serviceType.Name);
        }

        protected string ServiceLocationException(Type serviceType, string key)
        {
            //${Resources.ServiceLocationException}
            const string message = @"ServiceLocationException: {0}, {1}";

            var cultureInfo = CultureInfo.CurrentCulture;
            return String.Format(cultureInfo, message, serviceType.Name, key);
        }

        protected abstract IEnumerable<object> GetServicesImpl(Type serviceType);
        protected abstract object GetServiceImpl(Type serviceType, string key);

        public abstract bool HasService(Type serviceType);
    }
}