using System;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.ComponentModel
{
    public interface IExtensionRegistry : IServiceProvider
    {
        event EventHandler<ExtensionRegistryEventArgs> ExtensionAdded;

        IEnumerable<object> GetExtensions(Type extensionType);
        void AddExtension(Type extensionType, object extension);
    }

    public class ExtensionRegistryEventArgs : EventArgs
    {
        private readonly Type extensionType;

        public ExtensionRegistryEventArgs(Type extensionType)
        {
            this.extensionType = extensionType;
        }

        public Type ExtensionType
        {
            get { return this.extensionType; }
        }
    }

    public sealed class ExtensionRegistry : IExtensionRegistry
    {
        private readonly GroupedList<Type, object> extensions;

        public event EventHandler<ExtensionRegistryEventArgs> ExtensionAdded;

        public ExtensionRegistry()
        {
            this.extensions = new GroupedList<Type, object>();
        }

        public IEnumerable<object> GetExtensions(Type extensionType)
        {
            if (!this.extensions.ContainsKey(extensionType))
                return new object[0];

            return this.extensions[extensionType];
        }

        public void AddExtension(Type extensionType, object extension)
        {
            this.extensions.Add(extensionType).Add(extension);

            this.OnExtensionAdded(new ExtensionRegistryEventArgs(extensionType));
        }

        private void OnExtensionAdded(ExtensionRegistryEventArgs args)
        {
            var handler = this.ExtensionAdded;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        object IServiceProvider.GetService(Type serviceType)
        {
            return this.GetExtensions(serviceType).SingleOrDefault();
        }
    }

    public static class ExtensionRegistryExtensions
    {
        public static void AddExtension<T>(this IExtensionRegistry extensionRegistry, T extension)
        {
            extensionRegistry.AddExtension(typeof(T), extension);
        }

        public static IEnumerable<T> GetExtensions<T>(this IExtensionRegistry extensionRegistry)
        {
            return extensionRegistry.GetExtensions(typeof(T)).OfType<T>();
        }
    }
}
