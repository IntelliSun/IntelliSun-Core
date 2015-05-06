using System;

namespace IntelliSun.Aim
{
    public class LambdaIntent<T> : IIntent<T>
    {
        private readonly Action<T> callback;
        private readonly IIntentSite site;
        private readonly IntentPriority priority;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public LambdaIntent(Action<T> callback, IIntentSite site, IntentPriority priority)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            if (site == null)
                throw new ArgumentNullException("site");

            this.site = site;
            this.callback = callback;
            this.priority = priority;
        }

        public void Invoke(T instance)
        {
            this.callback(instance);
        }

        public IIntentSite Site
        {
            get { return this.site; }
        }

        public IntentPriority Priority
        {
            get { return this.priority; }
        }
    }
}