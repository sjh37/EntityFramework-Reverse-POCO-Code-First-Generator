using System;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Generates a static block of text.
    /// </summary>
    internal sealed class StaticMustacheGenerator : IMustacheGenerator
    {
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of a StaticGenerator.
        /// </summary>
        public StaticMustacheGenerator(string value, bool removeNewLines)
        {
            if (removeNewLines)
            {
                this.value = value.Replace(Environment.NewLine, String.Empty);
            }
            else
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the static text.
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        void IMustacheGenerator.GetText(TextWriter writer, Scope scope, Scope context, Action<Substitution> postProcessor)
        {
            writer.Write(Value);
        }
    }
}
