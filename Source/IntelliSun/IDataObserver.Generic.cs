using System;

namespace IntelliSun
{
    public interface IDataObserver<in T> : IDataObserver
    {
        void SendData(T data);
    }
}
