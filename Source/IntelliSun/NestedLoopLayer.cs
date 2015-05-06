using System;
using System.Collections;

namespace IntelliSun
{
    internal class NestedLoopLayer
    {
        private readonly IEnumerable source;
        private readonly int layerLevel;
        private readonly LoopNotifier notifier;
        private IEnumerator sourceEnumerator;

        public NestedLoopLayer(IEnumerable source, int layerLevel, LoopNotifier notifier)
        {
            this.source = source;
            this.layerLevel = layerLevel;
            this.notifier = notifier;
        }

        public void Enumerate()
        {
            this.Reset();
            this.sourceEnumerator = this.source.GetEnumerator();
            while (this.MoveNext())
            {
                var loopStartAction = this.notifier.LoopStart(this);
                if(loopStartAction == LoopAction.Continue)
                    continue;
                
                if (loopStartAction == LoopAction.Break)
                    break;

                this.ExecuteBody();

                if (this.notifier.LoopEnd(this) == LoopAction.Break)
                    break;
            }
        }

        private bool MoveNext()
        {
            if (!this.sourceEnumerator.MoveNext())
                return false;
            
            this.CurrentIndex++;
            return true;
        }

        private void ExecuteBody()
        {
            if (this.NestedLayer != null)
                this.NestedLayer.Enumerate();
        }

        private void Reset()
        {
            this.CurrentIndex = 0;
            if (this.sourceEnumerator != null)
                this.sourceEnumerator.Reset();

            if (this.NestedLayer != null)
                this.NestedLayer.Reset();

            this.sourceEnumerator = null;
        }

        public int CurrentIndex { get; set; }

        public object CurrentValue
        {
            get { return this.sourceEnumerator.Current; }
        }

        public NestedLoopLayer NestedLayer { get; set; }

        public int LayerLevel
        {
            get { return this.layerLevel; }
        }
    }
}