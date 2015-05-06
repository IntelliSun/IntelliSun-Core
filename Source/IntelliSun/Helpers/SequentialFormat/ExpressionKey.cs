using System;

namespace IntelliSun.Helpers
{
    internal enum ExpressionKey
    {
        //FirstItem
        First = (int)'f',

        //LastItem
        Last = (int)'l',

        //NonLastItem
        General = (int)'g',

        //NonFirstItem
        Internal = (int)'i',

        //FirstAndNonOnlyItem
        Prefix = (int)'p',

        //NonFirstOrLastItem
        Middle = (int)'m'
    }
}