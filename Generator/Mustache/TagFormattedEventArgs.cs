using System;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds the information about a tag that's been converted to text.
    /// </summary>
    public class TagFormattedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of a TagFormattedEventArgs.
        /// </summary>
        /// <param name="key">The fully-qualified key.</param>
        /// <param name="value">The formatted value being extended.</param>
        /// <param name="isExtension">Specifies whether the key was found within triple curly braces.</param>
        internal TagFormattedEventArgs(string key, string value, bool isExtension)
        {
            Key = key;
            Substitute = value;
            IsExtension = isExtension;
        }

        /// <summary>
        /// Gets the fully-qualified key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets whether the key appeared within triple curly braces.
        /// </summary>
        public bool IsExtension { get; private set; }

        /// <summary>
        /// Gets or sets the object to use as the substitute.
        /// </summary>
        public string Substitute { get; set; }
    }
}
