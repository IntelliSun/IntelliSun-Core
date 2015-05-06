using System;
using System.Collections.Generic;

namespace IntelliSun.ComponentModel
{
    public class DisposeHandler : Unmanaged
    {
        private readonly Stack<IDisposable> children;  

        public DisposeHandler()
        {
            this.children = new Stack<IDisposable>();
        }

        public void RegisterChild(IDisposable child)
        {
            this.ThrowIfDisposed();

            if (child == null)
                throw new ArgumentNullException("child");

            if (this.children.Contains(child))
                throw new ArgumentException("${Resources.ChildAlreadyPresented}", "child");

            this.children.Push(child);
        }

        public void DisposeIfAlive()
        {
            if (!this.IsDisposed)
                this.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            this.ThrowIfDisposed();

            if (disposing)
            {
                while (this.children.Count > 0)
                {
                    var disposable = this.children.Pop();
                    if (disposable != null)
                        disposable.Dispose();
                }

                this.children.Clear();
            }

            base.Dispose(disposing);
        }
    }
}
