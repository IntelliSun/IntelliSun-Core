using System;

namespace IntelliSun.ComponentModel
{
    public interface IInstanceProvider<out T>
    {
        T Instance { get; }
    }
}
