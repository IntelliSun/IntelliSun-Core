using System;

namespace IntelliSun
{
    public class ObjectFilterCollection<T> : ObjectFilterSet<T>
    {
        public ObjectFilterCollection(FiltersRelation relation)
            : base(relation)
        {
        }

        protected sealed override bool CheckValue(T value)
        {
            return true;
        }
    }
}