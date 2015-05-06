using System;

namespace IntelliSun.ComponentModel
{
    public interface IServiceConsumer
    {
        IServiceProvider ServiceProvider { get; }
    }
}
