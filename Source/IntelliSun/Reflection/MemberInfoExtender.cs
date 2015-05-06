using System;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class MemberInfoExtender : MemberInfoExtenderBase
    {
        public MemberInfoExtender(MemberInfo memberInfo)
            : this(memberInfo, true)
        {
            
        }

        public MemberInfoExtender(MemberInfo memberInfo, bool loadInheritedData)
            : base(memberInfo, loadInheritedData)
        {
        }

        protected override Attribute[] GetAttributes(bool inherit)
        {
            return this.MemberInfo.GetCustomAttributes(inherit).Cast<Attribute>().ToArray();
        }
    }
}
