using System;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds the information about a key that was found.
    /// </summary>
    public class KeyFoundEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of a KeyFoundEventArgs.
        /// </summary>
        /// <param name="key">The fully-qualified key.</param>
        /// <param name="value">The object to use as the substitute.</param>
        /// <param name="isExtension">Specifies whether the key was found within triple curly braces.</param>
        internal KeyFoundEventArgs(string key, object value, bool isExtension)
        {
            Key = key;
            Substitute = value;
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
        public object Substitute { get; set; }
    }
}
