using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public static class SignatureStringProvider
    {
        public static string Generate(Type type)
        {
            const string format = "{0}::{1}-{2}#{3}";

            if (type == null)
                throw new ArgumentNullException("type");

            var hashCode = type.GetHashCode();

            var attributes =
                type.GetMembers().SelectMany(x => x.GetCustomAttributes(true)).Concat(type.GetCustomAttributes(true));

            var nonPublicMembers = type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance);
            var statics = type.GetMembers(BindingFlags.Static | BindingFlags.Public);

            var items = type.GetMembers().Concat(attributes).Concat(nonPublicMembers).Concat(statics).ToArray();

            var typeCode = items.First().GetHashCode();
            typeCode = items.Skip(1).Aggregate(typeCode, (current, item) => current ^ item.GetHashCode());

            var asmDate = File.GetLastWriteTime(type.Assembly.Location);

            return String.Format(format, hashCode.ToString("X"), typeCode, items.Count(),
                asmDate.Ticks.ToString("X"));
        }

        public static bool MatchSignature(string source, string reference)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (reference == null)
                throw new ArgumentNullException("reference");

            return source.Substring(0, source.IndexOf('#')).Equals(reference.Substring(0, reference.IndexOf('#')));
        }

        public static bool MatchDateAndSignature(string source, string reference)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (reference == null)
                throw new ArgumentNullException("reference");

            return source.Equals(reference);
        }
    }
}
