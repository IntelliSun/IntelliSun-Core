using System;

namespace IntelliSun.Aim
{
    public interface IIntent<in T> : IHasIntentPriority
    {
        void Invoke(T instance);

        IIntentSite Site { get; }
    }
}