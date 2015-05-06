using System;

namespace IntelliSun.Helpers
{
    internal class StaticSequentialExpression : ISequentialExpression
    {
        private readonly string content;

        public StaticSequentialExpression(string content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            this.content = content;
        }

        public string Resolve(object[] args, int index)
        {
            return this.content;
        }
    }
}