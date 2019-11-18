using System.Collections.Generic;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a pseudo tag that wraps the entire content of a format string.
    /// </summary>
    internal sealed class MasterTagDefinition : ContentTagDefinition
    {
        /// <summary>
        /// Initializes a new instance of a MasterTagDefinition.
        /// </summary>
        public MasterTagDefinition()
            : base(string.Empty, true)
        {
        }

        /// <summary>
        /// Gets whether the tag only exists within the scope of its parent.
        /// </summary>
        protected override bool GetIsContextSensitive()
        {
            return true;
        }

        /// <summary>
        /// Gets the name of the tags that indicate that the tag's context is closed.
        /// </summary>
        /// <returns>The tag names.</returns>
        protected override IEnumerable<string> GetClosingTags() => Constants.EmptyTags;

        /// <summary>
        /// Gets the parameters that are used to create a new child context.
        /// </summary>
        /// <returns>The parameters that are used to create a new child context.</returns>
        public override IEnumerable<TagParameter> GetChildContextParameters() => Constants.EmptyTagParameters;
    }
}
