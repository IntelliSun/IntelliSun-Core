using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IntelliSun.IO
{
    [DebuggerDisplay("Content = {Content}")]
    public abstract class IniObject
    {
        private List<string> comments;

        protected IniObject()
        {
            this.comments = new List<string>();
        }

        public void AddComment(string text)
        {
            this.comments.Add(text);
        }

        public void AddComments(IEnumerable<string> comments)
        {
            this.comments.AddRange(comments);
        }

        public void RemoveComment(int id)
        {
            this.comments.RemoveAt(id);
        }

        public void ClearComments()
        {
            this.comments.Clear();
        }

        public string GetString()
        {
            return this.GetString(IniParameters.Default);
        }

        public abstract string GetString(IniParameters parameters);

        protected string GetCommentsString(IniParameters parameters)
        {
            var blr = new StringBuilder();
            var cc = (char)parameters.CommentChar;
            foreach (var comment in comments)
            {
                blr.AppendLine(cc + comment);
            }

            return blr.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IniObject))
                return false;

            if (obj == this)
                return true;

            var eq = (IniObject)obj;
            return eq.Content.Equals(this.Content);
        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }

        protected List<string> GetComments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public string[] Comments
        {
            get { return this.comments.ToArray(); }
            set {
                this.comments = value.ToList();
            }
        }

        public virtual string Content { get; set; }
    }
}
