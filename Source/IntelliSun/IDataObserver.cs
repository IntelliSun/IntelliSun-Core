using System;

namespace IntelliSun
{
    public interface IDataObserver
    {
        void SendData(object data);
    }
}