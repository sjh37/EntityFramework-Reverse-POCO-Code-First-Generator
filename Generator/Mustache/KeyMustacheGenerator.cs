using System;
using System.IO;
using System.Text;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Substitutes a key placeholder with the textual representation of the associated object.
    /// </summary>
    internal sealed class KeyMustacheGenerator : IMustacheGenerator
    {
        private readonly string _key;
        private readonly string _format;
        private readonly bool _isVariable;
        private readonly bool _isExtension;

        /// <summary>
        /// Initializes a new instance of a KeyGenerator.
        /// </summary>
        /// <param name="key">The key to substitute with its value.</param>
        /// <param name="alignment">The alignment specifier.</param>
        /// <param name="formatting">The format specifier.</param>
        /// <param name="isExtension">Specifies whether the key was found within triple curly braces.</param>
        public KeyMustacheGenerator(string key, string alignment, string formatting, bool isExtension)
        {
            if (key.StartsWith("@"))
            {
                _key = key.Substring(1);
                _isVariable = true;
            }
            else
            {
                _key = key;
                _isVariable = false;
            }
            _format = getFormat(alignment, formatting);
            _isExtension = isExtension;
        }

        private static string getFormat(string alignment, string formatting)
        {
            StringBuilder formatBuilder = new StringBuilder();
            formatBuilder.Append("{0");
            if (!String.IsNullOrWhiteSpace(alignment))
            {
                formatBuilder.Append(",");
                formatBuilder.Append(alignment.TrimStart('+'));
            }
            if (!String.IsNullOrWhiteSpace(formatting))
            {
                formatBuilder.Append(":");
                formatBuilder.Append(formatting);
            }
            formatBuilder.Append("}");
            return formatBuilder.ToString();
        }

        void IMustacheGenerator.GetText(TextWriter writer, Scope scope, Scope context, Action<Substitution> postProcessor)
        {
            object value = _isVariable ? context.Find(_key, _isExtension) : scope.Find(_key, _isExtension);
            string result = String.Format(writer.FormatProvider, _format, value);
            Substitution substitution = new Substitution()
            {
                Key = _key,
                Substitute = result,
                IsExtension = _isExtension
            };
            postProcessor(substitution);
            writer.Write(substitution.Substitute);
        }
    }
}
