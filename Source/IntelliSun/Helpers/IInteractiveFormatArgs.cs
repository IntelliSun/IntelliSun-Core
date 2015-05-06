using System;

namespace IntelliSun.Helpers
{
    public interface IInteractiveFormatArgs
    {
        string Key { get; }
        object ParametersProvider { get; }
        IInteractiveFormatArgs FormKey(string key);
    }
}