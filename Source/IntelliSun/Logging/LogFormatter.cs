using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using IntelliSun.Helpers;

namespace IntelliSun.Logging
{
    public static class LogFormatter
    {
        public static string Format(string template, ILogger logger, string message, Exception exception, string category, LogLevel level)
        {
            return StringHelper.TokenFormat(template, new Dictionary<string, string> {
                { "time", DateTime.Now.ToLongTimeString() },
                { "date", DateTime.Now.ToLongDateString() },
                { "type", logger.ContainerType.Name },
                { "typef", logger.ContainerType.FullName },
                { "prcc", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture) },
                { "ticks", Environment.TickCount.ToString(CultureInfo.InvariantCulture) },
                { "msg", message },
                { "ctr", category },
                { "lvl", level.ToString() }
            }, "{{{0}}}");
        }
    }
}
