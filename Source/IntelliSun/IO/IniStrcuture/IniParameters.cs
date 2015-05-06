using System;

namespace IntelliSun.IO
{
    public struct IniParameters
    {
        public static readonly IniParameters Default =
            new IniParameters(IniKeyTypeIdentifier.LowerLetterColon, IniCommentChar.Semicolon);

        public IniParameters(IniKeyTypeIdentifier keyTypeIdentifier,
            IniCommentChar commentChar)
            : this()
        {
            this.KeyTypeIdentifier = keyTypeIdentifier;
            this.CommentChar = commentChar;
        }

        public IniKeyTypeIdentifier KeyTypeIdentifier { get; set; }
        public IniCommentChar CommentChar { get; set; }
    }
}
