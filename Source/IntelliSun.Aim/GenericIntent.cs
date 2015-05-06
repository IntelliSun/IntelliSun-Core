using System;

namespace IntelliSun.Aim
{
    internal class GenericIntent<T> : IIntent<T>
    {
        private readonly IIntentInvokation<T> invokation;
        private readonly IIntentSite site;
        private readonly IntentPriority priority;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public GenericIntent(IIntentInvokation<T> invokation, IIntentSite site, IntentPriority priority)
        {
            if (invokation == null)
                throw new ArgumentNullException("invokation");

            if (site == null)
                throw new ArgumentNullException("site");

            this.invokation = invokation;
            this.site = site;
            this.priority = priority;
        }

        public void Invoke(T instance)
        {
            this.invokation.Invoke(instance);
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