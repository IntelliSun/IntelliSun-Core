using System;
using System.Collections.Generic;
using System.Text;
using IntelliSun.Helpers;

namespace IntelliSun.IO
{
    public class IniHeader : IniObject
    {
        public IniHeader()
        {

        }

        public IniHeader(IEnumerable<string> comments)
            : this()
        {
            this.GetComments.AddRange(comments);
        }


        public override string GetString(IniParameters parameters)
        {
            return this.GetString(parameters, DateTime.Now.Ticks + ".ini", DateTime.Now);
        }

        public string GetString(IniParameters parameters, string file, DateTime dateTime)
        {
            var blr = new StringBuilder();

            var cc = (char)parameters.CommentChar;
            foreach (var comment in Comments)
            {
                blr.AppendLine(cc + StringHelper.TokenFormat(comment,
                    new Dictionary<char, string> {
                        { 't', dateTime.ToShortTimeString() },
                        { 'd', dateTime.ToShortDateString() },
                        { 'f', file }
                    }));
            }

            return blr.ToString();
        }
    }
}
