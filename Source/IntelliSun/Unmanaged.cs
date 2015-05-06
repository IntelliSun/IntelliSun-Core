using System;

namespace IntelliSun
{
    public abstract class Unmanaged : INotifyDisposing, INotifyDisposed, IDisposable
    {
        private bool isDisposing;
        private bool isDisposed;
        private bool suppressFinalize = true;

        public event EventHandler<EventArgs> Disposing; 
        public event EventHandler<EventArgs> Disposed;

        public void Dispose()
        {
            if(this.IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            this.isDisposing = true;

            this.OnDisposing(EventArgs.Empty);
            this.Dispose(this.isDisposing);

            if(this.suppressFinalize)
                GC.SuppressFinalize(this);

            this.isDisposing = false;
            this.isDisposed = true;

            this.OnDisposed(EventArgs.Empty);
        }

        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
        }

        protected void OnDisposing(EventArgs args)
        {
            var handler = this.Disposing;
            if (handler != null)
                handler(this, args);
        }

        protected void OnDisposed(EventArgs args)
        {
            var handler = this.Disposed;
            if (handler != null)
                handler(this, args);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected virtual bool CanDispose()
        {
            return true;
        }

        public bool IsDisposing
        {
            get { return this.isDisposing; }
        }

        public virtual bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        protected bool SuppressFinalize
        {
            get { return this.suppressFinalize; }
            set { this.suppressFinalize = value; }
        }
    }
}
