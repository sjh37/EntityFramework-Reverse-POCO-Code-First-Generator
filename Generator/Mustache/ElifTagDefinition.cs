using System.Collections.Generic;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that conditionally renders its content if preceding if and elif tags fail.
    /// </summary>
    internal sealed class ElifTagDefinition : ConditionTagDefinition
    {
        private const string ElifTag = "elif";
        private static readonly string[] InnerClosingTags = { "if" };

        /// <summary>
        /// Initializes a new instance of an ElifTagDefinition.
        /// </summary>
        public ElifTagDefinition()
            : base(ElifTag)
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
        /// Gets the tags that indicate the end of the current tags context.
        /// </summary>
        protected override IEnumerable<string> GetClosingTags() => InnerClosingTags;
    }
}
