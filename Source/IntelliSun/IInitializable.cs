using System;

namespace IntelliSun
{
    public interface IInitializable
    {
        void Initialize();

        bool IsInitialized { get; }
    }
}
