using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliSun.Helpers;

namespace IntelliSun.Text
{
    public abstract class TextTreeItem
    {
        //internal TextTreeItem parentItem;

        public abstract TextTreeItemCollection ChildItems
        {
            get;
        }

        public virtual bool HasChildItems
        {
            get { return this.ChildItems.Any(); }
        }

        public virtual string InnerContent
        {
            get
            {
                var coll = this.ChildItems.Select((x) => x.FullContent);
                var content = String.Concat(coll);

                return content;
            }
        }

        public virtual string FullContent
        {
            get
            {
                return StringHelper.TFormat(this.ItemFormat,
                    new Dictionary<string, string>(){
                    { "ic", this.InnerContent}
                });
            }
        }

        protected abstract string ItemFormat
        {
            get;
            set;
        }
    }
}
