using System;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Applies the values of an object to the format plan, generating a string.
    /// </summary>
    internal interface IMustacheGenerator
    {
        /// <summary>
        /// Generates the text when applying the format plan.
        /// </summary>
        /// <param name="writer">The text writer to send all text to.</param>
        /// <param name="keyScope">The current lexical scope of the keys.</param>
        /// <param name="contextScope">The data associated to the context.</param>
        /// <param name="postProcessor">A function to apply after a substitution is made.</param>
        /// <returns>The generated text.</returns>
        void GetText(TextWriter writer, Scope keyScope, Scope contextScope, Action<Substitution> postProcessor);
    }
}
