using System;

namespace IntelliSun
{
    public interface IFormatter<in T> : IFormatter
    {
        string Format(T obj);
    }
}

