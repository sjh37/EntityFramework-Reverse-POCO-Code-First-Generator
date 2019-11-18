using System;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds the information descibing a key that is found in a template.
    /// </summary>
    public class PlaceholderFoundEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of a PlaceholderFoundEventArgs.
        /// </summary>
        /// <param name="key">The key that was found.</param>
        /// <param name="alignment">The alignment that will be applied to the substitute value.</param>
        /// <param name="formatting">The formatting that will be applied to the substitute value.</param>
        /// <param name="isExtension">Indicates whether the placeholder was found within triple curly braces.</param>
        /// <param name="context">The context where the placeholder was found.</param>
        internal PlaceholderFoundEventArgs(string key, string alignment, string formatting, bool isExtension, Context[] context)
        {
            Key = key;
            Alignment = alignment;
            Formatting = formatting;
            Context = context;
        }

        /// <summary>
        /// Gets or sets the key that was found.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the alignment that will be applied to the substitute value.
        /// </summary>
        public string Alignment { get; set; }

        /// <summary>
        /// Gets or sets the formatting that will be applied to the substitute value.
        /// </summary>
        public string Formatting { get; set; }

        /// <summary>
        /// Gets or sets whether the placeholder was found within triple curly braces.
        /// </summary>
        public bool IsExtension { get; set; }

        /// <summary>
        /// Gets the context where the placeholder was found.
        /// </summary>
        public Context[] Context { get; private set; }
    }
}
