using System;

namespace IntelliSun.IO
{
    public static class DataStructureTypeHelper
    {
        public static string FileExtention(DataStructureType dsType)
        {
            switch (dsType)
            {
                case DataStructureType.UnicodeData:
                    return "txt";
                case DataStructureType.BinaryData:
                    return "bin";
                case DataStructureType.IniStructureData:
                    return "ini";
                case DataStructureType.XmlStructureData:
                    return "xml";
                case DataStructureType.DedicatedData:
                case DataStructureType.DedicatedBinaryData:
                    return "ads";
            }

            return String.Empty;
        }
    }
}
