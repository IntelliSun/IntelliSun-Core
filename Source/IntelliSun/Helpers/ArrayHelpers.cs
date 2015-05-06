using System;

namespace IntelliSun.Helpers
{
    public static class ArrayHelpers
    {
        public static T[] Resize<T>(this T[] source, int size)
        {
            var result = new T[size];
            var len = source.Length < size ? source.Length : size;
            Array.Copy(source, 0, result, 0, len);

            return result;
        }
    }
}
