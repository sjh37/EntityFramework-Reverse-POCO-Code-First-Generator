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

        /// <summary>The <c>//</c> that comments a line out, sitting right after its indentation.</summary>
        private static readonly Regex CommentMarker = new Regex(@"^[ \t]*(?<marker>//)");

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
        ///     The value span covers the whole right-hand side however many lines it takes, so replacing it never
        ///     truncates a statement; whether a multi-line value is one a form may rewrite is the item's decision,
        ///     which it makes by reading it first.
        /// </remarks>
        public TemplateSettingsDocument WithValue(SettingAssignment assignment, string newValueText)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            if (newValueText == null)
                throw new ArgumentNullException(nameof(newValueText));

            if (newValueText == assignment.ValueText)
                return this;

            var text = _text.Substring(0, assignment.ValueStart)
                       + newValueText
                       + _text.Substring(assignment.ValueStart + assignment.ValueLength);

            // Re-scanned rather than patched, because every span after this one has moved. Cheap next to being
            // wrong: these files are a few hundred lines and this happens once per edit.
            return Build(text);
        }

        /// <summary>
        ///     Switches a commented-out assignment on with a new value: the value is replaced and the <c>//</c>
        ///     marker removed, and nothing else on the line moves, so it lines up with its neighbours as the
        ///     template's author aligned it.
        /// </summary>
        public TemplateSettingsDocument WithUncommentedValue(SettingAssignment assignment, string newValueText)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            if (!assignment.IsCommentedOut)
                return WithValue(assignment, newValueText);

            if (newValueText == null)
                throw new ArgumentNullException(nameof(newValueText));

            // Every line of a multi-line statement carries a marker, so those come off first and the value is
            // replaced on the re-scanned result.
            if (assignment.SpansMultipleLines)
            {
                var uncommented = WithUncommented(assignment);
                return uncommented.WithValue(uncommented.Find(assignment.Name), newValueText);
            }

            // The value first: it sits after the marker, so removing the marker afterwards does not move it.
            var text = _text.Substring(0, assignment.ValueStart)
                       + newValueText
                       + _text.Substring(assignment.ValueStart + assignment.ValueLength);

            text = text.Substring(0, assignment.CommentMarkerStart)
                   + text.Substring(assignment.CommentMarkerStart + assignment.CommentMarkerLength);

            return Build(text);
        }

        /// <summary>
        ///     Removes an assignment outright - every line from its first to the one holding its semicolon, line
        ///     endings included - so a multi-line delegate goes as cleanly as a one-liner and nothing around it moves.
        /// </summary>
        public TemplateSettingsDocument WithoutAssignment(SettingAssignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            var lines = SplitKeepingOffsets(_text);
            lines.RemoveRange(assignment.LineNumber - 1, assignment.EndLineNumber - assignment.LineNumber + 1);

            return Build(Join(lines));
        }

        /// <summary>The text of an assignment's lines, as they stand in the file, for showing what a removal takes out.</summary>
        public string StatementText(SettingAssignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            var lines = SplitKeepingOffsets(_text);

            return Join(lines.GetRange(assignment.LineNumber - 1, assignment.EndLineNumber - assignment.LineNumber + 1)).TrimEnd('\r', '\n');
        }

        /// <summary>
        ///     Switches an assignment off by putting <c>//</c> after the indentation of every line it spans, so the
        ///     generator uses its own default and the user's code stays in the file to read and to switch back on.
        /// </summary>
        public TemplateSettingsDocument WithCommentedOut(SettingAssignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            if (assignment.IsCommentedOut)
                return this;

            var lines = SplitKeepingOffsets(_text);
            for (var i = assignment.LineNumber - 1; i < assignment.EndLineNumber; i++)
            {
                var text   = lines[i].Text;
                var indent = text.Length - text.TrimStart(' ', '\t').Length;
                lines[i] = new Line(text.Substring(0, indent) + "//" + text.Substring(indent), lines[i].Offset, lines[i].LineEnding);
            }

            return Build(Join(lines));
        }

        /// <summary>
        ///     Switches a commented-out assignment back on by removing the <c>//</c> marker from every line it
        ///     spans, and nothing else, so what comes back is exactly what <see cref="WithCommentedOut"/> took away.
        /// </summary>
        public TemplateSettingsDocument WithUncommented(SettingAssignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            if (!assignment.IsCommentedOut)
                return this;

            var lines = SplitKeepingOffsets(_text);
            for (var i = assignment.LineNumber - 1; i < assignment.EndLineNumber; i++)
            {
                var match = CommentMarker.Match(lines[i].Text);
                if (!match.Success)
                    continue;

                lines[i] = new Line(lines[i].Text.Remove(match.Groups["marker"].Index, match.Groups["marker"].Length), lines[i].Offset, lines[i].LineEnding);
            }

            return Build(Join(lines));
        }

        /// <summary>
        ///     Adds a statement the template lacks, as many lines as its body takes, beside an existing assignment.
        ///     The first line takes the anchor's indentation and the body's continuation lines are re-indented to
        ///     match, so a block copied from Settings.cs or from the shipped template lands looking native.
        /// </summary>
        /// <param name="body">The right-hand side, without the trailing semicolon, lines joined with LF.</param>
        public TemplateSettingsDocument WithNewStatement(string name, string body, SettingAssignment anchor, bool insertAfter)
        {
            if (anchor == null)
                throw new ArgumentNullException(nameof(anchor));

            return InsertStatement(name, body, insertAfter ? anchor.EndLineNumber : anchor.LineNumber - 1, anchor.LineNumber);
        }

        /// <summary>Adds a statement directly after an arbitrary line, a section heading typically, taking its indentation.</summary>
        public TemplateSettingsDocument WithNewStatementAfterLine(string name, string body, int lineNumber)
        {
            return InsertStatement(name, body, lineNumber, lineNumber);
        }

        /// <summary>
        ///     Inserts lines immediately before a line, and optionally replaces that line's text - for adding an
        ///     entry to a collection initialiser ahead of the brace that closes it.
        /// </summary>
        /// <param name="lineNumber">One-based; the new lines go in front of it.</param>
        /// <param name="replaceLineWith">New text for the line itself, or null to leave it as it is.</param>
        public TemplateSettingsDocument WithLinesBeforeLine(int lineNumber, IReadOnlyList<string> newLines, string replaceLineWith)
        {
            if (newLines == null)
                throw new ArgumentNullException(nameof(newLines));

            var lines = SplitKeepingOffsets(_text);
            if (lineNumber < 1 || lineNumber > lines.Count)
                throw new ArgumentOutOfRangeException(nameof(lineNumber));

            if (replaceLineWith != null)
                lines[lineNumber - 1] = new Line(replaceLineWith, lines[lineNumber - 1].Offset, lines[lineNumber - 1].LineEnding);

            for (var i = 0; i < newLines.Count; i++)
                lines = InsertLine(lines, lineNumber - 1 + i, newLines[i]);

            return Build(Join(lines));
        }

        private TemplateSettingsDocument InsertStatement(string name, string body, int at, int formatLineNumber)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var lines  = SplitKeepingOffsets(_text);
            var format = lines[formatLineNumber - 1].Text;
            var indent = format.Substring(0, format.Length - format.TrimStart(' ', '\t').Length);

            var bodyLines = body.Replace("\r\n", "\n").Split('\n');
            var rest      = bodyLines.Skip(1).ToList();

            // The continuation lines keep their shape relative to each other; only the common margin changes.
            var margin = rest.Where(l => l.Trim().Length > 0)
                .Select(l => l.Length - l.TrimStart(' ', '\t').Length)
                .DefaultIfEmpty(0)
                .Min();

            var statement = new List<string> { indent + "Settings." + name + " = " + bodyLines[0].Trim() };
            for (var i = 0; i < rest.Count; i++)
            {
                var line = rest[i].Trim().Length == 0 ? string.Empty : indent + rest[i].Substring(margin);
                statement.Add(i == rest.Count - 1 ? line + ";" : line);
            }

            if (rest.Count == 0)
                statement[0] += ";";

            for (var i = 0; i < statement.Count; i++)
                lines = InsertLine(lines, at + i, statement[i]);

            return Build(Join(lines));
        }

        /// <summary>
        ///     Adds an assignment the template does not have, as one new line beside an existing one. The new
        ///     line copies the anchor's indentation, puts its equals sign in the same column when the name fits,
        ///     and carries the help text as a trailing comment - the shape every line in Database.tt has.
        /// </summary>
        /// <param name="anchor">An assignment already in the file, chosen by the caller for being a neighbour.</param>
        /// <param name="insertAfter">After the anchor's last line, or before its first.</param>
        public TemplateSettingsDocument WithNewAssignment(string name, string valueText, string help,
            SettingAssignment anchor, bool insertAfter)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (valueText == null)
                throw new ArgumentNullException(nameof(valueText));

            if (anchor == null)
                throw new ArgumentNullException(nameof(anchor));

            return Insert(name, valueText, help, insertAfter ? anchor.EndLineNumber : anchor.LineNumber - 1, anchor.LineNumber, true);
        }

        /// <summary>
        ///     Adds an assignment directly after an arbitrary line - a section heading, typically - taking only
        ///     the indentation from it. No column alignment, because a comment's equals sign means nothing.
        /// </summary>
        public TemplateSettingsDocument WithNewAssignmentAfterLine(string name, string valueText, string help, int lineNumber)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (valueText == null)
                throw new ArgumentNullException(nameof(valueText));

            return Insert(name, valueText, help, lineNumber, lineNumber, false);
        }

        /// <summary>
        ///     The one-based number of the heading <paramref name="text"/>, as in the
        ///     <c>// Generate files in sub-folders ****</c> banners Database.tt groups its settings under. Minus one
        ///     when there is none. Only a banner counts: a plain comment that happens to start with the same word,
        ///     such as <c>//    Schema = Settings.DefaultSchema,</c> inside a commented-out example, is not a place
        ///     to insert anything.
        /// </summary>
        public int FindCommentLine(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return -1;

            var lines = SplitKeepingOffsets(_text);

            for (var i = 0; i < lines.Count; i++)
            {
                var trimmed = lines[i].Text.TrimStart();
                if (!trimmed.StartsWith("//", StringComparison.Ordinal))
                    continue;

                var label = trimmed.Substring(2).Trim();
                var stars = label.Length - label.TrimEnd('*').Length;
                if (stars >= 5 && string.Equals(label.TrimEnd('*').TrimEnd(), text.Trim(), StringComparison.Ordinal))
                    return i + 1;
            }

            return -1;
        }

        private TemplateSettingsDocument Insert(string name, string valueText, string help, int at, int formatLineNumber, bool align)
        {
            var lines  = SplitKeepingOffsets(_text);
            var format = lines[formatLineNumber - 1].Text;

            var indent  = format.Substring(0, format.Length - format.TrimStart(' ', '\t').Length);
            var equals  = align ? format.IndexOf('=') : -1;
            var head    = indent + "Settings." + name;
            var padding = equals > head.Length ? new string(' ', equals - head.Length) : " ";
            var comment = string.IsNullOrWhiteSpace(help) ? string.Empty : " // " + help.Trim();
            var line    = head + padding + "= " + valueText.Trim() + ";" + comment;

            return Build(Join(InsertLine(lines, at, line)));
        }

        /// <summary>
        ///     Inserts a line at a position, taking the line ending of the line before it. A file whose last line
        ///     has no newline gets one there, and the new last line inherits the missing ending instead.
        /// </summary>
        private List<Line> InsertLine(List<Line> lines, int at, string text)
        {
            var ending = at > 0 ? lines[at - 1].LineEnding : at < lines.Count ? lines[at].LineEnding : DominantEnding();

            if (ending.Length == 0)
            {
                ending        = DominantEnding();
                lines[at - 1] = new Line(lines[at - 1].Text, lines[at - 1].Offset, ending);
                lines.Insert(at, new Line(text, -1, string.Empty));
                return lines;
            }

            lines.Insert(at, new Line(text, -1, ending));
            return lines;
        }

        private string DominantEnding()
        {
            var lf   = 0;
            var crlf = 0;

            foreach (var line in SplitKeepingOffsets(_text))
            {
                if (line.LineEnding == "\r\n")
                    crlf++;
                else if (line.LineEnding == "\n")
                    lf++;
            }

            return lf > crlf ? "\n" : "\r\n";
        }

        private static string Join(IEnumerable<Line> lines)
        {
            var text = new System.Text.StringBuilder();

            foreach (var line in lines)
                text.Append(line.Text).Append(line.LineEnding);

            return text.ToString();
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

            var commentedOut = match.Groups["comment"].Success;

            for (var i = start; i < lines.Count; i++)
            {
                // Only the first line is entered part way through, at the character after the equals sign.
                var fragment = i == start ? first.Text.Substring(match.Length) : lines[i].Text;

                // A statement commented out line by line carries the marker on every line; without stripping it
                // the scanner would read each continuation as a comment and never find the semicolon.
                if (commentedOut && i > start)
                {
                    var marker = CommentMarker.Match(fragment);
                    if (!marker.Success)
                        return lines.Count - 1;

                    fragment = fragment.Remove(marker.Groups["marker"].Index, marker.Groups["marker"].Length);
                }

                scanner.Feed(fragment);

                if (scanner.Finished)
                {
                    value += fragment.Substring(0, scanner.TerminatorIndex);

                    var comment = match.Groups["comment"];

                    assignments.Add(new SettingAssignment(
                        match.Groups["name"].Value,
                        value,
                        valueStart,
                        start + 1,
                        i + 1,
                        comment.Success,
                        comment.Success ? first.Offset + comment.Index : -1,
                        comment.Success ? comment.Length : 0,
                        i > start,
                        match.Groups["indent"].Value));

                    return i;
                }

                value += fragment + lines[i].LineEnding;
            }

            // No semicolon anywhere below: the file is truncated or is not what it claims to be. Recording a span
            // that runs to the end of the file would let a later write destroy it, so it is skipped entirely.
            return lines.Count - 1;
        }

        private static List<Line> SplitKeepingOffsets(string text)
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
