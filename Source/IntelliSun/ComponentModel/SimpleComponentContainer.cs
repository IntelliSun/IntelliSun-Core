using System;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.ComponentModel
{
    public class SimpleComponentContainer : Unmanaged, IComponentContainer
    {
        private readonly DisposeHandler disposeHandler;
        private readonly GroupedList<Type, object> components;

        public SimpleComponentContainer()
        {
            this.disposeHandler = new DisposeHandler();
            this.components = new GroupedList<Type, object>();
        }

        public void AddComponent<TComponent>(TComponent component)
            where TComponent : class
        {
            if (component == null)
                throw new ArgumentNullException("component");

            var componentType = typeof(TComponent);
            this.components.Add(componentType, component);

            this.LogComponent(component);
        }

        private void LogComponent(object component)
        {
            var disposable = component as IDisposable;
            if (disposable != null)
                this.disposeHandler.RegisterChild(disposable);
        }

        public TComponent GetComponent<TComponent>() 
            where TComponent : class
        {
            return (TComponent)this.GetComponent(typeof(TComponent));
        }

        public TComponent[] GetComponents<TComponent>()
            where TComponent : class
        {
            var sourceComponents = this.GetComponents(typeof(TComponent));
            var resultEnum = sourceComponents.OfType<TComponent>();

            return resultEnum.ToArray();
        }

        public object GetComponent(Type componentType)
        {
            var componentsSet = this.GetComponentsCore(componentType);
            if (componentsSet == null)
                throw new ArgumentException("${Resources.NoSuchComponentRegistered}", "componentType");

            return componentsSet.First();
        }

        public object[] GetComponents(Type componentsType)
        {
            var componentsSet = this.GetComponentsCore(componentsType);
            if(componentsSet == null) 
                throw new ArgumentException("${Resources.NoSuchComponentRegistered}", "componentsType");

            return componentsSet.ToArray();
        }

        private IEnumerable<object> GetComponentsCore(Type componentsType)
        {
            if (!this.components.ContainsKey(componentsType))
                return null;
                
            var componentsSet = this.components[componentsType];
            return componentsSet.Count == 0 ? null : componentsSet;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposeHandler != null)
                this.disposeHandler.DisposeIfAlive();

            this.components.Clear();
        }
    }
}