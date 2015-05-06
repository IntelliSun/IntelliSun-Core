using System;

namespace IntelliSun
{
    public interface INotifyDisposing
    {
        event EventHandler<EventArgs> Disposing;
    }
}