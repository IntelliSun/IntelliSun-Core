using System;

namespace IntelliSun.Collections
{
    public interface IMutableSet
    {
        void Add(object item);
        bool Remove(object item);
    }

    public interface IMutableSet<in T>
    {
        void Add(T item);
        bool Remove(T item);
    }
}
