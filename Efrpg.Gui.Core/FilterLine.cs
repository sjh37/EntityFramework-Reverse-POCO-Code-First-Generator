using System;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One live <c>FilterSettings.XFilters.Add(...)</c> line in a .tt, and whether this assembly can tell what it
    ///     does.
    /// </summary>
    /// <remarks>
    ///     Only a <c>RegexIncludeFilter</c> or <c>RegexExcludeFilter</c> built from a string literal can be
    ///     evaluated here, because that is the whole of what the generator does with one: <c>Regex.IsMatch</c> on
    ///     the database name. Anything else - a <c>new Regex(...)</c> with options, a custom filter class - is
    ///     recorded as present but unevaluable, and the picker then refuses to guess about the objects it governs.
    /// </remarks>
    public sealed class FilterLine
    {
        private readonly Regex _regex;

        public FilterLine(FilterList list, bool isInclude, string pattern, bool isPickerOwned, int lineIndex, string text)
        {
            List          = list;
            IsInclude     = isInclude;
            Pattern       = pattern;
            IsPickerOwned = isPickerOwned;
            LineIndex     = lineIndex;
            Text          = (text ?? string.Empty).Trim();

            if (pattern == null)
                return;

            try
            {
                _regex = new Regex(pattern);
            }
            catch (ArgumentException)
            {
                // A pattern the .NET regex engine rejects would also fail at generation time. Treated as
                // unevaluable rather than as matching nothing, so the picker says so instead of guessing.
                _regex = null;
            }
        }

        public FilterList List { get; }

        /// <summary>True for RegexIncludeFilter, false for RegexExcludeFilter.</summary>
        public bool IsInclude { get; }

        /// <summary>The regex as the generator will see it, or null when the line could not be read as one.</summary>
        public string Pattern { get; }

        /// <summary>
        ///     True for the lines the object picker wrote and therefore owns. Everything else belongs to the user
        ///     and is never rewritten.
        /// </summary>
        public bool IsPickerOwned { get; }

        /// <summary>Zero-based line in the template, for messages.</summary>
        public int LineIndex { get; }

        /// <summary>The line as written, trimmed, for showing the user which filter is responsible.</summary>
        public string Text { get; }

        public bool CanEvaluate => _regex != null;

        /// <summary>
        ///     Exactly what the generator does with it: <c>IsMatch</c> against the raw database name, no anchoring
        ///     and no case folding added.
        /// </summary>
        public bool Matches(string name)
        {
            if (_regex == null)
                throw new InvalidOperationException("This filter cannot be evaluated: " + Text);

            return _regex.IsMatch(name ?? string.Empty);
        }
    }
}
