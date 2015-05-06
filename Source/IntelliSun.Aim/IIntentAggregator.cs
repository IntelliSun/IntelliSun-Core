using System;

namespace IntelliSun.Aim
{
    public interface IIntentAggregator<T>
    {
        IntentCollection<T> Aggregate(IIntent<T>[] intents);
    }
}