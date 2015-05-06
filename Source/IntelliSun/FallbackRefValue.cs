using System;

namespace IntelliSun
{
    public class FallbackRefValue<T> : FallbackValue<T>
        where T : class
    {
        private T value;

        public FallbackRefValue(T defaultValue)
            : base(defaultValue)
        {
        }

        public override void ClearValue()
        {
            this.value = null;
        }

        public override void SetValue(T value)
        {
            this.value = value;
        }

        public override T Value
        {
            get { return this.value ?? this.DefaultValue; }
        }
    }
}