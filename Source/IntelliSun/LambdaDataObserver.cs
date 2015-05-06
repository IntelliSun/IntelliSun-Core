using System;

namespace IntelliSun
{
    public class LambdaDataObserver : IDataObserver
    {
        private readonly Action<object> expression;

        private LambdaDataObserver(Action<object> expression)
        {
            this.expression = expression;
        }

        public static LambdaDataObserver Create(Action<object> expression)
        {
            return new LambdaDataObserver(expression);
        }

        public static LambdaDataObserver<T> Create<T>(Action<T> expression)
        {
            return LambdaDataObserver<T>.Create(expression);
        }

        public void SendData(object data)
        {
            this.expression(data);
        }
    }
}