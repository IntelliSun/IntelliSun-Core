using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliSun.Text
{
    public class TextTreeItemCollection : IEnumerable<TextTreeItem>, System.Collections.IEnumerable
    {
        private List<TextTreeItem> items;

        public TextTreeItemCollection()
        {
            this.items = new List<TextTreeItem>();
        }

        internal void Add(TextTreeItem item)
        {
            this.items.Add(item);
        }

        internal void Clear()
        {
            this.items.Clear();
        }

        internal bool Remove(TextTreeItem item)
        {
            return this.items.Remove(item);
        }

        public bool Contains(TextTreeItem item)
        {
            return this.items.Contains(item);
        }

        public void CopyTo(TextTreeItem[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TextTreeItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public TextTreeItem this[int index]
        {
            get { return this.items[index]; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
