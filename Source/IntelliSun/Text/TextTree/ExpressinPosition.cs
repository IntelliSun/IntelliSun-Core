using System;

namespace IntelliSun.Text
{
    internal struct ExpressinPosition
    {
        public int Index;
        public int Length;
        public int Parameter;
        public ExpressionPart Part;

        public ExpressinPosition(int index)
            : this(index, 1)
        {
            
        }

        public ExpressinPosition(int index, int length)
        {
            this.Index = index;
            this.Length = length;
            this.Parameter = -1;
            this.Part = ExpressionPart.Text;
        }
    }
}