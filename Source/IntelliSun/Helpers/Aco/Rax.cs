using System;
using System.Linq;

namespace IntelliSun.Helpers.Aco
{
    public delegate void RefFunc<TRef>(ref TRef outRef);

    public static class Rax
    {
        public static bool IfMatrix<T>(Func<T, bool> predicate, params T[] matrix)
        {
            return matrix.Any(predicate);
        }

        public static bool EqualsAny<T>(T obj, params T[] matrix)
        {
            return IfMatrix(i => obj.Equals(i), matrix);
        }

        public static bool Switch(ref bool state)
        {
            var current = state;
            state = !state;

            return current;
        }

        public static void Switch(ref bool state, Action action)
        {
            if (state)
                action();

            state = !state;
        }

        public static void SwitchOnce(ref bool state, Action action)
        {
            if (!state)
                action();

            state = false;
        }

        public static TRef Out<TRef>(RefFunc<TRef> func)
        {
            TRef refInstance = default(TRef);
            func(ref refInstance);

            return refInstance;
        }

        public static TRes If<TRes>(bool predicate, Func<TRes> trueValue, Func<TRes> falseValue)
        {
            return (predicate ? trueValue : falseValue)();
        }

        public static TRes ValueOrDefault<T, TRes>(T obj, Func<T, TRes> getter)
            where T : class 
        {
            return obj == null ? default(TRes) : getter(obj);
        }
    }
}