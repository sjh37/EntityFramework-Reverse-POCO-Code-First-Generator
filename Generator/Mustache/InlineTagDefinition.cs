using System.Collections.Generic;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that cannot contain inner text.
    /// </summary>
    public abstract class InlineTagDefinition : TagDefinition
    {
        /// <summary>
        /// Initializes a new instance of an InlineTagDefinition.
        /// </summary>
        /// <param name="tagName">The name of the tag being defined.</param>
        protected InlineTagDefinition(string tagName)
            : base(tagName)
        {
        }

        /// <summary>
        /// Initializes a new instance of an InlineTagDefinition.
        /// </summary>
        /// <param name="tagName">The name of the tag being defined.</param>
        /// <param name="isBuiltin">Specifies whether the tag is a built-in tag.</param>
        internal InlineTagDefinition(string tagName, bool isBuiltin)
            : base(tagName, isBuiltin)
        {
        }

        /// <summary>
        /// Gets or sets whether the tag can have content.
        /// </summary>
        /// <returns>True if the tag can have a body; otherwise, false.</returns>
        protected override bool GetHasContent()
        {
            return false;
        }

        /// <summary>
        /// Gets the parameters that are used to create a child context.
        /// </summary>
        /// <returns>The parameters that are used to create a child context.</returns>
        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }
    }
}
