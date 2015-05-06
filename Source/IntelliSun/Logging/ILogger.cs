using System;

namespace IntelliSun.Logging
{
    public interface ILogger
    {
        void Log(string message, string category, LogLevel level);
        void Log(string message, Exception exception, string category, LogLevel level);

        Type ContainerType { get; }
    }
}
