using System;

namespace IntelliSun.Reflection
{
    public class TypeInfoExtender : MemberInfoExtender
    {
        private readonly Type type;

        public TypeInfoExtender(Type type)
            : this(type, true)
        {

        }

        public TypeInfoExtender(Type type, bool loadInheritedData)
            : base(type, loadInheritedData)
        {
            this.type = type;
        }

        public Type Type
        {
            get { return this.type; }
        }
    }
}