using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using IntelliSun.Helpers;

namespace IntelliSun.IO
{
    [DebuggerDisplay("Value = {Value}")]
    public class IniEntry : IniObject
    {
        private string key;
        private string keyFull;

        private string value;
        private IniEntryType valueType;

        private char typeKey;

        public IniEntry(string key, string value)
            : this(key, value, IniEntryType.String)
        {

        }

        public IniEntry(string key, int value)
            : this(key, value, IniEntryType.Int32)
        {

        }

        public IniEntry(string key, long value)
            : this(key, value, IniEntryType.Long)
        {

        }

        public IniEntry(string key, float value)
            : this(key, value, IniEntryType.Float)
        {

        }

        public IniEntry(string key, decimal value)
            : this(key, value, IniEntryType.Decimal)
        {

        }

        public IniEntry(string key, bool value)
            : this(key, value, IniEntryType.Boolean)
        {
            
        }

        public IniEntry(string key, char value)
            : this(key, value, IniEntryType.Char)
        {

        }

        public IniEntry(string key, object value)
            : this(key, value, IniEntryType.String)
        {

        }

        public IniEntry(string key, DateTime value)
            : this(key, value, IniEntryType.DateTime)
        {

        }

        public IniEntry(string key, object value, IniEntryType type)
        {
            this.value = value.ToString();

            this.UpdateKey(key);
            this.UpdateType(type);
        }

        public void SetValue(string value)
        {
            this.SetValue(value, IniEntryType.Default);
        }

        public void SetValue(int value)
        {
            this.SetValue(value, IniEntryType.Int32);
        }

        public void SetValue(float value)
        {
            this.SetValue(value, IniEntryType.Float);
        }

        public void SetValue(decimal value)
        {
            this.SetValue(value, IniEntryType.Decimal);
        }

        public void SetValue(bool value)
        {
            this.SetValue(value, IniEntryType.Boolean);
        }

        public void SetValue(char value)
        {
            this.SetValue(value, IniEntryType.Char);
        }

        public void SetValue(object value)
        {
            this.SetValue(value, IniEntryType.Default);
        }

        public void SetValue(DateTime value)
        {
            this.SetValue(value, IniEntryType.DateTime);
        }

        private void SetValue(object value, IniEntryType type)
        {
            this.value = value.ToString();
            this.UpdateType(type);
        }

        private void UpdateKey(string key)
        {
            if (key.Contains(new[] { ' ', '=' }))
                throw new ArgumentException("Argument may only contain letters, numbers and '_'", key);

            this.key = key;
            this.keyFull = key;
        }

        private void UpdateType(IniEntryType type)
        {
            this.valueType = type;

            var charType = type;
            if (type == IniEntryType.Default)
                charType = IniEntryType.String;

            this.typeKey = (char)(int)charType;
        }

        public override string GetString(IniParameters parameters)
        {
            var blr = new StringBuilder(this.GetCommentsString(parameters));

            if (parameters.KeyTypeIdentifier != IniKeyTypeIdentifier.None)
            {
                blr.Append((int)parameters.KeyTypeIdentifier % 10 == 0
                    ? Char.ToLower(this.typeKey)
                    : Char.ToUpper(this.typeKey));

                if ((int)parameters.KeyTypeIdentifier < 20)
                    blr.Append(':');
            }

            blr.Append(this.keyFull + '=');
            blr.Append(this.value);

            return blr.ToString();
        }

        internal void SetCategory(IniCategory category)
        {
            this.Category = category;
        }

        public string Key
        {
            get { return this.key; }
            set
            {
                if(this.key != value)
                    this.UpdateKey(value);
            }
        }

        public string Value
        {
            get { return this.value;}
        }

        public override string Content
        {
            get {
                return String.Format("{0}={1}",
                    this.keyFull, this.value);
            }
        }

        public IniEntryType ValueType
        {
            get { return this.valueType; }
        }

        public IniCategory Category { get; protected set; }

        internal class NameComparer : IEqualityComparer<IniEntry>
        {
            public static NameComparer Default = new NameComparer();

            public bool Equals(IniEntry x, IniEntry y)
            {
                return y != null && String.Equals(x.Key, y.Key, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(IniEntry obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
