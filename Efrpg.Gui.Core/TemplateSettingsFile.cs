using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Reads and rewrites single-line <c>Settings.Something = ...</c> assignments in a .tt file, leaving every
    ///     other byte of the file alone.
    /// </summary>
    /// <remarks>
    ///     The .tt is authoritative and is usually in source control, frequently customised, and often the only place
    ///     a setting exists. So this replaces the value in place rather than regenerating anything: the alignment,
    ///     the trailing comment, the line endings and everything above and below are untouched.
    ///
    ///     Reading matters as much as writing. The dialog is reachable again after the file exists, and it has to
    ///     open showing what the template already says rather than the defaults - otherwise pressing OK would
    ///     silently overwrite the user's own connection string.
    ///
    ///     Deliberately line-based and limited to single-line string and <c>Type.Member</c> enum settings, which is
    ///     all the dialog needs. Phase 3's editor handles the general case with Roslyn; this is the same idea at the
    ///     size the dialog requires, not a competing implementation.
    /// </remarks>
    public sealed class TemplateSettingsFile
    {
        /// <summary>
        ///     What a freshly added template carries in place of a database name. The efrpg tool rejects a connection
        ///     string containing it without attempting to connect, so it doubles as the marker for "not configured".
        /// </summary>
        public const string Placeholder = "**TODO**";

        private string _text;

        public TemplateSettingsFile(string templateText)
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
        ///     Returns the value of a single-line string setting, unescaped, or null when the setting is absent,
        ///     commented out or not a plain string literal.
        /// </summary>
        public string GetString(string settingName)
        {
            var match = StringPattern(settingName).Match(_text);

            return match.Success ? Unescape(match.Groups["value"].Value) : null;
        }

        /// <summary>
        ///     Returns the member of a single-line enum setting - <c>SqlServer</c> from
        ///     <c>Settings.DatabaseType = DatabaseType.SqlServer;</c> - or null when there is no such assignment.
        /// </summary>
        public string GetEnum(string settingName)
        {
            var match = EnumPattern(settingName).Match(_text);

            return match.Success ? match.Groups["value"].Value : null;
        }

        /// <summary>
        ///     Replaces the value of a single-line string setting. Returns false and changes nothing when the setting
        ///     is absent, commented out, or not a plain string literal - a user who has replaced it with an
        ///     expression meant that, and silently overwriting it would be the worst thing this class could do.
        /// </summary>
        public bool TrySetString(string settingName, string value)
        {
            var match = StringPattern(settingName).Match(_text);

            return match.Success && Replace(match.Groups["value"], Escape(value));
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
            // Anything but a plain identifier would be injected into the .tt as code. Nothing legitimate reaches
            // here with one, so refuse rather than escape - there is nothing sensible to escape it to.
            if (string.IsNullOrEmpty(memberName) || !Identifier.IsMatch(memberName))
                return false;

            var match = EnumPattern(settingName).Match(_text);

            return match.Success && Replace(match.Groups["value"], memberName);
        }

        /// <summary>
        ///     Anchored at the start of a line so a mention inside a comment or a longer identifier cannot match, and
        ///     so a commented-out setting never does. The literal honours escapes, so a value containing \" does not
        ///     end it early.
        /// </summary>
        private static Regex StringPattern(string settingName)
        {
            return new Regex(
                @"(?<head>^[ \t]*Settings\." + Name(settingName) + @"[ \t]*=[ \t]*"")(?<value>(?:[^""\\]|\\.)*)(?<tail>"";)",
                RegexOptions.Multiline);
        }

        /// <summary>
        ///     The tail requires the semicolon immediately after the member, which is what refuses "A.B | A.C;" and
        ///     "Path.Combine(...);" - both would otherwise look like an enum member followed by more text.
        /// </summary>
        private static Regex EnumPattern(string settingName)
        {
            return new Regex(
                @"(?<head>^[ \t]*Settings\." + Name(settingName) + @"[ \t]*=[ \t]*[A-Za-z_]\w*[ \t]*\.[ \t]*)(?<value>[A-Za-z_]\w*)(?<tail>[ \t]*;)",
                RegexOptions.Multiline);
        }

        private static string Name(string settingName)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException(nameof(settingName));

            return Regex.Escape(settingName);
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

        /// <summary>
        ///     The inverse of <see cref="Escape"/>. Scanned left to right rather than done as two independent
        ///     replacements, which would misread an escaped backslash sitting in front of a quote as an escaped
        ///     quote - a real case, since a connection string can end in a directory separator.
        /// </summary>
        private static string Unescape(string value)
        {
            var result = new StringBuilder(value.Length);

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == '\\' && i + 1 < value.Length)
                    i++;

                result.Append(value[i]);
            }

            return result.ToString();
        }
    }
}
