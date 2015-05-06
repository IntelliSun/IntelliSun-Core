using System;

namespace IntelliSun.ComponentModel
{
    public interface IIdentifiableService : IInitializable
    {
        string ServiceKey { get; }
    }
}
