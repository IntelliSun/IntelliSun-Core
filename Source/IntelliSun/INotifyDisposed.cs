using System;

namespace IntelliSun
{
    public interface INotifyDisposed
    {
        event EventHandler<EventArgs> Disposed;
    }
}