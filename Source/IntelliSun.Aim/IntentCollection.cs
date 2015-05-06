using System;
using System.Collections.Generic;
using IntelliSun.Collections;

namespace IntelliSun.Aim
{
    public sealed class IntentCollection<T> : ImmutableCollection<IIntent<T>>
    {
        public IntentCollection()
        {
        }

        public IntentCollection(params IIntent<T>[] items)
            : base(items)
        {
        }

        public IntentCollection(IEnumerable<IIntent<T>> items)
            : base(items)
        {
        }

        public void InvokeAll(T instance)
        {
            this.Invoke(intent => intent.Invoke(instance));
        }
    }
}