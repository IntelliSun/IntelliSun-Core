using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliSun.Helpers.ObjectExtensions;

namespace IntelliSun.Text
{
    public class StringMapper
    {
        private string content;

        private int currentIndex;

        private bool recordBacklog;
        private StringBuilder backlog;

        public StringMapper(string content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            this.content = content;

            this.currentIndex = 0;

            this.recordBacklog = false;
            this.backlog = new StringBuilder();
        }

        public bool IsLongerThan(int value)
        {
            return (value == this.Length);
        }

        public bool HasNext()
        {
            return (this.currentIndex + 1 < this.Length);
        }

        public bool HasNext(char value)
        {
            if (!HasNext())
                return false;

            return (this.content[currentIndex + 1] == value);  
        }

        public bool HasNext(Predicate<char> predicate)
        {
            if (!HasNext())
                return false;

            return predicate(this.content[currentIndex + 1]);
        }

        public bool HasPrevious()
        {
            return (this.currentIndex > 0);
        }

        public bool HasPrevious(char value)
        {
            if (!HasPrevious())
                return false;

            return (this.content[currentIndex - 1] == value);
        }

        public bool HasPrevious(Predicate<char> predicate)
        {
            if (!HasPrevious())
                return false;

            return predicate(this.content[currentIndex - 1]);
        }

        public char Pull()
        {
            return this.content[currentIndex];
        }

        public void StartBacklog()
        {
            this.recordBacklog = true;
        }

        public void ClearBacklog()
        {
            this.backlog.Clear();
        }

        public void StopBacklog()
        {
            this.recordBacklog = false;
        }

        public void ResetBacklog()
        {
            this.ClearBacklog();
            this.recordBacklog = false;
        }

        public void MoveWhile(Predicate<char> predicate)
        {
            this.MoveWhile(predicate, (c) => this._MoveNext());
        }

        public void MoveWhile(Predicate<char> predicate, Action<char> task)
        {
            while (predicate(content[currentIndex]))
            {
                this._MoveNext();
                task(this.Pull());
            }
        }

        public void MoveToEnd()
        {
            this.MoveToEnd(false);
        }

        public void MoveToEnd(bool jump)
        {
            if (jump)
                this.currentIndex = this.Length - 1;
            else
            {
                while (this.HasNext())
                    this._MoveNext();
            }
        }

        public char MoveNext()
        {
            char c;

            if(!this.TryMoveNext(out c))
                throw new IndexOutOfRangeException();

            return c;
        }

        public bool TryMoveNext(out char nextChar)
        {
            nextChar = '\r';

            if (!this.HasNext())
                return false;

            _MoveNext();
            nextChar = this.content[currentIndex];
            return true;
        }

        private void _MoveNext()
        {
            this.currentIndex++;
            if(this.recordBacklog)
                backlog.Append(this.Pull());
        }

        public StringSelectionResult Select(StringSelector selector)
        {
            return selector.Select(content, currentIndex);
        }

        public string Content
        {
            get { return this.content; }
        }

        public int Length
        {
            get { return this.content.Length; }
        }
    }
}
