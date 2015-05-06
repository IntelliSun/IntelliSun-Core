using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public struct MemberAttributesPair
    {
        private readonly MemberInfo member;
        private readonly Attribute[] attributes;

        public MemberAttributesPair(MemberInfo member, params Attribute[] attributes)
        {
            this.member = member;
            this.attributes = attributes;
        }

        public MemberInfo Member
        {
            get { return this.member; }
        }

        public Attribute Attribute
        {
            get
            {
                return this.HasAttributes
                    ? this.attributes[0]
                    : default(Attribute);
            }
        }

        public Attribute[] Attributes
        {
            get { return this.attributes; }
        }

        public bool HasAttributes
        {
            get { return this.attributes.Length != 0; }
        }

        public static implicit operator MemberInfo(MemberAttributesPair pair)
        {
            return pair.Member;
        }

        public static implicit operator Attribute(MemberAttributesPair pair)
        {
            return pair.Attribute;
        }

        public static implicit operator Attribute[](MemberAttributesPair pair)
        {
            return pair.Attributes;
        }
    }

    public struct MemberAttributesPair<T>
        where T : Attribute
    {
        private readonly MemberInfo member;
        private readonly T[] attributes;

        public MemberAttributesPair(MemberInfo member, params T[] attributes)
        {
            this.member = member;
            this.attributes = attributes;
        }

        public MemberInfo Member
        {
            get { return this.member; }
        }

        public T Attribute
        {
            get
            {
                return this.HasAttributes
                    ? this.attributes[0]
                    : default(T);
            }
        }

        public T[] Attributes
        {
            get { return this.attributes; }
        }

        public bool HasAttributes
        {
            get { return this.attributes.Length != 0; }
        }

        public static implicit operator MemberInfo(MemberAttributesPair<T> pair)
        {
            return pair.Member;
        }

        public static implicit operator T(MemberAttributesPair<T> pair)
        {
            return pair.Attribute;
        }

        public static implicit operator T[](MemberAttributesPair<T> pair)
        {
            return pair.Attributes;
        }
    }
}