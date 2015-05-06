using System;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.Text
{
    public class BasicNodeBuilder : INodeBuilder
    {
        private readonly string content;

        public BasicNodeBuilder(string content)
        {
            this.content = content;
        }

        public TextNode Build(INodeBuilderContext context)
        {
            if (!context.ChildNodes.IsNullOrEmpty())
                return this.BuildSingleNode();

            return this.BuildGroupNode(context.Manager, context.ChildNodes);
        }

        private TextNode BuildGroupNode(INodeBuildManager manager, IEnumerable<INodeBuilder> childNodes)
        {
            return new GroupTextNode(this.content, childNodes.Select(builder => {
                var childContext = manager.ChildContext(builder);
                return builder.Build(childContext);
            }).ToArray());
        }

        private TextNode BuildSingleNode()
        {
            return new SingleTextNode(this.content);
        }
    }
}