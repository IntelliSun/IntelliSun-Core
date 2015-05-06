using System;

namespace IntelliSun.Reflection.Unify
{
    public class ReflectData<T> : IReflectData<T>
    {
        private readonly Func<IReflectInfo, T> dataGetter;

        public ReflectData(Func<IReflectInfo, T> dataGetter)
        {
            if (dataGetter == null)
                throw new ArgumentNullException("dataGetter");

            this.dataGetter = dataGetter;
        }

        public T GetData(IReflectInfo reflectInfo)
        {
            return this.dataGetter(reflectInfo);
        }
    }
}