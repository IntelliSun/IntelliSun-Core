using System;

namespace IntelliSun
{
    internal abstract class LoopNotifier
    {
        private readonly NestedLoop loop;

        protected LoopNotifier(NestedLoop loop)
        {
            this.loop = loop;
        }

        public NestedLoop Loop
        {
            get { return this.loop; }
        }

        public abstract LoopAction LoopStart(NestedLoopLayer layer);
        public abstract LoopAction LoopEnd(NestedLoopLayer layer);
    }
}
