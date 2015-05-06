using System;

namespace IntelliSun.Logging
{
    public class BasicDebugLogger : TextLogger
    {
        public BasicDebugLogger(Type containerType)
            : base(containerType, DebugTextWriter.Instance)
        {

        }
    }
}
