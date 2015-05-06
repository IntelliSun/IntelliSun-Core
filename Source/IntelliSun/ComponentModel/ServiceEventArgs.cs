using System;

namespace IntelliSun.ComponentModel
{
    public class ServiceEventArgs : EventArgs
    {
        public ServiceEventArgs(Type serviceType, object serviceInstance)
        {
            this.ServiceType = serviceType;
            this.Instance = serviceInstance;
        }

        public Type ServiceType { get; set; }
        public object Instance { get; set; }
    }
}
