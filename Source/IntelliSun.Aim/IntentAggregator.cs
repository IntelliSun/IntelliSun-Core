using System;
using IntelliSun.Collections;

namespace IntelliSun.Aim
{
    public class IntentAggregator<T> : IIntentAggregator<T>
    {
        public IntentCollection<T> Aggregate(IIntent<T>[] intents)
        {
            var collection = new SortedIntentCollection<T>();
            intents.Invoke(collection.AddIntent);

            return new IntentCollection<T>(collection.GetHighest());
        }
    }
}