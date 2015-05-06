using System;

namespace IntelliSun.Aim
{
    public interface IIntentSetPriority<in T>
    {
        IIntent<T> Priority(IntentPriority priority);
    }
}