using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     A .tt file seen as a list of settings assignments, each with the exact span of its value, so one value
    ///     can be rewritten without touching a single other byte.
    /// </summary>
    /// <remarks>
    ///     **Nothing here re-renders the file.** Every write is a substring replacement over the original text.
    ///     That is not an optimisation, it is the entire safety property: these templates are in source control,
    ///     heavily commented, and often edited by hand for years. Reconstructing one from a parse tree would lose
    ///     the comments, the alignment, the T4 markers and any customisation the parser did not model.
    ///
    ///     Statements are found with <see cref="StatementScanner"/>, the same lexer BuildTT uses to generate the
    ///     settings metadata, so a semicolon inside a string, a URL inside a trailing comment and a brace inside a
    ///     commented-out example all behave the same way in both. A multi-line assignment - a lambda, an object
    ///     initialiser - is recognised and recorded, but never rewritten.
    ///
    ///     Roslyn was the original plan and was rejected. It would have to be loaded in process by Visual Studio,
    ///     which brings its own, and this assembly cannot be exercised there from a test. The file is a flat list
    ///     of one-per-line assignments; the scanner already handles it and is already proven against Database.tt.
    /// </remarks>
    public sealed class TemplateSettingsDocument
    {
        /// <summary>
        ///     Matches the start of an assignment: optional comment marker, <c>Settings.</c>, a name, and the
        ///     equals sign. Anchored to the line start so a mention inside prose cannot match, and <c>=(?!=)</c>
        ///     so a comparison is not mistaken for an assignment.
        /// </summary>
        private static readonly Regex AssignmentStart =
            new Regex(@"^(?<indent>[ \t]*)(?<comment>//[ \t]*)?Settings\.(?<name>\w+)[ \t]*=(?!=)[ \t]*");

        private readonly string _text;

        private TemplateSettingsDocument(string text, IReadOnlyList<SettingAssignment> assignments,
            IReadOnlyList<string> filterLines)
        {
            _text       = text;
            Assignments = assignments;
            FilterLines = filterLines;
        }

        /// <summary>The template exactly as it stands, including every edit made so far.</summary>
        public string Text => _text;

        /// <summary>In the order they appear in the file.</summary>
        public IReadOnlyList<SettingAssignment> Assignments { get; }

        /// <summary>
        ///     The FilterSettings lines, exactly as written, for display only.
        /// </summary>
        /// <remarks>
        ///     Filtering is regexes and function calls rather than values, so there is nothing a form could safely
        ///     edit. Showing them still matters: filters decide which tables reach the generated code, so somebody
        ///     wondering why a table is missing needs to see them without leaving the dialog to read the file.
        /// </remarks>
        public IReadOnlyList<string> FilterLines { get; }

        public SettingAssignment Find(string name)
        {
            return Assignments.FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.Ordinal) && !a.IsCommentedOut)
                ?? Assignments.FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.Ordinal));
        }

        public static TemplateSettingsDocument Parse(string templateText)
        {
            if (templateText == null)
                throw new ArgumentNullException(nameof(templateText));

            return Build(templateText);
        }

        /// <summary>
        ///     Returns a new document with one value replaced. The original is untouched, so a dialog can build up
        ///     a set of edits and still show the user what the file looked like before.
        /// </summary>
        /// <remarks>
        ///     Refuses a multi-line assignment. The caller should not have offered to edit one, and silently
        ///     truncating a lambda to its first line is exactly the destruction this class exists to avoid.
        /// </remarks>
        public TemplateSettingsDocument WithValue(SettingAssignment assignment, string newValueText)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            if (newValueText == null)
                throw new ArgumentNullException(nameof(newValueText));

            if (assignment.SpansMultipleLines)
                throw new InvalidOperationException(
                    "Settings." + assignment.Name + " spans more than one line and cannot be rewritten from a form.");

            if (newValueText == assignment.ValueText)
                return this;

            var text = _text.Substring(0, assignment.ValueStart)
                       + newValueText
                       + _text.Substring(assignment.ValueStart + assignment.ValueLength);

            // Re-scanned rather than patched, because every span after this one has moved. Cheap next to being
            // wrong: these files are a few hundred lines and this happens once per edit.
            return Build(text);
        }

        private static TemplateSettingsDocument Build(string text)
        {
            return new TemplateSettingsDocument(text, Scan(text), FindFilterLines(text));
        }

        private static IReadOnlyList<string> FindFilterLines(string text)
        {
            return text.Split('\n')
                .Select(line => line.Trim())
                .Where(line => line.StartsWith("FilterSettings.", StringComparison.Ordinal))
                .ToList();
        }

        private static IReadOnlyList<SettingAssignment> Scan(string text)
        {
            var assignments = new List<SettingAssignment>();
            var lines       = SplitKeepingOffsets(text);
            var inCodeBlock = false;

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                if (!inCodeBlock)
                {
                    // <#@ is a directive, not the start of the code block.
                    inCodeBlock = line.Text.TrimStart().StartsWith("<#", StringComparison.Ordinal) &&
                                  !line.Text.TrimStart().StartsWith("<#@", StringComparison.Ordinal);
                    continue;
                }

                var match = AssignmentStart.Match(line.Text);
                if (match.Success)
                {
                    i = ReadAssignment(lines, i, match, assignments);
                    continue;
                }

                if (line.Text.IndexOf("#>", StringComparison.Ordinal) >= 0)
                    inCodeBlock = false;
            }

            return assignments;
        }

        /// <summary>
        ///     Reads one assignment from its equals sign to its terminating semicolon, however many lines that
        ///     takes, and returns the index of the last line it consumed.
        /// </summary>
        private static int ReadAssignment(IReadOnlyList<Line> lines, int start, Match match,
            List<SettingAssignment> assignments)
        {
            var first      = lines[start];
            var valueStart = first.Offset + match.Length;
            var scanner    = new StatementScanner();
            var value      = string.Empty;

            for (var i = start; i < lines.Count; i++)
            {
                // Only the first line is entered part way through, at the character after the equals sign.
                var fragment = i == start ? first.Text.Substring(match.Length) : lines[i].Text;

                scanner.Feed(fragment);

                if (scanner.Finished)
                {
                    value += fragment.Substring(0, scanner.TerminatorIndex);

                    assignments.Add(new SettingAssignment(
                        match.Groups["name"].Value,
                        value,
                        valueStart,
                        start + 1,
                        match.Groups["comment"].Success,
                        i > start));

                    return i;
                }

                value += fragment + lines[i].LineEnding;
            }

            // No semicolon anywhere below: the file is truncated or is not what it claims to be. Recording a span
            // that runs to the end of the file would let a later write destroy it, so it is skipped entirely.
            return lines.Count - 1;
        }

        private static IReadOnlyList<Line> SplitKeepingOffsets(string text)
        {
            var lines  = new List<Line>();
            var offset = 0;

            while (offset <= text.Length)
            {
                var newline = text.IndexOf('\n', offset);

                if (newline < 0)
                {
                    lines.Add(new Line(text.Substring(offset), offset, string.Empty));
                    break;
                }

                var end    = newline > offset && text[newline - 1] == '\r' ? newline - 1 : newline;
                var ending = text.Substring(end, newline - end + 1);

                lines.Add(new Line(text.Substring(offset, end - offset), offset, ending));
                offset = newline + 1;
            }

            return lines;
        }

        private struct Line
        {
            public Line(string text, int offset, string lineEnding)
            {
                Text       = text;
                Offset     = offset;
                LineEnding = lineEnding;
            }

            /// <summary>The line without its ending.</summary>
            public string Text { get; }

            /// <summary>Index into the whole template where this line starts.</summary>
            public int Offset { get; }

            /// <summary>"\r\n", "\n", or empty for the last line of a file with no trailing newline.</summary>
            public string LineEnding { get; }
        }
    }
}
