using System;

namespace IntelliSun.ComponentModel.Composition
{
    public interface IExtensionProvider
    {
        object GetExtensionInstance(Type extension);
    }
}