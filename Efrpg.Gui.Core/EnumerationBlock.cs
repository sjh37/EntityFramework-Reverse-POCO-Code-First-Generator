using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Appends entries to the <c>Settings.Enumerations = new List&lt;EnumerationSettings&gt; { ... };</c> block.
    ///     Append only: the entries already there, the comments and the shipped example are never read or moved,
    ///     which is what makes this safe on a block somebody wrote by hand.
    /// </summary>
    public static class EnumerationBlock
    {
        public const string SettingName = "Enumerations";

        private static readonly Regex Initialiser =
            new Regex(@"^\s*new\s+List\s*<\s*EnumerationSettings\s*>\s*(\(\s*\))?\s*\{", RegexOptions.Singleline);

        /// <summary>Why nothing can be appended to this template's block, or null when it can.</summary>
        public static string CannotAppendReason(TemplateSettingsDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var assignment = document.Find(SettingName);
            if (assignment == null)
                return "Settings." + SettingName + " is not in this template. Switch it on first.";

            if (assignment.IsCommentedOut)
                return "Settings." + SettingName + " is commented out. Switch it on first.";

            if (!Initialiser.IsMatch(assignment.ValueText))
                return "Settings." + SettingName + " is not a List<EnumerationSettings> initialiser, so entries cannot be appended. Edit it in the editor.";

            return null;
        }

        /// <summary>
        ///     Returns the document with the entry added as the last element of the initialiser, immediately
        ///     before the <c>}</c> that closes it on the statement's final line.
        /// </summary>
        public static TemplateSettingsDocument Append(TemplateSettingsDocument document, EnumerationEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            var reason = CannotAppendReason(document);
            if (reason != null)
                throw new InvalidOperationException(reason);

            if (!entry.IsValid)
                throw new InvalidOperationException(entry.Problem);

            var assignment = document.Find(SettingName);
            var statement  = document.StatementText(assignment).Replace("\r\n", "\n").Split('\n');
            var last       = statement[statement.Length - 1];
            var close      = last.LastIndexOf('}');

            if (close < 0 || statement.Length < 2)
                throw new InvalidOperationException("The Settings." + SettingName + " block does not end with a closing brace on its own line.");

            // Entries sit one level inside the braces, which is where the first existing line of the body is.
            var indent = assignment.Indent + "    ";

            var lines = entry.ToLines(indent).ToList();

            // Anything on the closing line before the brace (text after the last entry) is kept ahead of the new entry.
            var before = last.Substring(0, close);
            if (before.Trim().Length > 0)
                lines.Insert(0, before.TrimEnd());

            return document.WithLinesBeforeLine(assignment.EndLineNumber, lines, before.Trim().Length > 0 ? last.Substring(close) : null);
        }

        /// <summary>True when the block already names this table, by plain text rather than parsing; for a warning, not a refusal.</summary>
        public static bool MentionsTable(TemplateSettingsDocument document, string table)
        {
            if (document == null || string.IsNullOrWhiteSpace(table))
                return false;

            var assignment = document.Find(SettingName);
            if (assignment == null)
                return false;

            var statement = document.StatementText(assignment);
            var pattern   = "\"" + Regex.Escape(table.Trim()) + "\"";

            return Regex.IsMatch(statement, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Every table, in name order, so the dropdown reads like a list rather than a ranking. Views are left
        ///     out: an enum is read from a table. <see cref="LooksLikeEnumTable"/> is there for a caller that wants
        ///     to mark the likely ones.
        /// </summary>
        public static IReadOnlyList<DatabaseObject> Candidates(DatabaseSchema schema)
        {
            if (schema == null)
                return new DatabaseObject[0];

            return schema.Of(DatabaseObjectKind.Table)
                .OrderBy(t => t.FullName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public static bool LooksLikeEnumTable(DatabaseObject table)
        {
            return table != null &&
                   table.Columns.Any(c => c.IsIntegral) &&
                   table.Columns.Any(c => c.IsText) &&
                   table.Columns.Count <= 6;
        }

        /// <summary>
        ///     The entry a table most likely wants: its integral key as the value, its first text column as the
        ///     name, and the table's name in PascalCase as the enum's.
        /// </summary>
        public static EnumerationEntry Suggest(DatabaseObject table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            var value = table.Columns.FirstOrDefault(c => c.IsPrimaryKey && c.IsIntegral)
                        ?? table.Columns.FirstOrDefault(c => c.IsIntegral);
            var name  = table.Columns.FirstOrDefault(c => c.IsText && !c.IsPrimaryKey)
                        ?? table.Columns.FirstOrDefault(c => c.IsText);

            return new EnumerationEntry(PascalCase(table.Name), table.FullName,
                name == null ? string.Empty : name.Name,
                value == null ? string.Empty : value.Name,
                string.Empty);
        }

        /// <summary>order_status, ORDER_STATUS and OrderStatus all become OrderStatus; anything that is not a letter or digit is dropped.</summary>
        public static string PascalCase(string name)
        {
            var parts = Regex.Split(name ?? string.Empty, @"[^A-Za-z0-9]+").Where(p => p.Length > 0).ToList();

            if (parts.Count == 1 && parts[0].Any(char.IsLower))
                return char.ToUpperInvariant(parts[0][0]) + parts[0].Substring(1);

            var result = string.Concat(parts.Select(p => char.ToUpperInvariant(p[0]) + p.Substring(1).ToLowerInvariant()));
            return result.Length > 0 && char.IsDigit(result[0]) ? "_" + result : result;
        }
    }
}
