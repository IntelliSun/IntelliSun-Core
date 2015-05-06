using System;

namespace IntelliSun.ComponentModel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class ServiceProviderAttribute : Attribute
    {
        private readonly string propertyName;

        public ServiceProviderAttribute()
        {
            
        }

        public ServiceProviderAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return this.propertyName; }
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ServiceAddTargetAttribute : Attribute
    {
        private readonly string methodName;

        public ServiceAddTargetAttribute()
        {
            
        }

        public ServiceAddTargetAttribute(string methodName)
        {
            this.methodName = methodName;
        }

        public string MethodName
        {
            get { return this.methodName; }
        }
    }
}
