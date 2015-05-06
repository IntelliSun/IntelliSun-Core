using System;

namespace IntelliSun
{
    public abstract class FallbackValue<T>
    {
        private readonly T defaultValue;

        protected FallbackValue(T defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public abstract void ClearValue();
        public abstract void SetValue(T value);

        protected T DefaultValue
        {
            get { return this.defaultValue; }
        }

        public abstract T Value { get; }
    }
}
