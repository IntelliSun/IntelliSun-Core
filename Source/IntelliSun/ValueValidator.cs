using System;

namespace IntelliSun
{
    public abstract class ValueValidator<T> : IValueValidator<T>
    {
        public abstract ValueValidatorResult IsValid(T value);

        ValueValidatorResult IValueValidator.IsValid(object value)
        {
            try
            {
                var val = Convert.ChangeType(value, typeof(T));
                return this.IsValid((T)val);
            } catch
            {
                return new ValueValidatorResult(false, new InvalidCastException("Value is of invalid Type"));
            }
        }
    }
}