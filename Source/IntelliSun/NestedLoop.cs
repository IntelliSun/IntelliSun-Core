using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun
{
    public class NestedLoop : IEnumerable<INestedLayerData>
    {
        private readonly IEnumerable[] layers;
        private readonly NestedLoopLayer[] loopLayers;
        private readonly NestedLayerData[] layersData;
        private readonly LoopNotifier notifier;

        public NestedLoop(IEnumerable[] layers)
        {
            this.layers = layers;
            this.notifier = new NestedLoopNotifier(this);
            this.loopLayers = this.CreateLayers();
            this.layersData = this.GenerateLayersData();
            this.InsertLayers();
        }

        private NestedLoopLayer[] CreateLayers()
        {
            return this.layers.Select((l, i) => new NestedLoopLayer(l, i, this.notifier)).ToArray();
        }

        private NestedLayerData[] GenerateLayersData()
        {
            return this.loopLayers.Select(l => new NestedLayerData(this, l)).ToArray();
        }

        private void InsertLayers()
        {
            var layer = this.loopLayers[0];
            for (var i = 1; i < this.loopLayers.Length; i++)
            {
                layer.NestedLayer = this.loopLayers[i];
                layer = this.loopLayers[i];
            }
        }

        public void Loop()
        {
            this.loopLayers.First().Enumerate();
        }

        public INestedLayerData this[int layer]
        {
            get { return this.layersData[layer]; }
        }

        private class NestedLoopNotifier : LoopNotifier
        {
            public NestedLoopNotifier(NestedLoop loop)
                : base(loop)
            {
            }

            public override LoopAction LoopStart(NestedLoopLayer layer)
            {
                var args = new NesterLoopEventArgs(layer);
                this.Loop.layersData[layer.LayerLevel].OnLoopStart(args);

                return args.LoopAction;
            }

            public override LoopAction LoopEnd(NestedLoopLayer layer)
            {
                var args = new NesterLoopEventArgs(layer);
                this.Loop.layersData[layer.LayerLevel].OnLoopEnd(args);

                return args.LoopAction;
            }
        }

        private class NestedLayerData : INestedLayerData
        {
            private readonly NestedLoop loop;
            private readonly NestedLoopLayer layer;
            public event NestedLoopHandler LoopStart;
            public event NestedLoopHandler LoopEnd;

            public NestedLayerData(NestedLoop loop, NestedLoopLayer layer)
            {
                this.loop = loop;
                this.layer = layer;
            }

            public int Index
            {
                get { return this.layer.CurrentIndex; }
            }

            public object Value
            {
                get { return this.layer.CurrentValue; }
            }

            internal void OnLoopStart(LoopEventArgs args)
            {
                var handler = this.LoopStart;
                if (handler != null)
                    handler(this.loop, args);
            }

            internal void OnLoopEnd(LoopEventArgs args)
            {
                var handler = this.LoopEnd;
                if (handler != null)
                    handler(this.loop, args);
            }
        }

        private class NesterLoopEventArgs : LoopEventArgs
        {
            private readonly NestedLoopLayer loopLayer;

            public NesterLoopEventArgs(NestedLoopLayer loopLayer)
                : base(loopLayer.LayerLevel)
            {
                this.loopLayer = loopLayer;
            }

            public override int Iteration
            {
                get { return this.loopLayer.CurrentIndex; }
            }

            public override object Value
            {
                get { return this.loopLayer.CurrentValue; }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<INestedLayerData> GetEnumerator()
        {
            return this.layersData.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}