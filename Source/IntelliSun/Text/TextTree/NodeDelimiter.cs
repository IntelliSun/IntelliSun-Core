using System;

namespace IntelliSun.Text
{
    public class NodeDelimiter : ExpressionTextLocator, IDelimiter
    {
        private readonly DelimiterFactory factory;

        protected NodeDelimiter(string expression)
            : this(expression, null, true)
        {
            
        }

        public NodeDelimiter(string expression, DelimiterFactory factory)
            : this(expression, factory, false)
        {
            
        }

        public NodeDelimiter(string expression, DelimiterFactory factory, bool protectedCall)
            : base(expression)
        {
            if (factory == null && !protectedCall)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }

        public virtual void Parse(IDelimiterContext context)
        {
            if(this.factory != null)
                this.factory(context, this.Parameters);
        }
    }
}