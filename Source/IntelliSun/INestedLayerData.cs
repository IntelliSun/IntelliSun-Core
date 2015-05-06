using System;

namespace IntelliSun
{
    public interface INestedLayerData
    {
        event NestedLoopHandler LoopStart;
        event NestedLoopHandler LoopEnd;

        int Index { get; }
        object Value { get; }
    }
}