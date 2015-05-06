using System;

namespace IntelliSun.Logging
{
    public abstract class LoggerBase : ILogger
    {
        private readonly Type containerType;

        protected LoggerBase(Type containerType)
        {
            this.LogTemplate = "{{msg}}";
            this.containerType = containerType;
        }

        protected abstract void LogMessage(string message, string category, LogLevel logLevel);
        protected abstract void LogMessage(string message, Exception exception, string category, LogLevel logLevel);

        public virtual void Log(string message, string category, LogLevel level)
        {
            this.LogMessage(message, category, level);
        }

        public virtual void Log(string message, Exception exception, string category, LogLevel level)
        {
            if (exception == null)
                this.Log(message, category, level);
            else
                this.LogMessage(message, exception, category, level);
        }

        public void Log(LogText message, string category, LogLevel level)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, category, level);
        }

        public void Log(LogText message, Exception exception, string category, LogLevel level)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, exception, category, level);
        }

        public void Log(string message, LogCategory category, LogLevel level)
        {
            this.Log(message, category.ToString(), level);
        }

        public void Log(string message, Exception exception, LogCategory category, LogLevel level)
        {
            this.Log(message, exception, category.ToString(), level);
        }

        public void Log(string message, LogCategory category)
        {
            this.Log(message, category.ToString(), LevelForCategory(category));
        }

        public void Log(string message, Exception exception)
        {
            this.Log(message, exception, LogCategory.Exception, LogLevel.Error);
        }

        public void Log(string message, Exception exception, LogLevel level)
        {
            this.Log(message, exception, LogCategory.Exception, level);
        }

        public void Log(LogText message, LogCategory category, LogLevel level)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, category, level);
        }

        public void Log(LogText message, Exception exception, LogCategory category, LogLevel level)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, exception, category, level);
        }

        public void Log(LogText message, Exception exception)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, exception);
        }

        public void Log(LogText message, LogCategory category)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, category);
        }

        public void Log(LogText message, Exception exception, LogLevel level)
        {
            var messageText = message.Resolve(this);
            this.Log(messageText, exception, level);
        }

        protected string FormatMessage(string message, Exception exception, string category, LogLevel logLevel)
        {
            return LogFormatter.Format(this.LogTemplate, this, message, exception, category, logLevel);
        }

        private static LogLevel LevelForCategory(LogCategory category)
        {
            switch (category)
            {
                case LogCategory.Debug:
                    return LogLevel.Debug;
                case LogCategory.Trace:
                    return LogLevel.Info;
                case LogCategory.Exception:
                    return LogLevel.Error;
                case LogCategory.Initialization:
                    return LogLevel.Debug;
                case LogCategory.Construction:
                    return LogLevel.Debug;
                case LogCategory.Finalization:
                    return LogLevel.Debug;
                case LogCategory.Disposing:
                    return LogLevel.Debug;
                default:
                    throw new ArgumentOutOfRangeException("category");
            }
        }

        public Type ContainerType
        {
            get { return this.containerType; }
        }

        public string LogTemplate { get; set; }
    }
}