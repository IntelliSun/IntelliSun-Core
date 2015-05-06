using System;
using System.Collections.Generic;

namespace IntelliSun
{
    public static class InstanceManager
    {
        public static InstanceManager<TIn, TOut> Create<TIn, TOut>(Func<TIn, TOut> factory)
        {
            return new InstanceManager<TIn, TOut>(factory);
        }
    }

    public class InstanceManager<TIn, TOut>
    {
        private readonly Func<TIn, TOut> factory;
        private readonly IDictionary<TIn, TOut> instances;

        public InstanceManager(Func<TIn, TOut> factory)
        {
            this.factory = factory;
            this.instances = new Dictionary<TIn, TOut>();
        }

        public TOut InstanceFor(TIn value)
        {
            var key = this.KeyTransformer(value);
            if (!this.instances.ContainsKey(key))
                this.instances.Add(key, this.factory(value));

            return this.instances[key];
        }

        protected virtual TIn KeyTransformer(TIn key)
        {
            return key;
        }

        public void Override(TIn key, TOut instance)
        {
            key = this.KeyTransformer(key);
            this.instances[key] = instance;
        }
    }
}
