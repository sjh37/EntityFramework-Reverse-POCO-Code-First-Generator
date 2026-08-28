using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BuildTT.SettingsMetadata
{
    /// <summary>
    ///     Extracts the settings a source file assigns, together with the trailing // comment that documents each
    ///     one. Two shapes are understood: the <c>Settings.Foo = bar;</c> assignments in Database.tt, and the
    ///     <c>public static Type Foo = bar;</c> declarations in Settings.cs.
    /// </summary>
    /// <remarks>
    ///     This is where the GUI's tooltips come from. Database.tt already documents every setting inline, so
    ///     reading the comments back out is what stops a hand-maintained help file drifting from the template the
    ///     way the two copies of the language mappings drifted from each other.
    /// </remarks>
    public static class SettingsSourceParser
    {
        private static readonly Regex Assignment = new Regex(
            @"^\s*(?<commented>//\s*)?Settings\.(?<name>\w+)\s*=(?!=)",
            RegexOptions.Compiled);

        private static readonly Regex Declaration = new Regex(
            @"^\s*public\s+static\s+[^=]+?\s+(?<name>\w+)\s*=(?!=|>)",
            RegexOptions.Compiled);

        // A banner such as "// Filtering *****...". Five asterisks is well clear of anything in ordinary prose.
        private static readonly Regex SectionBanner = new Regex(
            @"^\s*//\s*(?<label>.*?)\s*\*{5,}\s*$",
            RegexOptions.Compiled);

        /// <summary>
        ///     Parses the <c>Settings.Foo = bar;</c> assignments in a .tt template.
        /// </summary>
        public static List<SettingSource> ParseTemplate(string text)
        {
            return Parse(text, Assignment);
        }

        /// <summary>
        ///     Parses the <c>public static Type Foo = bar;</c> declarations in Settings.cs.
        /// </summary>
        public static List<SettingSource> ParseDeclarations(string text)
        {
            return Parse(text, Declaration);
        }

        private static List<SettingSource> Parse(string text, Regex pattern)
        {
            var found   = new List<SettingSource>();
            var lines   = text.Replace("\r\n", "\n").Split('\n');
            var section = (string) null;

            for (var index = 0; index < lines.Length; index++)
            {
                var banner = SectionBanner.Match(lines[index]);
                if (banner.Success)
                {
                    var label = banner.Groups["label"].Value;
                    section = label.Length == 0 ? null : label;
                    continue;
                }

                var match = pattern.Match(lines[index]);
                if (!match.Success)
                    continue;

                var setting = new SettingSource
                {
                    Name         = match.Groups["name"].Value,
                    Section      = section,
                    Line         = index + 1,
                    CommentedOut = match.Groups["commented"].Success
                };

                index = ReadStatement(lines, index, match.Index + match.Length, setting);
                found.Add(setting);
            }

            return found;
        }

        /// <summary>
        ///     Consumes the statement starting after the "=" on <paramref name="firstLine" /> and returns the index of
        ///     the last line it occupied, so the caller resumes below it rather than inside a delegate body.
        /// </summary>
        private static int ReadStatement(string[] lines, int firstLine, int valueStart, SettingSource setting)
        {
            var scanner = new StatementScanner();
            var value   = lines[firstLine].Substring(valueStart);

            scanner.Feed(value);
            if (scanner.Finished)
            {
                setting.DefaultValue = value.Substring(0, scanner.TerminatorIndex).Trim();
                setting.Help         = HelpText(value, scanner.LineCommentIndex);
                return firstLine;
            }

            setting.MultiLine = true;

            for (var index = firstLine + 1; index < lines.Length; index++)
            {
                scanner.Feed(lines[index]);
                if (scanner.Finished)
                {
                    setting.Help = HelpText(lines[index], scanner.LineCommentIndex);
                    return index;
                }
            }

            return lines.Length - 1;
        }

        private static string HelpText(string line, int commentIndex)
        {
            if (commentIndex < 0)
                return null;

            var help = line.Substring(commentIndex + 2).Trim();
            return help.Length == 0 ? null : help;
        }
    }
}
