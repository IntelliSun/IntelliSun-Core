using System;

namespace IntelliSun.Aim
{
    internal class LambdaIntentInvokation<T> : IIntentInvokation<T>
    {
        private readonly Action<T> callback;

        public LambdaIntentInvokation(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            this.callback = callback;
        }

        public void Invoke(T instance)
        {
            this.callback(instance);
        }
    }
}