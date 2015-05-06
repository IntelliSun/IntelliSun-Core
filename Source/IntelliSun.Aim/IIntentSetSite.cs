using System;

namespace IntelliSun.Aim
{
    public interface IIntentSetSite<in T>
    {
        IIntent<T> Site(IIntentSite site);
    }
}