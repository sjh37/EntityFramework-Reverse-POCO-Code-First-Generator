using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds the objects to use when processing a child context of another tag.
    /// </summary>
    public sealed class NestedContext
    {
        /// <summary>
        /// Initializes a new instance of a NestedContext.
        /// </summary>
        public NestedContext()
        {
        }

        /// <summary>
        /// Gets or sets the writer to use when generating the child context.
        /// </summary>
        /// <remarks>Setting the writer to null will indicate that the tag's writer should be used.</remarks>
        public TextWriter Writer { get; set; }

        /// <summary>
        /// Gets or sets whether the text sent to the returned writer needs to be added
        /// to the parent tag's writer. This should be false if the parent writer is
        /// being returned or is being wrapped.
        /// </summary>
        public bool WriterNeedsConsidated { get; set; }

        /// <summary>
        /// Gets or sets the key scope to use when generating the child context.
        /// </summary>
        /// <remarks>Setting the scope to null will indicate that the current scope should be used.</remarks>
        public Scope KeyScope { get; set; }

        /// <summary>
        /// Gets or sets data associated with the context.
        /// </summary>
        public Scope ContextScope { get; set; }
    }
}
