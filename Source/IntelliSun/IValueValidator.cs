using System;

namespace IntelliSun
{
    public interface IValueValidator
    {
        ValueValidatorResult IsValid(object value);
    }

    public interface IValueValidator<in T> : IValueValidator
    {
        ValueValidatorResult IsValid(T value);
    }
}