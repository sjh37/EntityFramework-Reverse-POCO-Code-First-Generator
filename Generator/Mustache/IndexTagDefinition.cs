using System.Collections.Generic;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Defines a tag that outputs the current index within an each loop.
    /// </summary>
    internal sealed class IndexTagDefinition : InlineTagDefinition
    {
        /// <summary>
        /// Initializes a new instance of an IndexTagDefinition.
        /// </summary>
        public IndexTagDefinition()
            : base("index", true)
        {
        }

        /// <summary>
        /// Gets the text to output.
        /// </summary>
        /// <param name="writer">The writer to write the output to.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="contextScope">Extra data passed along with the context.</param>
        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            object index;
            if (contextScope.TryFind("index", out index))
            {
                writer.Write(index);
            }
        }
    }
}
