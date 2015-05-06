using System;

namespace IntelliSun.ComponentModel.Composition
{
    public static class ExtensionProviderExtensions
    {
        public static IExtensionProvider GetProviderInstance(this ExtensionAssemblyAttribute attribute)
        {
            var providerType = attribute.ProviderType;
            return (IExtensionProvider)Activator.CreateInstance(providerType);
        }
    }
}