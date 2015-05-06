using System;
using IntelliSun.Reflection;

namespace IntelliSun.ComponentModel.Composition
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExtensionAssemblyAttribute : Attribute
    {
        private readonly Type extensionType;
        private readonly Type providerType;

        public ExtensionAssemblyAttribute(Type extensionType, Type providerType)
        {
            this.extensionType = extensionType;

            if (!providerType.IsImplementingInterface(typeof(IExtensionProvider)))
                throw new ArgumentException("${Resources.InvalidExtensionProvider}");

            this.providerType = providerType;
        }

        public Type ExtensionType
        {
            get { return this.extensionType; }
        }

        public Type ProviderType
        {
            get { return this.providerType; }
        }
    }
}
