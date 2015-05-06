using System;

namespace IntelliSun
{
    public class LambdaDataObserver<T> : DataObserver<T>
    {
        private readonly Action<T> expression;

        private LambdaDataObserver(Action<T> expression)
        {
            this.expression = expression;
        }

        public static LambdaDataObserver Create(Action<object> expression)
        {
            return LambdaDataObserver.Create(expression);
        }

        public static LambdaDataObserver<T> Create(Action<T> expression)
        {
            return new LambdaDataObserver<T>(expression);
        }

        public override void SendData(T data)
        {
            this.expression(data);
        }
    }
}