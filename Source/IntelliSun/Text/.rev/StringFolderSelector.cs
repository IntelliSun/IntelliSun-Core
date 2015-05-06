using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliSun.Text
{
    public class StringFolderSelector : StringSelector
    {
        private char folderOpen;
        private char folderClose;
        private EscapeInfo escapeInfo;

        public StringFolderSelector(char folderMark)
            : this(folderMark, folderMark)
        {
        }

        public StringFolderSelector(char folderOpen, char folderClose)
            : this(folderOpen, folderClose, null)
        {

        }

        public StringFolderSelector(char folderOpen, char folderClose, char escapeChar)
            : this(folderOpen, folderClose, (char?)escapeChar)
        {

        }

        public StringFolderSelector(char folderOpen, char folderClose, char? escapeChar)
        {
            this.folderOpen = folderOpen;
            this.folderClose = folderClose;

            this.escapeInfo = new EscapeInfo() {
                UseEscape = (escapeChar.HasValue),
                EscapeChar = escapeChar ?? '\0',
                DoubleMarkEscape = false
            };
        }

        public StringFolderSelector(char folderOpen, char folderClose, bool doubleMarkEscape)
        {
            this.folderOpen = folderOpen;
            this.folderClose = folderClose;

            this.escapeInfo = new EscapeInfo() {
                UseEscape = doubleMarkEscape,
                DoubleMarkEscape = doubleMarkEscape
            };
        }

        public override StringSelectionResult Select(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            var selection = new StringBuilder();

            var folderCount = 0;
            var hasFolder = false;
            var success = false;

            for (int i = 0; i < text.Length; i++)
            {
                var cast = text[i];
                if (cast == folderOpen)
                {
                    folderCount++;
                    hasFolder=true;
                } 
                
                if (hasFolder)
                {
                    selection.Append(cast);

                    if (cast == folderClose)
                    {
                        folderCount--;
                        if(folderCount <= 0)
                        {
                            success = true;
                            break;
                        }
                    }
                }
            }

            return new StringSelectionResult(text, success);
        }

        private class EscapeInfo
        {
            public bool UseEscape { get; set; }
            public char EscapeChar { get; set; }
            public bool DoubleMarkEscape { get; set; }
        }
    }
}
