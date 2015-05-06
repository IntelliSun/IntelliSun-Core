using System;
using System.Linq;

namespace IntelliSun.Text
{
    public class ConsoleWriter
    {
        private readonly ConsoleWriterConfig config;
        private readonly IConsoleWriterContext root;

        public ConsoleWriter(ConsoleWriterConfig config)
        {
            this.config = config;
            this.root = new ConsoleWriterContext(null, this.config);
        }

        public ConsoleWriterConfig Config
        {
            get { return this.config; }
        }

        public IConsoleWriterContext Root
        {
            get { return this.root; }
        }
    }

    public interface IConsoleWriterContext
    {
        IConsoleWriterContext Write(string value);
        IConsoleWriterContext Write(object value);
        IConsoleWriterContext Write(string format, object arg);
        IConsoleWriterContext Write(string format, params object[] args);

        IConsoleWriterContext WriteLine();
        IConsoleWriterContext WriteLine(string value);
        IConsoleWriterContext WriteLine(object value);
        IConsoleWriterContext WriteLine(string format, object arg);
        IConsoleWriterContext WriteLine(string format, params object[] args);

        IConsoleWriterContext Write(ConsoleColor color, string value);
        IConsoleWriterContext Write(ConsoleColor color, object value);
        IConsoleWriterContext Write(ConsoleColor color, string format, object arg);
        IConsoleWriterContext Write(ConsoleColor color, string format, params object[] args);

        IConsoleWriterContext WriteLine(ConsoleColor color, string value);
        IConsoleWriterContext WriteLine(ConsoleColor color, object value);
        IConsoleWriterContext WriteLine(ConsoleColor color, string format, object arg);
        IConsoleWriterContext WriteLine(ConsoleColor color, string format, params object[] args);

        IConsoleWriterContext Tab(Action<IConsoleWriterContext> context);
        IConsoleWriterContext Tabs(uint count, Action<IConsoleWriterContext> context);

        IConsoleWriterContext Root { get; }
        IConsoleWriterContext Parent { get; }
    }

    internal class ConsoleWriterContext : IConsoleWriterContext
    {
        private readonly IConsoleWriterContext parent;
        private readonly ConsoleWriterConfig config;
        private readonly uint tabLevel;

        private bool isInline;

        public ConsoleWriterContext(IConsoleWriterContext parent, ConsoleWriterConfig config)
            : this(parent, config, 0)
        {
        }

        public ConsoleWriterContext(IConsoleWriterContext parent, ConsoleWriterConfig config, uint tabLevel)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            this.parent = parent;
            this.config = config;
            this.tabLevel = tabLevel;
        }

        public IConsoleWriterContext Write(string value)
        {
            this.StartLine();
            Console.Write(value);

            return this;
        }

        public IConsoleWriterContext Write(object value)
        {
            var text = value != null ? value.ToString() : String.Empty;

            return this.Write(text);
        }

        public IConsoleWriterContext Write(string format, object arg)
        {
            return this.Write(String.Format(format, arg));
        }

        public IConsoleWriterContext Write(string format, params object[] args)
        {
            return this.Write(String.Format(format, args));
        }

        public IConsoleWriterContext WriteLine()
        {
            this.Write(Environment.NewLine);
            this.EndLine();

            return this;
        }

        public IConsoleWriterContext WriteLine(string value)
        {
            this.Write(value);
            return this.WriteLine();
        }

        public IConsoleWriterContext WriteLine(object value)
        {
            this.Write(value);
            return this.WriteLine();
        }

        public IConsoleWriterContext WriteLine(string format, object arg)
        {
            this.Write(format, arg);
            return this.WriteLine();
        }

        public IConsoleWriterContext WriteLine(string format, params object[] args)
        {
            this.Write(format, args);
            return this.WriteLine();
        }

        public IConsoleWriterContext Write(ConsoleColor color, string value)
        {
            CallWithColor(color, () => this.Write(value));

            return this;
        }

        public IConsoleWriterContext Write(ConsoleColor color, object value)
        {
            CallWithColor(color, () => this.Write(value));

            return this;
        }

        public IConsoleWriterContext Write(ConsoleColor color, string format, object arg)
        {
            CallWithColor(color, () => this.Write(format, arg));

            return this;
        }

        public IConsoleWriterContext Write(ConsoleColor color, string format, params object[] args)
        {
            CallWithColor(color, () => this.Write(format, args));

            return this;
        }

        public IConsoleWriterContext WriteLine(ConsoleColor color, string value)
        {
            CallWithColor(color, () => this.WriteLine(value));

            return this;
        }

        public IConsoleWriterContext WriteLine(ConsoleColor color, object value)
        {
            CallWithColor(color, () => this.WriteLine(value));

            return this;
        }

        public IConsoleWriterContext WriteLine(ConsoleColor color, string format, object arg)
        {
            CallWithColor(color, () => this.WriteLine(format, arg));

            return this;
        }

        public IConsoleWriterContext WriteLine(ConsoleColor color, string format, params object[] args)
        {
            CallWithColor(color, () => this.WriteLine(format, args));

            return this;
        }

        private void StartLine()
        {
            if (!this.isInline)
                Console.Write(String.Concat(Enumerable.Repeat('\t', (int)this.tabLevel)));

            this.isInline = true;
        }

        private void EndLine()
        {
            this.isInline = false;
        }

        private static void CallWithColor(ConsoleColor color, Action call)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            call();

            Console.ForegroundColor = lastColor;
        }

        public IConsoleWriterContext Tab(Action<IConsoleWriterContext> context)
        {
            return this.Tabs(1, context);
        }

        public IConsoleWriterContext Tabs(uint count, Action<IConsoleWriterContext> context)
        {
            var instance = new ConsoleWriterContext(this, this.config, this.tabLevel + count);
            context(instance);

            return this;
        }

        public IConsoleWriterContext Root
        {
            get { return this.Parent == null ? this : this.Parent.Root; }
        }

        public IConsoleWriterContext Parent
        {
            get { return this.parent; }
        }
    }

    public class ConsoleWriterConfig
    {
        public int ColumnWidth { get; set; }
    }
}
