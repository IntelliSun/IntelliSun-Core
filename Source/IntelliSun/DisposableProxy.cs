using System;

namespace IntelliSun
{
    public class DisposableProxy : IDisposable
    {
        private readonly IDisposable target;

        public DisposableProxy(IDisposable target)
        {
            this.target = target;
        }

        public void Dispose()
        {
            if (this.target != null)
                this.target.Dispose();
        }
    }
}
