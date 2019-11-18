using System;
using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that outputs a newline.
    /// </summary>
    internal sealed class NewlineTagDefinition : InlineTagDefinition
    {
        private const string NewlineTag = "newline";

        /// <summary>
        /// Initializes a new instance of an NewlineTagDefinition.
        /// </summary>
        public NewlineTagDefinition()
            : base(NewlineTag)
        {
        }

        /// <summary>
        /// Gets the text to output.
        /// </summary>
        /// <param name="writer">The writer to write the output to.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="context">Extra data passed along with the context.</param>
        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write(Environment.NewLine);
        }
    }
}
