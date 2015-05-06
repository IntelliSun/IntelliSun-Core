using System;
using System.Collections.Generic;
using System.IO;

namespace IntelliSun.IO
{
    public static class Extentions
    {
        public static FileInfo[] GetFiles(this DirectoryInfo dir, string[] patterns)
        {
            var fls = new List<FileInfo>();
            foreach (var p in patterns)
            {
                fls.AddRange(dir.GetFiles("*." + p));
            }

            return fls.ToArray();
        }
    }
}
