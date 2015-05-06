using System;

namespace IntelliSun.Reflection
{
    public static class MemberExtenderExtensions
    {
        public static TResult? GetAttributeValueNullable<TAttr, TResult>(this MemberInfoExtenderBase extender, Func<TAttr, TResult> selector)
            where TAttr : Attribute
            where TResult : struct
        {
            if (extender.HasAttribute<TAttr>())
                return extender.GetAttributeValue(selector);

            return null;
        }
    }
}
