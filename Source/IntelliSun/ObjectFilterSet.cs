using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun
{
    public abstract class ObjectFilterSet<T> : IObjectFilter<T>
    {
        private readonly FiltersRelation relation;
        private readonly List<IObjectFilter<T>> filters;

        protected ObjectFilterSet(FiltersRelation relation)
        {
            this.relation = relation;
            this.filters = new List<IObjectFilter<T>>();
        }

        public bool Check(T value)
        {
            return this.CombineByOperation(this.CheckValue(value), this.CheckFilters(value));
        }

        protected bool CombineByOperation(bool a, bool b)
        {
            switch (this.relation)
            {
                case FiltersRelation.And:
                    return a && b;
                case FiltersRelation.Or:
                    return a || b;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected bool CheckFilters(T value)
        {
            switch (this.relation)
            {
                case FiltersRelation.And:
                    return this.filters.All(f => f.Check(value));
                case FiltersRelation.Or:
                    return this.filters.Any(f => f.Check(value));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract bool CheckValue(T value);

        public void AddFilter(IObjectFilter<T> filter)
        {
            this.filters.Add(filter);
        }
    }
}