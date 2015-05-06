using System;
using System.Collections.Generic;
using IntelliSun.Collections;

namespace IntelliSun.Text
{
    public class StringCompositionProvider : IStringCompositionProvider
    {
        private readonly GroupedList<string, string> partsSet;
        private readonly Dictionary<string, string> compositions;

        public StringCompositionProvider()
        {
            this.partsSet = new GroupedList<string, string>();
            this.compositions = new Dictionary<string, string>();
        }

        public void AddComposition(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (value == null)
                throw new ArgumentNullException("value");

            this.compositions[name] = value;
        }

        public void AddPartsSet(string key, params string[] parts)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.partsSet.Add(key).AddRange(parts);
        }

        public string GetComposition(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return this.compositions.ContainsKey(name) ? this.compositions[name] : null;
        }

        public IEnumerable<string> GetCompositionPart(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.partsSet[key];
        }
    }
}