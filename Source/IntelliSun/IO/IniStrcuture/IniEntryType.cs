using System;

namespace IntelliSun.IO
{
    public enum IniEntryType
    {
        Default = 0x00,
        String = 0x73,
        Int32 = 0x69,
        Long = 0x6c,
        Float = 0x66,
        Decimal = 0x64,
        Boolean = 0x62,
        Char = 0x63,
        DateTime = 0x74
    }
}
