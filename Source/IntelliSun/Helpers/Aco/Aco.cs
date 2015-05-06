using System;
using IntelliSun.Collections;

namespace IntelliSun.Helpers.Aco
{
    public static class FlatAcoManager
    {
        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T> trueAction)
        {
            return IfAco(obj, predicate(obj), trueAction, null);
        }

        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T> trueAction)
        {
            return IfAco(obj, predicate(), trueAction, null);
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T> trueAction)
        {
            return IfAco(obj, condition, trueAction, null);
        }

        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T, bool> action)
        {
            return IfAco(obj, predicate(obj), action);
        }

        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T, bool> action)
        {
            return IfAco(obj, predicate(), action);
        }

        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, predicate(obj), trueAction, falseAction);
        }
        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, predicate(), trueAction, falseAction);
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, condition, ((o, c) => {
                if (c)
                {
                    if (trueAction != null)
                        trueAction(o);
                } else
                {
                    if (falseAction != null)
                        falseAction(o);
                }
            }));
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T, bool> action)
        {
            action(obj, condition);
            return obj;
        }


        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, predicate(obj), trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, predicate(), trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, condition, trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, bool, TResult> action)
        {
            return IfAco(obj, predicate(obj), action);
        }

        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, bool, TResult> action)
        {
            return IfAco(obj, predicate(), action);
        }

        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, predicate(obj), trueFunc, falseFunc);
        }
        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, predicate(), trueFunc, falseFunc);
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, condition, ((o, c) => {
                if (c)
                {
                    if (trueFunc != null)
                        return trueFunc(o);
                } else
                {
                    if (falseFunc != null)
                        return falseFunc(o);
                }

                return default(TResult);
            }));
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, bool, TResult> func)
        {
            return func(obj, condition);
        }

        public static bool HasValue<T>(this T obj)
        {
            return !ReferenceEquals(obj, null);
        }

        public static TResult HasValue<T, TResult>(this T obj, Func<IAcoObject<T>, TResult> func)
        {
            return !ReferenceEquals(obj, null) ? func(obj.Aco()) : default(TResult);
        }
    }

    public static class AcoManager
    {
        public static IAcoObject<T> Aco<T>(this T obj)
        {
            if (obj is IAcoObject)
                throw new Exception("${Resources.WrappingOfAco}");

            return AcoContainer.Wrap(obj);
        }

        public static IAcoObject Aco()
        {
            return AcoContainer.Wrap();
        }

        public static bool ReturnAfIf(bool condition, Action trueAction)
        {
            return ReturnAfIf(condition, r => {
                if (r)
                    trueAction();
            });
        }

        public static bool ReturnAfIf(bool condition, Action trueAction, Action falseAction)
        {
            return ReturnAfIf(condition, r => {
                if (r)
                    trueAction();
                else
                    falseAction();
            });
        }

        public static bool ReturnAfIf(bool condition, Action<bool> action)
        {
            action(condition);
            return condition;
        }
    }

    public interface IAcoObject
    {
        TRes Return<TRes>(TRes value);
        TRes Return<TRes>(Func<TRes> func);
        TRes Return<TRes>(Func<IAcoObject, TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<IAcoObject, TRes> func);
        IAcoObject Push(params Action[] actions);
        IAcoObject Push(params Action<IAcoObject>[] actions);
    }

    public interface IAcoObject<out T> : IAcoObject
    {
        T Return();

        TRes Return<TRes>(Func<T, TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<T, TRes> func);
        new IAcoObject<T> Push(params Action[] actions);
        IAcoObject<T> Push(params Action<T>[] actions);
        T ReturnAfter(Action action);
        T ReturnAfter(Action<T> action);

        TRes Using<TRes>(Func<T, TRes> func);

        void Void(Action<T> action);
    }

    public interface IAcoExpression : IAcoObject
    {

    }

    public interface IAcoIfStatement : IAcoObject
    {

    }

    public interface IAcoIfStatement<out T> : IAcoIfStatement, IAcoObject<T>
    {

    }

    internal class AcoObject : IAcoObject
    {
        public TRes Return<TRes>(TRes value)
        {
            return value;
        }

        public TRes Return<TRes>(Func<TRes> func)
        {
            return func();
        }

        public TRes Return<TRes>(Func<IAcoObject, TRes> func)
        {
            return func(this);
        }

        public IAcoObject<TRes> Inline<TRes>(Func<TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public IAcoObject<TRes> Inline<TRes>(Func<IAcoObject, TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public IAcoObject Push(params Action[] actions)
        {
            actions.Foreach(a => Push(a));

            return this;
        }

        public IAcoObject Push(params Action<IAcoObject>[] actions)
        {
            actions.Foreach(a => Push(a));

            return this;
        }

        internal IAcoObject Push(Action action)
        {
            action();

            return this;
        }

        internal IAcoObject Push(Action<IAcoObject> action)
        {
            action(this);

            return this;
        }
    }

    internal class AcoObject<T> : AcoObject, IAcoObject<T> 
    {
        private readonly T value;

        public AcoObject(T value)
        {
            this.value = value;
        }

        public virtual T Return()
        {
            return this.value;
        }

        public TRes Return<TRes>(Func<T, TRes> func)
        {
            return func(this.Return());
        }

        public IAcoObject<TRes> Inline<TRes>(Func<T, TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public new IAcoObject<T> Push(params Action[] actions)
        {
            actions.Foreach(a => a());

            return this;
        }

        public IAcoObject<T> Push(params Action<T>[] actions)
        {
            actions.Foreach(a => a(this.Return()));

            return this;
        }

        public T ReturnAfter(Action action)
        {
            return this.ReturnAfter(t => action());
        }

        public T ReturnAfter(Action<T> action)
        {
            action(this.value);
            return this.value;
        }

        public TRes Using<TRes>(Func<T, TRes> func)
        {
            var result = func(this.value);
            this.TryDisposeValue();

            return result;
        }

        private void TryDisposeValue()
        {
            var disposable = this.value as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        public void Void(Action<T> action)
        {
            action(this.value);
        }
    }

    internal class AcoAcoObject<T> : AcoObject<T>
    {
        private readonly IAcoObject<T> value;

        public AcoAcoObject(IAcoObject<T> value)
            : base(value.Return())
        {
            this.value = value;
        }

        public override T Return()
        {
            return this.value.Return();
        }
    }

    internal static class AcoContainer
    {
        public static IAcoObject Wrap()
        {
            return new AcoObject();
        }

        public static IAcoObject<T> Wrap<T>(T obj)
        {
            return new AcoObject<T>(obj);
        }

        public static IAcoObject<T> Wrap<T>(IAcoObject<T> aco)
        {
            return new AcoAcoObject<T>(aco);
        }
    }
}
