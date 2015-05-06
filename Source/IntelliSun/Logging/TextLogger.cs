using System;
using System.IO;

namespace IntelliSun.Logging
{
    public class TextLogger : LoggerBase
    {
        private readonly TextWriter stream;

        public TextLogger(Type containerType, TextWriter stream)
            : base(containerType)
        {
            this.stream = stream;
        }

        protected override void LogMessage(string message, string category, LogLevel logLevel)
        {
            var formated = this.FormatMessage(message, null, category, logLevel);
            this.LogMessage(formated);
        }

        protected override void LogMessage(string message, Exception exception, string category, LogLevel logLevel)
        {
            var formated = this.FormatMessage(message, exception, category, logLevel);
            this.LogMessage(formated, exception);
        }

        protected void LogMessage(string message)
        {
            stream.WriteLine(message);
        }

        protected void LogMessage(string message, Exception exception)
        {
            stream.WriteLine(message);
            stream.WriteLine(exception);
        }
    }
}
