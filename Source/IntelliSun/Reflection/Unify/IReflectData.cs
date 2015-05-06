using System;

namespace IntelliSun.Reflection.Unify
{
    public interface IReflectData<out T>
    {
        T GetData(IReflectInfo reflectInfo);
    }
}