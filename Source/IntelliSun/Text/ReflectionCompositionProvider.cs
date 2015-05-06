using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IntelliSun.Reflection;

namespace IntelliSun.Text
{
    internal class ReflectionCompositionProvider : CacheObject, IStringCompositionProvider
    {
        private readonly object composition;
        private readonly bool staticValues;
        private readonly Dictionary<string, PropertyInfo> partProperties; 
        private readonly Dictionary<string, PropertyInfo> compositionProperties; 

        public ReflectionCompositionProvider(object composition)
            : this(composition, false)
        {
            
        }

        public ReflectionCompositionProvider(object composition, bool staticValues)
            : this(composition, composition.GetType(), staticValues)
        {
            
        }

        public ReflectionCompositionProvider(Type compositionType)
            : this(compositionType, false)
        {
            
        }

        public ReflectionCompositionProvider(Type compositionType, bool staticValues)
            : this(null, compositionType, staticValues)
        {
            
        }

        protected ReflectionCompositionProvider(object composition, Type compositionType, bool staticValues)
        {
            this.composition = composition;
            this.staticValues = staticValues;
            this.partProperties = new Dictionary<string, PropertyInfo>();
            this.compositionProperties = new Dictionary<string, PropertyInfo>();

            this.PopulateProperties(compositionType);
        }

        private void PopulateProperties(Type sourceType)
        {
            var props = sourceType.GetProperties();
            foreach (var propertyInfo in props)
                this.LoadProperty(propertyInfo);
        }

        private void LoadProperty(PropertyInfo property)
        {
            var extender = new MemberInfoExtender(property, true);
            var partAttributes = extender.GetAttributes<StringCompositionPartAttribute>().ToArray();
            var compositionAttributes = extender.GetAttributes<StringCompositionAttribute>().ToArray();

            if (!(partAttributes.Any() || compositionAttributes.Any()))
                return;

            foreach (var key in partAttributes.SelectMany(attribute => attribute.PartKeys))
            {
                var lwKey = key.ToLower();
                if (this.partProperties.ContainsKey(lwKey))
                    throw new Exception("${Resources.KeyIsPresentedMultipleTimes}");

                this.partProperties.Add(lwKey, property);
            }

            foreach (var name in compositionAttributes.Select(attribute => attribute.Name))
            {
                if(this.compositionProperties.ContainsKey(name))
                    throw new Exception("${Resources.KeyIsPresentedMultipleTimes}");

                this.compositionProperties.Add(name, property);
            }
        }

        public string GetComposition(string name)
        {
            if (!this.compositionProperties.ContainsKey(name))
                throw new KeyNotFoundException();

            return !this.staticValues
                ? this.GetCompositionString(name)
                : this.GetCachedDataOrValue(GetCompositionCacheKey(name), () => this.GetCompositionString(name));
        }

        private string GetCompositionString(string name)
        {
            return this.compositionProperties[name].GetValue(this.composition, new object[0]).ToString();
        }

        private static string GetCompositionCacheKey(string name)
        {
            return String.Format("%{0}", name);
        }

        public IEnumerable<string> GetCompositionPart(string key)
        {
            if (!this.partProperties.ContainsKey(key))
                throw new KeyNotFoundException();

            return !this.staticValues
                ? this.GetPartStrings(key)
                : this.GetCachedDataOrValue(GetPartCacheKey(key), () => this.GetPartStrings(key));
        }

        private IEnumerable<string> GetPartStrings(string key)
        {
            var source = this.partProperties[key].GetValue(this.composition, new object[0]);
            if(source == null)
                yield break;

            if (source is String)
                yield return source.ToString();

            if (!(source is IEnumerable) || source is String)
                yield break;

            var enumerable = (IEnumerable)source;
            foreach (var value in enumerable)
                yield return value.ToString();
        }

        private static string GetPartCacheKey(string key)
        {
            return String.Format("@{0}", key);
        }
    }
}