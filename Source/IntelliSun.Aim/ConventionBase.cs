using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Aim
{
    public abstract class ConventionBase<T> : IConvention<T>
    {
        public IIntent<T>[] QueryIntents(T instance)
        {
            var builder = new IntentBuilder<T>();
            return this.GetIntents(instance, builder).ToArray();
        }

        protected abstract IEnumerable<IIntent<T>> GetIntents(T instance, IntentBuilder<T> root);
    }
}