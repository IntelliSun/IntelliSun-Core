using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal abstract class ReflectMemberInfo : ReflectInfo
    {
        private readonly MemberInfo memberInfo;

        protected ReflectMemberInfo(MemberInfo memberInfo)
            : base(memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public override string Name
        {
            get { return this.memberInfo.Name; }
        }

        public override object ReflectionObject
        {
            get { return this.memberInfo; }
        }
    }
}