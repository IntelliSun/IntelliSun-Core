using System;
using System.Runtime.Serialization;

namespace IntelliSun.ComponentModel.Composition
{
    [Serializable]
    public class ServiceLocationException : Exception
    {
        public ServiceLocationException()
        {
        }

        public ServiceLocationException(string message)
            : base(message)
        {
        }

        public ServiceLocationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ServiceLocationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}