using System;
using System.Reflection;

namespace IntelliSun.Aim
{
    internal class SetPropertyInvokation<T> : IIntentInvokation<T>
    {
        private readonly PropertyInfo property;
        private readonly object value;

        public SetPropertyInvokation(PropertyInfo property, object value)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            this.property = property;
            this.value = value;
        }

        public void Invoke(T instance)
        {
            var isStatic = this.property.SetMethod.IsStatic;
            this.property.SetValue(isStatic ? null : (object)instance, this.value);
        }
    }
}