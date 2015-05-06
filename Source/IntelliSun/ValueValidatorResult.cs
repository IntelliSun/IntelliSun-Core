using System;

namespace IntelliSun
{
    public class ValueValidatorResult
    {
        private static readonly ValueValidatorResult _valid;
        private static readonly ValueValidatorResult _invalid;

        static ValueValidatorResult()
        {
            _valid = new ValueValidatorResult(true);
            _invalid = new ValueValidatorResult(false);
        }

        public ValueValidatorResult(bool isValid)
        {
            this.IsValid = isValid;
        }

        public ValueValidatorResult(bool isValid, params Exception[] errors)
            : this(isValid)
        {
            this.Errors = errors;
        }

        public static implicit operator bool(ValueValidatorResult value)
        {
            return value.IsValid;
        }

        public Exception[] Errors { get; set; }
        public bool IsValid { get; set; }

        public static ValueValidatorResult Valid
        {
            get { return _valid; }
        }

        public static ValueValidatorResult Invalid
        {
            get { return _invalid; }
        }
    }
}