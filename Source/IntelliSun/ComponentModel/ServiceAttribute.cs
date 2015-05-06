using System;

namespace IntelliSun.ComponentModel
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ServiceAttribute : Attribute
    {
        private readonly Type serviceType;

        public ServiceAttribute(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public Type ServiceType
        {
            get { return this.serviceType; }
        }
    }
}
