using System;

namespace IntelliSun
{
    public class LoopEventArgs : EventArgs
    {
        private readonly int iteration;
        private readonly object value;

        public LoopEventArgs(int loopLayer)
        {
            this.LoopLayer = loopLayer;
            this.LoopAction = LoopAction.None;
        }

        public LoopEventArgs(int loopLayer, int iteration, object value)
            : this(loopLayer)
        {
            this.iteration = iteration;
            this.value = value;
        }

        public int LoopLayer { get; set; }
        public LoopAction LoopAction { get; set; }

        public virtual int Iteration
        {
            get { return this.iteration; }
        }

        public virtual object Value
        {
            get { return this.value; }
        }
    }
}