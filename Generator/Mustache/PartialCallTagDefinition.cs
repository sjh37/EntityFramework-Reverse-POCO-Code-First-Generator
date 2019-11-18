using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    public class PartialCallTagDefinition : TagDefinition
    {
        private const string PartialCallTag = ">";
        private const string NameParameter = "name";
        private const string ContextParameter = "context";
        private static readonly TagParameter[] InnerTags =
        {
            new TagParameter(NameParameter) { IsRequired = true },
            new TagParameter(ContextParameter) { IsRequired = false }
        };
        private static readonly TagParameter[] InnerContextTags =
        {
            new TagParameter(ContextParameter) { IsRequired = false }
        };

        public PartialCallTagDefinition()
            : base(PartialCallTag, true) { }

        protected override IEnumerable<TagParameter> GetParameters() => InnerTags;

        public override IEnumerable<TagParameter> GetChildContextParameters() => InnerContextTags;

        protected override bool GetHasContent() => false;

        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer,
            Scope keyScope,
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            object contextSource = arguments[ContextParameter];

            Scope scope;
            if (contextSource == null)
                scope = keyScope.CreateChildScope();
            else
                scope = keyScope.CreateChildScope(contextSource);

            NestedContext context = new NestedContext()
            {
                KeyScope = scope,
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }
    }
}
