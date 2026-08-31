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

            return Replace(match.Groups["value"], Escape(value));
        }

        /// <summary>
        ///     Replaces the member of a single-line enum setting, as in <c>Settings.DatabaseType = DatabaseType.SqlServer;</c>.
        ///     The enum type name in the file is left exactly as written, so only the member after the dot changes.
        /// </summary>
        /// <remarks>
        ///     Returns false and changes nothing when the setting is absent, commented out, or anything other than a
        ///     bare <c>Type.Member</c> - a combination of flags, a call, a variable. Those are all deliberate on the
        ///     user's part and rewriting them as a single member would silently change what the template does.
        /// </remarks>
        public bool TrySetEnum(string settingName, string memberName)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException(nameof(settingName));

            // Anything but a plain identifier would be injected into the .tt as code. Nothing legitimate reaches
            // here with one, so refuse rather than escape - there is nothing sensible to escape it to.
            if (string.IsNullOrEmpty(memberName) || !Identifier.IsMatch(memberName))
                return false;

            // The tail requires the semicolon immediately after the member, which is what refuses "A.B | A.C;" and
            // "Path.Combine(...);" - both would otherwise look like an enum member followed by more text.
            var pattern = new Regex(
                @"(?<head>^[ \t]*Settings\." + Regex.Escape(settingName) + @"[ \t]*=[ \t]*[A-Za-z_]\w*[ \t]*\.[ \t]*)(?<value>[A-Za-z_]\w*)(?<tail>[ \t]*;)",
                RegexOptions.Multiline);

            var match = pattern.Match(_text);
            if (!match.Success)
                return false;

            return Replace(match.Groups["value"], memberName);
        }

        private static readonly Regex Identifier = new Regex(@"^[A-Za-z_]\w*$");

        /// <summary>Swaps one matched group for new text, leaving every other byte of the file alone.</summary>
        private bool Replace(Group value, string replacement)
        {
            _text = _text.Substring(0, value.Index) + replacement + _text.Substring(value.Index + value.Length);
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
