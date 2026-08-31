using System;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Rewrites the value of a <c>Settings.Something = "..."</c> assignment in a .tt file, leaving every other
    ///     byte of the file alone.
    /// </summary>
    /// <remarks>
    ///     The .tt is authoritative and is usually in source control, frequently customised, and often the only place
    ///     a setting exists. So this replaces the string literal in place rather than regenerating anything: the
    ///     alignment, the trailing comment, the line endings and everything above and below are untouched.
    ///
    ///     Deliberately line-based and limited to single-line string settings, which is all the wizard needs to fill
    ///     in a new template. Phase 3's editor handles the general case with Roslyn; this is the same idea at the
    ///     size the wizard requires, not a competing implementation.
    /// </remarks>
    public sealed class TemplateSettingWriter
    {
        /// <summary>
        ///     What a freshly added template carries in place of a database name. The efrpg tool rejects a connection
        ///     string containing it without attempting to connect, so it doubles as the marker for "not configured".
        /// </summary>
        public const string Placeholder = "**TODO**";

        private string _text;

        public TemplateSettingWriter(string templateText)
        {
            if (templateText == null)
                throw new ArgumentNullException(nameof(templateText));

            _text = templateText;
        }

        public string Text => _text;

        /// <summary>
        ///     True when the template still carries the placeholder, so nothing can be reverse engineered from it yet.
        /// </summary>
        public bool IsUnconfigured => _text.IndexOf(Placeholder, StringComparison.Ordinal) >= 0;

        /// <summary>
        ///     Replaces the value of a single-line string setting. Returns false and changes nothing when the setting
        ///     is absent, commented out, or not a plain string literal - a user who has replaced it with an
        ///     expression meant that, and silently overwriting it would be the worst thing this class could do.
        /// </summary>
        public bool TrySetString(string settingName, string value)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException(nameof(settingName));

            // Anchored at the start of a line so a mention inside a comment or a longer identifier cannot match.
            // The literal is matched non-greedily with escapes honoured, so a value containing \" does not end it.
            var pattern = new Regex(
                @"(?<head>^[ \t]*Settings\." + Regex.Escape(settingName) + @"[ \t]*=[ \t]*"")(?<value>(?:[^""\\]|\\.)*)(?<tail>"";)",
                RegexOptions.Multiline);

            var match = pattern.Match(_text);
            if (!match.Success)
                return false;

            _text = _text.Substring(0, match.Groups["value"].Index)
                    + Escape(value)
                    + _text.Substring(match.Groups["value"].Index + match.Groups["value"].Length);

            return true;
        }

        /// <summary>
        ///     Escapes for a C# regular string literal. Connection strings routinely contain backslashes - a named
        ///     SQL Server instance is <c>Data Source=.\SQLEXPRESS</c> - and leaving those raw produces a .tt that
        ///     does not compile.
        /// </summary>
        private static string Escape(string value)
        {
            return (value ?? string.Empty).Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
