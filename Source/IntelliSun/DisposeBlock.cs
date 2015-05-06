using System;

namespace IntelliSun
{
    public class DisposeBlock<T> : Unmanaged
        where T : class 
    {
        private T data;
        private readonly Func<T> dataGetter;
        private readonly bool preventDispose;

        public DisposeBlock(T data)
        {
            this.data = data;
            this.dataGetter = () => this.data;
        }

        public DisposeBlock(Func<T> data)
        {
            this.dataGetter = data;
        }

        public DisposeBlock(Func<T> data, bool preventDispose)
        {
            this.dataGetter = data;
            this.preventDispose = preventDispose;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !preventDispose)
            {
                var disposable = this.Data as IDisposable;
                if (disposable != null)
                    disposable.Dispose();

                this.data = null;
            }

            base.Dispose(disposing);
        }

        public T Data
        {
            get { return dataGetter(); }
        }
    }
}
