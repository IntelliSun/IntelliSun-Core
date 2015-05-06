using System;

namespace IntelliSun
{
    public interface IObservable
    {
        void AddObserver(IDataObserver observer);
    }
}
