using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Efrpg.Mustache
{
    /// <summary>
    /// Generates text by substituting an object's values for placeholders.
    /// </summary>
    public sealed class MustacheGenerator
    {
        private readonly IMustacheGenerator _mustacheGenerator;
        private readonly List<EventHandler<KeyFoundEventArgs>> _foundHandlers;
        private readonly List<EventHandler<KeyNotFoundEventArgs>> _notFoundHandlers;
        private readonly List<EventHandler<ValueRequestEventArgs>> _valueRequestedHandlers;

        /// <summary>
        /// Initializes a new instance of a Generator.
        /// </summary>
        /// <param name="mustacheGenerator">The text generator to wrap.</param>
        internal MustacheGenerator(IMustacheGenerator mustacheGenerator)
        {
            _mustacheGenerator = mustacheGenerator;
            _foundHandlers = new List<EventHandler<KeyFoundEventArgs>>();
            _notFoundHandlers = new List<EventHandler<KeyNotFoundEventArgs>>();
            _valueRequestedHandlers = new List<EventHandler<ValueRequestEventArgs>>();
        }

        /// <summary>
        /// Occurs when a key/property is found.
        /// </summary>
        public event EventHandler<KeyFoundEventArgs> KeyFound
        {
            add { _foundHandlers.Add(value); }
            remove { _foundHandlers.Remove(value); }
        }

        /// <summary>
        /// Occurs when a key/property is not found in the object graph.
        /// </summary>
        public event EventHandler<KeyNotFoundEventArgs> KeyNotFound
        {
            add { _notFoundHandlers.Add(value); }
            remove { _notFoundHandlers.Remove(value); }
        }

        /// <summary>
        /// Occurs when a setter is encountered and requires a value to be provided.
        /// </summary>
        public event EventHandler<ValueRequestEventArgs> ValueRequested
        {
            add { _valueRequestedHandlers.Add(value); }
            remove { _valueRequestedHandlers.Remove(value); }
        }

        /// <summary>
        /// Occurs when a tag is replaced by its text.
        /// </summary>
        public event EventHandler<TagFormattedEventArgs> TagFormatted;

        /// <summary>
        /// Gets the text that is generated for the given object.
        /// </summary>
        /// <param name="source">The object to generate the text with.</param>
        /// <returns>The text generated for the given object.</returns>
        public string Render(object source)
        {
            return render(CultureInfo.CurrentCulture, source);
        }

        /// <summary>
        /// Gets the text that is generated for the given object.
        /// </summary>
        /// <param name="provider">The format provider to use.</param>
        /// <param name="source">The object to generate the text with.</param>
        /// <returns>The text generated for the given object.</returns>
        public string Render(IFormatProvider provider, object source)
        {
            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }
            return render(provider, source);
        }

        private string render(IFormatProvider provider, object source)
        {
            Scope keyScope = new Scope(source);
            Scope contextScope = new Scope(new Dictionary<string, object>());
            foreach (EventHandler<KeyFoundEventArgs> handler in _foundHandlers)
            {
                keyScope.KeyFound += handler;
                contextScope.KeyFound += handler;
            }
            foreach (EventHandler<KeyNotFoundEventArgs> handler in _notFoundHandlers)
            {
                keyScope.KeyNotFound += handler;
                contextScope.KeyNotFound += handler;
            }
            foreach (EventHandler<ValueRequestEventArgs> handler in _valueRequestedHandlers)
            {
                contextScope.ValueRequested += handler;
            }
            StringWriter writer = new StringWriter(provider);
            _mustacheGenerator.GetText(writer, keyScope, contextScope, postProcess);
            return writer.ToString();
        }

        private void postProcess(Substitution substitution)
        {
            if (TagFormatted == null)
            {
                return;
            }
            TagFormattedEventArgs args = new TagFormattedEventArgs(substitution.Key, substitution.Substitute, substitution.IsExtension);
            TagFormatted(this, args);
            substitution.Substitute = args.Substitute;
        }
    }
}
