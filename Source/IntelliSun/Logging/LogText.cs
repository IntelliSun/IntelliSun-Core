using System;
using System.Collections.Generic;
using IntelliSun.Helpers;

namespace IntelliSun.Logging
{
    public class LogText
    {
        private readonly string message;

        public LogText(string message)
        {
            this.message = message;
        }

        public string Resolve(ILogger logger)
        {
            return StringHelper.TokenFormat(message, new Dictionary<string, string> {
                { "t", logger.ContainerType.Name },
                { "q", logger.ContainerType.FullName }
            });
        }

        public string Message
        {
            get { return this.message; }
        }
    }
}
