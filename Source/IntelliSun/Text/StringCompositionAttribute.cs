using System;

namespace IntelliSun.Text
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class StringCompositionAttribute : Attribute
    {
        private readonly string name;

        public StringCompositionAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}