using System;

namespace IntelliSun.Text
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class StringCompositionPartAttribute : Attribute
    {
        private readonly string[] partKeys;

        public StringCompositionPartAttribute(string partKey)
            : this(new [] { partKey} )
        {
            
        }

        public StringCompositionPartAttribute(params string[] partKeys)
        {
            this.partKeys = partKeys;
        }

        public string[] PartKeys
        {
            get { return this.partKeys; }
        }
    }
}