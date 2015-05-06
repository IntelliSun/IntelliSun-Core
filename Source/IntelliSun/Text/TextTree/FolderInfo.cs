using System;

namespace IntelliSun.Text
{
    [Obsolete]
    public struct FolderInfo
    {
        private readonly string open;
        private readonly string colse;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public FolderInfo(string open, string colse)
            : this()
        {
            this.open = open;
            this.colse = colse;

            this.NodeType = TextNodeTypes.Folder;
        }

        public string Open
        {
            get { return this.open; }
        }

        public string Colse
        {
            get { return this.colse; }
        }

        public TextNodeType NodeType { get; set; }
    }

    public interface ISymbolLocator
    {
        bool Match(char current);
    }

    public enum SymbolLocatorResult
    {
        Default,
        Open,
        Inner,
        Close
    }

    public interface ITranslatorContext : ISymbolContext
    {
        void TranslateUp();

        void TranslateDown();

        void GoTop();

        void GoButtom();

        void TranslateIn(INodeBuilder node);

        void TranslateOut();
    }

    public interface ISymbolContext
    {
        char Symbol { get; }
    }

    public interface ITranslator : ISymbolLocator
    {
        void Translate(ITranslatorContext context);
    }

    public interface IFolder : ISymbolLocator
    {
        void Open(ITranslatorContext context);
        void Close(ITranslatorContext context);
    }
}