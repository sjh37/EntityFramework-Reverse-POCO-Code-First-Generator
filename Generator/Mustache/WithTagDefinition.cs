using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that changes the scope to the object passed as an argument.
    /// </summary>
    internal sealed class WithTagDefinition : ContentTagDefinition
    {
        private const string WithTag = "with";
        private const string ContextParameter = "context";
        private static readonly TagParameter[] InnerContextTags = { new TagParameter(ContextParameter) { IsRequired = true } };

        /// <summary>
        /// Initializes a new instance of a WithTagDefinition.
        /// </summary>
        public WithTagDefinition()
            : base(WithTag, true)
        {
        }

        /// <summary>
        /// Gets whether the tag only exists within the scope of its parent.
        /// </summary>
        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        /// <summary>
        /// Gets the parameters that can be passed to the tag.
        /// </summary>
        /// <returns>The parameters.</returns>
        protected override IEnumerable<TagParameter> GetParameters() => InnerContextTags;

        /// <summary>
        /// Gets the parameters that are used to create a new child context.
        /// </summary>
        /// <returns>The parameters that are used to create a new child context.</returns>
        public override IEnumerable<TagParameter> GetChildContextParameters() => InnerContextTags;

        /// <summary>
        /// Gets the context to use when building the inner text of the tag.
        /// </summary>
        /// <param name="writer">The text writer passed</param>
        /// <param name="keyScope">The current key scope.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="contextScope">The context scope.</param>
        /// <returns>The scope to use when building the inner text of the tag.</returns>
        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer, 
            Scope keyScope, 
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            object contextSource = arguments[ContextParameter];
            NestedContext context = new NestedContext() 
            { 
                KeyScope = keyScope.CreateChildScope(contextSource), 
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }
    }
}
