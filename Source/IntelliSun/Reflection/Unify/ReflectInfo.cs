using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal abstract class ReflectInfo : IReflectInfo
    {
        private readonly ICustomAttributeProvider source;

        protected ReflectInfo(ICustomAttributeProvider source)
        {
            this.source = source;
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.source.GetCustomAttributes(attributeType, inherit);
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return this.source.GetCustomAttributes(inherit);
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return this.source.IsDefined(attributeType, inherit);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.ReflectionObject != null
                ? this.ReflectionObject.ToString()
                : base.ToString();
        }

        public abstract string Name { get; }
        public abstract Type ResourceType { get; }

        public abstract object ReflectionObject { get; }

    }
}