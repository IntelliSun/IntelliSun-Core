using System;

namespace IntelliSun
{
    public interface IValueConverter<out T> : IValueConverter
    {
        new T Convert(object value);
    }
}