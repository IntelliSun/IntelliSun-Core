using System;

namespace IntelliSun.Text
{
    public static class TextNodeTypes
    {
        public const int DefaltIndex = 0;
        public const string DefaltName = "@node";

        public const int FolderIndex = 0xE2;
        public const string FolderName = "@folder";

        public const int GroupIndex = 0xE3;
        public const string GroupName = "@group";

        public static TextNodeType Default
        {
            get { return new TextNodeType(DefaltIndex, DefaltName); }
        }

        public static TextNodeType Folder
        {
            get { return new TextNodeType(FolderIndex, FolderName); }
        }

        public static TextNodeType Group
        {
            get { return new TextNodeType(GroupIndex, GroupName); }
        }
    }
}