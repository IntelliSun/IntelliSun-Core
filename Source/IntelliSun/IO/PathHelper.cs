using System;
using System.IO;
using System.Text.RegularExpressions;

namespace IntelliSun.IO
{
    public static class PathHelper
    {
        public static bool IsFullRootedPath(string path)
        {
            const string expression = ".:\\.*";

            return Regex.IsMatch(path, expression);
        }

        public static string RelateTo(string originalPath, string rootPath)
        {
            var pathA = Path.GetFullPath(originalPath);
            var pathB = Path.GetFullPath(rootPath);

            return pathA.Replace(pathB, "");
        }
    }
}
