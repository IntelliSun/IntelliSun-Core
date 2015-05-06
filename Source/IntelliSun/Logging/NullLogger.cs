using System;

namespace IntelliSun.Logging
{
    public class NullLogger : LoggerBase
    {
        public NullLogger(Type containerType)
            : base(containerType)
        {
        }

        protected override void LogMessage(string message, string category, LogLevel logLevel)
        {
            
        }

        protected override void LogMessage(string message, Exception exception, string category, LogLevel logLevel)
        {
            
        }
    }
}
