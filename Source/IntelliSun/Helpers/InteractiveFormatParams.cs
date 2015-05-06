using System;

namespace IntelliSun.Helpers
{
    public class InteractiveFormatParams
    {
        public const string DefaultTemplate = "-{0}";

        private readonly Func<IInteractiveFormatArgs, string> accessor;
        private readonly IInteractiveFormatArgs argsPreset;

        protected InteractiveFormatParams()
        {
            this.accessor = null;
            this.argsPreset = null;
            this.Template = DefaultTemplate;
        }

        public InteractiveFormatParams(Func<IInteractiveFormatArgs, string> accessor)
            : this()
        {
            this.accessor = accessor;
        }

        public InteractiveFormatParams(Func<IInteractiveFormatArgs, string> accessor, IInteractiveFormatArgs argsPreset)
            : this()
        {
            this.accessor = accessor;
            this.argsPreset = argsPreset;
        }

        public InteractiveFormatParams(Func<IInteractiveFormatArgs, string> accessor, IInteractiveFormatArgs argsPreset, string template)
            : this()
        {
            this.accessor = accessor;
            this.argsPreset = argsPreset;
            this.Template = template;
        }

        public Func<IInteractiveFormatArgs, string> Accessor
        {
            get { return accessor; }
        }

        public IInteractiveFormatArgs ArgsPreset
        {
            get { return argsPreset; }
        }

        public string Template { get; set; }
    }
}