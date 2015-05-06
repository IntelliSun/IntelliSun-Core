using System;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.Aim
{
    public class ConventionManager<T>
    {
        private readonly IIntentAggregator<T> aggregator;
        private readonly List<IConvention<T>> conventions;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ConventionManager()
            : this(new IntentAggregator<T>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ConventionManager(IIntentAggregator<T> aggregator)
        {
            this.aggregator = aggregator;
            this.conventions = new List<IConvention<T>>();
        }

        public void AddConvention(IConvention<T> convention)
        {
            if (convention == null)
                throw new ArgumentNullException("convention");

            this.conventions.Add(convention);
        }

        public bool RemoveConvention(IConvention<T> convention)
        {
            return this.conventions.Remove(convention);
        }

        public void ApplyTo(T instance)
        {
            var sourceIntentes =
                from conv in this.conventions
                from ii in conv.QueryIntents(instance)
                select ii;

            var intents = this.aggregator.Aggregate(sourceIntentes.ToArray());
            intents.Invoke(i => i.Invoke(instance));
        }
    }
}