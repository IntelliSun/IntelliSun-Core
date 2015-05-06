using System;

namespace IntelliSun.Aim
{
    public interface IConvention<in T>
    {
        IIntent<T>[] QueryIntents(T instance);
    }
}