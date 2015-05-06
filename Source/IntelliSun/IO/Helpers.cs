using System;
using System.Collections.Generic;
using IntelliSun.Helpers;

namespace IntelliSun.IO
{
    public static class IoHelper
    {
        public static string FormatPath(string format)
        {
            var p = format;
            p = StringHelper.AdvancedFormat(p, new Dictionary<char, Func<int, string, string>> {
                {'a', (x, s) => new RelativeDirectory(AppDomain.CurrentDomain.BaseDirectory).Up(x)},
                {'u', (x, s) => new RelativeDirectory(s).Up(x)}
            });

            return p;
        }
    }
}
