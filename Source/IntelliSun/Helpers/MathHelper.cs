using System;

namespace IntelliSun.Helpers
{
    public static class MathHelper
    {
        public static bool IsBetween(double number, double a, double b)
        {
            if (Equals(a, b))
                return false;

            var u = (a > b) ? a : b;
            var l = (a < b) ? a : b;
            return (u >= number && l <= number);
        }

        public static int Limit(int number, int increment, int limit)
        {
            var val = number + increment;
            if (val >= 0)
                return val % limit + 1;
            
            return ((limit + 1) - (val * -1)) % (limit + 1);
        }

        public static bool NearEquality(double a, double b)
        {
            return NearEquality(a, b, double.Epsilon);
        }

        public static bool NearEquality(double a, double b, double epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static bool NearEquality(float a, float b)
        {
            return NearEquality(a, b, float.Epsilon);
        }

        public static bool NearEquality(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value <= min)
                return min;

            return value >= max ? max : value;
        }
    }
}
