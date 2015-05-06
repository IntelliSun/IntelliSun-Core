using System;

namespace IntelliSun.Aim
{
    internal interface IIntentInvokation<in T>
    {
        void Invoke(T instance);
    }
}