using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IntelliSun.IO
{
    public class ObservableTextWriter : TextWriter, IObservable
    {
        private readonly Encoding encoding;
        private readonly List<IDataObserver> observers;

        public ObservableTextWriter()
        {
            this.encoding = Encoding.UTF8;
            this.observers = new List<IDataObserver>();
        }

        public override void Write(string value)
        {
            observers.ForEach(x => {
                if (x != null)
                    x.SendData(value);
            });
        }

        public override void WriteLine()
        {
            this.Write(Environment.NewLine);
        }

        public override void WriteLine(string value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public void AddObserver(IDataObserver observer)
        {
            if (observers != null)
                this.observers.Add(observer);
        }

        public override Encoding Encoding
        {
            get { return this.encoding; }
        }

        public List<IDataObserver> Observers
        {
            get { return this.observers; }
        }
    }
}
