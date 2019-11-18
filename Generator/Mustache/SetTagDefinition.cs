using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that declares a named value in the current context.
    /// </summary>
    internal sealed class SetTagDefinition : InlineTagDefinition
    {
        private const string nameParameter = "name";
        private static readonly TagParameter name = new TagParameter(nameParameter) { IsRequired = true };

        /// <summary>
        /// Initializes a new instance of an SetTagDefinition.
        /// </summary>
        public SetTagDefinition()
            : base("set", true)
        {
        }

        protected override bool GetIsSetter()
        {
            return true;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { name };
        }

        /// <summary>
        /// Gets the text to output.
        /// </summary>
        /// <param name="writer">The writer to write the output to.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="contextScope">Extra data passed along with the context.</param>
        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            string name = (string)arguments[nameParameter];
            contextScope.Set(name);
        }
    }
}
