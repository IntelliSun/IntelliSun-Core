using System;
using System.Linq;

namespace IntelliSun
{
    public sealed class FlagsResult : Flags
    {
        private bool result;

        internal FlagsResult(long source, bool result)
            : base(source)
        {
            this.result = result;
        }

        public FlagsResult AndDeclare(long flag)
        {
            return this.IsDeclare(flag);
        }

        public FlagsResult AndDeclare(Enum flag)
        {
            return this.IsDeclare(flag);
        }

        public FlagsResult AndDeclareAny(params long[] flags)
        {
            return this.IsDeclareAny(flags);
        }

        public FlagsResult AndDeclareAny(params Enum[] flags)
        {
            return this.IsDeclareAny(flags);
        }

        public FlagsResult AndDeclareAll(params long[] flags)
        {
            return this.IsDeclareAll(flags);
        }

        public FlagsResult AndDeclareAll(params Enum[] flags)
        {
            return this.IsDeclareAll(flags);
        }

        public FlagsResult AndNotDeclare(long flag)
        {
            return this.IsNotDeclare(flag);
        }

        public FlagsResult AndNotDeclare(Enum flag)
        {
            return this.IsNotDeclare(flag);
        }

        public FlagsResult AndNotDeclareAny(params long[] flags)
        {
            return this.IsNotDeclareAny(flags);
        }

        public FlagsResult AndNotDeclareAny(params Enum[] flags)
        {
            return this.IsNotDeclareAny(flags);
        }

        public FlagsResult AndNotDeclareAll(params long[] flags)
        {
            return this.IsNotDeclareAll(flags);
        }

        public FlagsResult AndNotDeclareAll(params Enum[] flags)
        {
            return this.IsNotDeclareAll(flags);
        }

        public FlagsResult OrDeclare(long flag)
        {
            return this.GetResult(x => x || Declare(this.Source, flag));
        }

        public FlagsResult OrDeclare(Enum flag)
        {
            return this.GetResult(x => x || Declare(this.Source, flag.GetHashCode()));
        }

        public FlagsResult OrDeclareAny(params long[] flags)
        {
            return this.GetResult(x => x || DeclareAny(this.Source, flags));
        }

        public FlagsResult OrDeclareAny(params Enum[] flags)
        {
            return this.GetResult(x => x || 
                                         DeclareAny(this.Source, flags.Select(f => (long)f.GetHashCode()).ToArray()));
        }

        public FlagsResult OrDeclareAll(params long[] flags)
        {
            return this.GetResult(x => x || DeclareAll(this.Source, flags));
        }

        public FlagsResult OrDeclareAll(params Enum[] flags)
        {
            return this.GetResult(x => x ||
                                         DeclareAll(this.Source, flags.Select(f => (long)f.GetHashCode()).ToArray()));
        }

        public FlagsResult OrNotDeclare(long flag)
        {
            return this.GetResult(x => x || NotDeclare(this.Source, flag));
        }

        public FlagsResult OrNotDeclare(Enum flag)
        {
            return this.GetResult(x => x || NotDeclare(this.Source, flag.GetHashCode()));
        }

        public FlagsResult OrNotDeclareAny(params long[] flags)
        {
            return this.GetResult(x => x || NotDeclareAny(this.Source, flags));
        }

        public FlagsResult OrNotDeclareAny(params Enum[] flags)
        {
            return this.GetResult(x => x ||
                                         NotDeclareAny(this.Source, flags.Select(f => (long)f.GetHashCode()).ToArray()));
        }

        public FlagsResult OrNotDeclareAll(params long[] flags)
        {
            return this.GetResult(x => x || NotDeclareAll(this.Source, flags));
        }

        public FlagsResult OrNotDeclareAll(params Enum[] flags)
        {
            return this.GetResult(x => x ||
                                         NotDeclareAll(this.Source, flags.Select(f => (long)f.GetHashCode()).ToArray()));
        }

        public FlagsResult And(Func<FlagsResult, FlagsResult> expression)
        {
            return this.GetResult(x => x && expression(this).result);
        }

        public FlagsResult And(Func<FlagsResult, bool> expression)
        {
            return this.GetResult(x => x && expression(this));
        }

        public FlagsResult Or(Func<FlagsResult, FlagsResult> expression)
        {
            return this.GetResult(x => x || expression(this).result);
        }

        public FlagsResult Or(Func<FlagsResult, bool> expression)
        {
            return this.GetResult(x => x || expression(this));
        }

        internal FlagsResult GetResult(Func<bool, bool> result)
        {
            return this.GetResultCore(result(this.result));
        }

        internal override FlagsResult GetResult(bool result)
        {
            return this.GetResultCore(this.result & result);
        }

        public override string ToString()
        {
            return this.Result.ToString();
        }

        private FlagsResult GetResultCore(bool result)
        {
            this.result = result;
            return this;
        }

        public bool Result
        {
            get { return this.result; }
        }

        public static implicit operator bool(FlagsResult source)
        {
            return source.Result;
        }
    }
}