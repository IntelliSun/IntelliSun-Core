using System;
using System.Linq.Expressions;
using IntelliSun.ComponentModel;
using IntelliSun.Expressions;

namespace IntelliSun.Aim
{
    public class IntentBuilder<T>
    {
        private readonly IndexProvider indexer;

        public IntentBuilder()
        {
            this.indexer = IndexProvider.GetProvider(this);
        }

        public IIntentSetSitePriority<T> Intent(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            var invk = new LambdaIntentInvokation<T>(callback);
            return new SetSitePriority(invk);
        }

        public IIntentSetPriority<T> Intent(Action<T> callback, string siteKey)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            var invk = new LambdaIntentInvokation<T>(callback);
            return ((IIntentSetSitePriority<T>)new SetSitePriority(invk))
                .Site(new StringIntentSite(siteKey));
        }

        public IIntentSetSite<T> Intent(Action<T> callback, IntentPriority priority)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            var invk = new LambdaIntentInvokation<T>(callback);
            return ((IIntentSetSitePriority<T>)new SetSitePriority(invk))
                .Priority(priority);
        }

        public IIntentSetPriority<T> Unique(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            var invk = new LambdaIntentInvokation<T>(callback);
            return ((IIntentSetSitePriority<T>)new SetSitePriority(invk))
                .Site(new StringIntentSite(String.Format("UNIQUE+#{0}", this.indexer.GetIndex())));
        }

        public IIntentSetPriority<T> SetProperty<TProp>(Expression<Func<T, TProp>> property, TProp value)
        {
            var propertyInfo = property.GetPropertyInfo();

            var invk = new SetPropertyInvokation<T>(propertyInfo, value);
            return ((IIntentSetSitePriority<T>)new SetSitePriority(invk))
                .Site(new StringIntentSite(String.Format("SET+PROPERTY+{0}", propertyInfo.Name)));
        }

        public IIntentSetPriority<T> CallMethod(Expression<Action<T>> method)
        {
            var methodInfo = method.GetMethodInfo();
            var lambda = method.Compile();

            var invk = new LambdaIntentInvokation<T>(lambda);
            return ((IIntentSetSitePriority<T>)new SetSitePriority(invk))
                .Site(new StringIntentSite(String.Format("CALL+METHOD+{0}", methodInfo.Name)));
        }

        private class SetSitePriority : IIntentSetSitePriority<T>, IIntentSetSite<T>, IIntentSetPriority<T>
        {
            private readonly IIntentInvokation<T> invokation;

            private IIntentSite intentSite;
            private IntentPriority intentPriority;

            public SetSitePriority(IIntentInvokation<T> invokation)
            {
                this.invokation = invokation;
            }

            IIntentSetPriority<T> IIntentSetSitePriority<T>.Site(IIntentSite site)
            {
                if (site == null)
                    throw new ArgumentNullException("site");

                this.intentSite = site;
                return this;
            }

            IIntentSetSite<T> IIntentSetSitePriority<T>.Priority(IntentPriority priority)
            {
                this.intentPriority = priority;
                return this;
            }

            IIntent<T> IIntentSetSite<T>.Site(IIntentSite site)
            {
                return new GenericIntent<T>(this.invokation, site, this.intentPriority);
            }

            IIntent<T> IIntentSetPriority<T>.Priority(IntentPriority priority)
            {
                return new GenericIntent<T>(this.invokation, this.intentSite, priority);
            }
        }
    }
}