using System;

namespace IntelliSun
{
    public abstract class DataObserver<T> : IDataObserver<T>
    {
        public abstract void SendData(T data);

        void IDataObserver.SendData(object data)
        {
            this.SendData((T)data);
        }
    }
}