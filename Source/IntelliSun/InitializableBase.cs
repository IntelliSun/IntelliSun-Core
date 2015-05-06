using System;

namespace IntelliSun
{
    public abstract class InitializableBase : IInitializable
    {
        private bool initialized;

        public event EventHandler Initialized;

        public void Initialize()
        {
            if (this.initialized)
                return;

            this.Initializing();
            this.initialized = true;

            this.OnInitialized(EventArgs.Empty);
        }

        protected virtual void Initializing()
        {
            
        }

        protected virtual void OnInitialized(EventArgs args)
        {
            var handler = this.Initialized;
            if (handler != null)
                handler(this, args);
        }

        public bool IsInitialized
        {
            get { return this.initialized; }
        }
    }
}
