using System;

namespace IntelliSun
{
    public static class FlagsExtensions
    {
        public static Flags AsFlags(this Enum source)
        {
            return new Flags(source);
        }
    }
}
