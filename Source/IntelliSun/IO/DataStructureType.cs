using System;

namespace IntelliSun.IO
{
    public enum DataStructureType
    {
        // -RawData [1 - 99]
        UnicodeData = 1,
        BinaryData = 2,

        // -Encoded Structured Data [100 - 199]
        IniStructureData = 100,
        XmlStructureData = 101,

        DedicatedData = 198,
        DedicatedBinaryData = 199
    }
}
