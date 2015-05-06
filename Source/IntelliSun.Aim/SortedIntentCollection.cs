using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Aim
{
    internal class SortedIntentCollection<T>
    {
        private readonly Dictionary<IIntentSite, SortedSet<IIntent<T>>> intents;

        public SortedIntentCollection()
        {
            this.intents = new Dictionary<IIntentSite, SortedSet<IIntent<T>>>();
        }

        public void AddIntent(IIntent<T> intent)
        {
            if (intent == null)
                throw new ArgumentNullException("intent");

            var list = this.GetIntentList(intent);
            list.Add(intent);
        }

        public IIntent<T>[] GetHighest()
        {
            return this.intents.Select(arg => arg.Value.First()).ToArray();
        }

        private SortedSet<IIntent<T>> GetIntentList(IIntent<T> intent)
        {
            var site = intent.Site;
            if (!this.intents.ContainsKey(site))
                this.intents.Add(site, new SortedSet<IIntent<T>>(new HasIntentPriorityComparer()));

            return this.intents[site];
        }
    }
}