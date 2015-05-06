using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntelliSun.IO
{
    internal static class IniTypeHelper
    {
        private static readonly Dictionary<Type, IniEntryType> _typeCastingTable;
        private static readonly Dictionary<IniEntryType, Type> _eTypeCastingTable;
        static IniTypeHelper()
        {
            _typeCastingTable = new Dictionary<Type, IniEntryType> {
                { typeof(Boolean), IniEntryType.Boolean },
                { typeof(Char), IniEntryType.Char },
                { typeof(DateTime), IniEntryType.DateTime },
                { typeof(Decimal), IniEntryType.Decimal },
                { typeof(Single), IniEntryType.Float },
                { typeof(Int32), IniEntryType.Int32 },
                { typeof(Int64), IniEntryType.Long },
                { typeof(String), IniEntryType.String }
            };

            _eTypeCastingTable = new Dictionary<IniEntryType, Type> {
                { IniEntryType.Boolean, typeof(Boolean) },
                { IniEntryType.Char, typeof(Char) },
                { IniEntryType.DateTime, typeof(DateTime) },
                { IniEntryType.Decimal, typeof(Decimal) },
                { IniEntryType.Float, typeof(Single) },
                { IniEntryType.Int32, typeof(Int32) },
                { IniEntryType.Long, typeof(Int64) },
                { IniEntryType.String, typeof(String) },
                { IniEntryType.Default, typeof(String) }
            };
        }

        public static IniEntryType GetIniType(Type type)
        {
            return !_typeCastingTable.ContainsKey(type) ? default(IniEntryType) : _typeCastingTable[type];
        }

        public static Type GetClrType(IniEntryType type)
        {
            return !_eTypeCastingTable.ContainsKey(type) ? typeof(Object) : _eTypeCastingTable[type];
        }

        public static bool IsValidType(Type objectType)
        {
            return IsValidType(objectType, IniEntryType.Default);
        }

        public static bool IsValidType(Type objectType, IniEntryType type)
        {
            var converter = TypeDescriptor.GetConverter(objectType);
            var eType = _eTypeCastingTable[type];

            return converter.CanConvertTo(eType);
        }

        public static bool IsIniType(Type type)
        {
            return _typeCastingTable.ContainsKey(type);
        }

        public static bool CanCast<TFrom, TTo>()
        {
            return CanCast(typeof(TFrom), typeof(TTo));
        }

        public static bool CanCast(Type from, Type to)
        {
            var converter = TypeDescriptor.GetConverter(to);
            return converter.CanConvertFrom(from);
        }

        public static TOut Cast<TOut, TIn>(TIn value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TOut));

            if (converter.CanConvertFrom(typeof(TIn)))
                return (TOut)converter.ConvertFrom(value);

            throw new InvalidCastException();
        }

        public static object Cast<TIn>(TIn value, Type outType)
        {
            var converter = TypeDescriptor.GetConverter(outType);
            if (converter.CanConvertFrom(typeof(TIn)))
                return converter.ConvertFrom(value);

            throw new InvalidCastException();
        }

        public static IniEntryType DefaultType
        {
            get { return IniEntryType.Default; }
        }
    }
}
