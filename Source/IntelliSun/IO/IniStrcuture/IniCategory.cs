using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IntelliSun.IO
{
    [DebuggerDisplay("Name = {Name}")]
    public class IniCategory : IniObject
    {
        private IniEntries entries;

        public IniCategory(string name)
        {
            this.Name = name;

            this.entries = new IniEntries();
        }

        public override string GetString(IniParameters parameters)
        {
            var blr = new StringBuilder(this.GetCommentsString(parameters));
            blr.Append(String.Format("[{0}]", this.Content));

            return blr.ToString();
        }

        public bool HasEntry(string name)
        {
            return entries.Contains(name);
        }

        protected internal void AddEntry(IniEntry entry)
        {
            entry.SetCategory(this);
            this.entries.Add(entry);
        }

        public IniEntries Entries
        {
            get { return this.entries; }
            protected internal set 
            {
                this.entries = value; 
            }
        }

        public override string Content
        {
            get { return this.Name; }
        }

        public IniEntry this[string name]
        {
            get { return this.entries[name]; }
        }

        public string Name { get; set; }

        internal class NameComparer : IEqualityComparer<IniCategory>
        {
            public static NameComparer Default = new NameComparer();

            public bool Equals(IniCategory x, IniCategory y)
            {
                return y != null && String.Equals(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(IniCategory obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
