using System;

namespace IntelliSun.Aim
{
    public interface IIntentSetSitePriority<in T>
    {
        IIntentSetPriority<T> Site(IIntentSite site);
        IIntentSetSite<T> Priority(IntentPriority priority);
    }
}