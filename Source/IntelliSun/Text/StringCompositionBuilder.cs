using System;

namespace IntelliSun.Text
{
    public static class StringCompositionBuilder
    {
        public static ICompositionBuilderContext New()
        {
            var provider = new StringCompositionProvider();
            return new CompositionBuilderContext(provider);
        }
    }
}
