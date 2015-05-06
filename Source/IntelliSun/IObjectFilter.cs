using System;

namespace IntelliSun
{
    public interface IObjectFilter<in T>
    {
        bool Check(T value);
    }
}