using System;
using System.Collections.Generic;

namespace IntelliSun.Reflection
{
    public static class RuntimeInjector
    {
        public static void Inject(object instance, IEnumerable<RuntimePropertySetter> setters)
        {
            var type = instance.GetType();
            foreach (var runtimePropertySetter in setters)
            {
                var property = type.GetProperty(runtimePropertySetter.Property);
                if (property == null)
                    continue;

                property.SetValue(instance, runtimePropertySetter.Value, new object[0]);
            }
        }
    }
}
