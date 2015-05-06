using System;

namespace IntelliSun
{
    public interface IValueConverter
    {
        bool CanConvert(object value);
        object Convert(object value);
    }
}