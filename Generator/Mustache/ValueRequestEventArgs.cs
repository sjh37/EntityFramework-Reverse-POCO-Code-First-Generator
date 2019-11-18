using System;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Holds the value that a context variable is set to.
    /// </summary>
    public class ValueRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the value being set.
        /// </summary>
        public object Value { get; set; }
    }
}
