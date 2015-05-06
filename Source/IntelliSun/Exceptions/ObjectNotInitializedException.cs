using System;

namespace IntelliSun
{
    [Serializable]
    public class ObjectNotInitializedException : InvalidOperationException
    {
        public ObjectNotInitializedException()
            : base("${Resources.ObjectNotInitialized}")
        {

        }

        public ObjectNotInitializedException(string message)
            : base(message)
        {

        }

        public ObjectNotInitializedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
