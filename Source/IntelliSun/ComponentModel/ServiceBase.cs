using System;

namespace IntelliSun.ComponentModel
{
    public abstract class IdentifiableServiceBase : InitializableBase, IIdentifiableService
    {
        private Guid identifier;

        protected IdentifiableServiceBase()
        {
            identifier = this.GetType().GUID;
        }

        public virtual string ServiceKey
        {
            get { return this.identifier.ToString(); }
        }

        public Guid ServiceIdentifier
        {
            get { return this.identifier; }
        }
    }
}
