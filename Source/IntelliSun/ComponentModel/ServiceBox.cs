using System;

namespace IntelliSun.ComponentModel
{
    public struct ServiceBox
    {
        private readonly object service;
        private readonly Type serviceType;

        public ServiceBox(object service)
        {
            this.service = service;
            this.serviceType = service.GetType();
        }

        public ServiceBox(object service, Type serviceType)
        {
            this.service = service;
            this.serviceType = serviceType;
        }

        public object Service
        {
            get { return this.service; }
        }

        public Type ServiceType
        {
            get { return this.serviceType; }
        }
    }
}
