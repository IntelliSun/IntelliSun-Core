using System;
using System.Linq;

namespace IntelliSun
{
    public class Flags
    {
        private readonly long source;

        public Flags(Enum source)
        {
            this.source = source.GetHashCode();
        }

        public Flags(long source)
        {
            this.source = source;
        }

        public FlagsResult IsDeclare(long flag)
        {
            return this.GetResult(Declare(this.source, flag));
        }

        public FlagsResult IsDeclare(Enum flag)
        {
            return this.IsDeclare(flag.GetHashCode());
        }

        public FlagsResult IsDeclareAny(params long[] flags)
        {
            return this.GetResult(DeclareAny(this.source, flags));
        }

        public FlagsResult IsDeclareAny(params Enum[] flags)
        {
            return this.IsDeclareAny(flags.Select(x => (long)x.GetHashCode()).ToArray());
        }

        public FlagsResult IsDeclareAll(params long[] flags)
        {
            return this.GetResult(DeclareAll(this.source, flags));
        }

        public FlagsResult IsDeclareAll(params Enum[] flags)
        {
            return this.IsDeclareAll(flags.Select(x => (long)x.GetHashCode()).ToArray());
        }

        public FlagsResult IsNotDeclare(long flag)
        {
            return this.GetResult(NotDeclare(this.source, flag));
        }

        public FlagsResult IsNotDeclare(Enum flag)
        {
            return this.IsNotDeclare(flag.GetHashCode());
        }

        public FlagsResult IsNotDeclareAny(params long[] flags)
        {
            return this.GetResult(NotDeclareAny(this.source, flags));
        }

        public FlagsResult IsNotDeclareAny(params Enum[] flags)
        {
            return this.IsNotDeclareAny(flags.Select(x => (long)x.GetHashCode()).ToArray()); 
        }

        public FlagsResult IsNotDeclareAll(params long[] flags)
        {
            return this.GetResult(NotDeclareAll(this.source, flags));
        }

        public FlagsResult IsNotDeclareAll(params Enum[] flags)
        {
            return this.IsNotDeclareAll(flags.Select(x => (long)x.GetHashCode()).ToArray()); 
        }

        internal virtual FlagsResult GetResult(bool result)
        {
            return new FlagsResult(this.Source, result);
        }

        public long Source
        {
            get { return this.source; }
        }

        #region Static

        public static bool Declare(long source, long flag)
        {
            return (source & flag) == flag;
        }

        public static bool Declare(Enum source, Enum flag)
        {
            return Declare(source.GetHashCode(), flag.GetHashCode());
        }

        public static bool DeclareAny(long source, params long[] flags)
        {
            return flags.Any(x => Declare(source, x));
        }

        public static bool DeclareAll(long source, params long[] flags)
        {
            return flags.All(x => Declare(source, x));
        }

        public static bool NotDeclare(long source, long flag)
        {
            return (source & flag) == 0;
        }

        public static bool NotDeclareAny(long source, params long[] flags)
        {
            return flags.Any(x => NotDeclare(source, x));
        }

        public static bool NotDeclareAll(long source, params long[] flags)
        {
            return flags.All(x => NotDeclare(source, x));
        }

        #endregion
    }
}