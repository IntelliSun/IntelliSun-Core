using System;
using System.Collections;
using System.Collections.Generic;

namespace IntelliSun.Reflection
{
    public class InstanceSettings : IEnumerable<RuntimePropertySetter>
    {
        private readonly List<RuntimePropertySetter> settings;

        public InstanceSettings()
        {
            this.settings = new List<RuntimePropertySetter>();
        }

        public InstanceSettings(IEnumerable<RuntimePropertySetter> source)
        {
            this.settings = new List<RuntimePropertySetter>(source);
        }

        public void AddSetting(string property, object value)
        {
            this.settings.Add(new RuntimePropertySetter(property, value));
        }

        public void AddSetting(RuntimePropertySetter setting)
        {
            this.settings.Add(setting);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RuntimePropertySetter> GetEnumerator()
        {
            return this.settings.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
