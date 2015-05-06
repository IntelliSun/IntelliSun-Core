using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Helpers;

namespace IntelliSun.Text
{
    public class StringCompositor
    {
        private readonly IStringCompositionProvider compositionProvider;

        public StringCompositor(object composition)
            : this(new ReflectionCompositionProvider(composition))
        {
            
        }

        public StringCompositor(IStringCompositionProvider compositionProvider)
        {
            this.compositionProvider = compositionProvider;
        }

        public bool HasComposition(string compositionName)
        {
            return this.compositionProvider.GetComposition(compositionName) != null;
        }

        public string Decompose(string compositionName, string value)
        {
            var composition = this.compositionProvider.GetComposition(compositionName);
            var textParts = new TextParts(composition);
            var stringParts = this.GetStringParts(textParts, new[] { String.Empty });
            var collection = textParts.Parts.Select((part, idx) => new {
                Part = part,
                Parameters = stringParts[idx],
                Index = idx
            }).ToArray();

            var index = 0;
            foreach (var set in collection)
            {
                if (set.Part.Parts.Any(str => str.Equals("$")))
                    break;

                index++;
                var subPart = set.Parameters.Where(value.StartsWith).Aggregate(String.Empty,
                    (current, part) => part.Length > current.Length ? part : current);

                value = value.Substring(subPart.Length);
            }

            foreach (var set in collection.Skip(index).Reverse())
            {
                var subPart = set.Parameters.Where(value.EndsWith).Aggregate(String.Empty,
                    (current, part) => part.Length > current.Length ? part : current);

                value = value.Substring(0, value.Length - subPart.Length);
            }

            return value;
        }

        public IEnumerable<string> Compose(string compositionName, params string[] looseParameters)
        {
            var composition = this.compositionProvider.GetComposition(compositionName);
            return this.ComposeWithComposition(composition, looseParameters);
        }

        public IEnumerable<string> ComposeWithComposition(string composition, params string[] looseParameters)
        {
            return this.GetCompositionStrings(composition, looseParameters);
        }

        private IEnumerable<string> GetCompositionStrings(string composition, IEnumerable<string> looseParameters)
        {
            var textParts = new TextParts(composition);
            var stringParts = this.GetStringParts(textParts, looseParameters);

            // ReSharper disable once CoVariantArrayConversion
            return Compose(stringParts);
        }

        private string[][] GetStringParts(TextParts textParts, IEnumerable<string> looseParameters)
        {
            return textParts.Parts.Select(
                textPart => textPart.IsParameter
                    ? this.GetParametersWithPart(textPart, looseParameters).ToArray()
                    : textPart.Parts).ToArray();
        }

        private IEnumerable<string> GetParametersWithPart(TextPart part, IEnumerable<string> looseParameters)
        {
            var lsParam = looseParameters.ToArray();
            return from key in part.Parts
                   from parameter in this.GetStringParameter(lsParam, key)
                   select part.Format(parameter);
        }

        private static IEnumerable<string> Compose(IEnumerable[] parts)
        {
            var loop = new NestedLoop(parts);
            var ls = new List<string>();
            loop.Last().LoopStart += (nestedLoop, args) =>
                ls.Add(StringHelper.ConcatWith("{0}", nestedLoop.Select(l => l.Value ?? String.Empty)));

            loop.Loop();

            return ls;
        }

        private IEnumerable<string> GetStringParameter(IEnumerable<string> looseParameters, string parameterName)
        {
            return this.GetParameter(looseParameters, parameterName);
        }

        protected virtual IEnumerable<string> GetParameter(IEnumerable<string> looseParameters, string parameterName)
        {
            if (parameterName == "$")
                return looseParameters;

            var bySource = this.compositionProvider.GetCompositionPart(parameterName).ToArray();
            return bySource.Length != 0 ? bySource : new[] { String.Empty };
        }

        public static IEnumerable<string> MapParts(object instance, string key)
        {
            return StringCompositorMappingHelper.GetInstanceParts(instance, key);
        }

        public static string MapCompositions(object instance, string name)
        {
            return StringCompositorMappingHelper.GetInstanceComposition(instance, name);
        }

        private static class StringCompositorMappingHelper
        {
            private readonly static IDictionary<object, IStringCompositionProvider> _mappers;

            static StringCompositorMappingHelper()
            {
                _mappers = new Dictionary<object, IStringCompositionProvider>();
            }

            private static bool IsMapped(object instance)
            {
                return _mappers.ContainsKey(instance);
            }

            private static void MapInstance(object instance)
            {
                if (IsMapped(instance))
                    _mappers[instance] = new ReflectionCompositionProvider(instance);
                else
                    _mappers.Add(instance, new ReflectionCompositionProvider(instance));
            }

            public static IEnumerable<string> GetInstanceParts(object instance, string key)
            {
                if (!IsMapped(instance))
                    MapInstance(instance);

                return _mappers[instance].GetCompositionPart(key);
            }

            public static string GetInstanceComposition(object instance, string name)
            {
                if (!IsMapped(instance))
                    MapInstance(instance);

                return _mappers[instance].GetComposition(name);
            }
        }
    }
}
