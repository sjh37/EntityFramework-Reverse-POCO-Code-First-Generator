using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     The <c>FilterSettings</c> block of a .tt as the object picker sees it: the five on/off flags, every live
    ///     regex filter line, and the lines the picker itself wrote last time.
    /// </summary>
    /// <remarks>
    ///     The picker persists a choice as ordinary generator code - <c>FilterSettings.TableFilters.Add(new
    ///     RegexIncludeFilter(...))</c> - rather than in a side-car file, so the .tt stays the one place everything
    ///     lives and a user who never opens the dialog again can still read and edit what it did. Its lines carry a
    ///     trailing marker comment, which is how they are found and replaced on the next run; every other filter
    ///     line belongs to the user and is read but never touched.
    ///
    ///     Edits are whole-line: a value replaced in place, a commented-out flag uncommented, marked lines removed
    ///     and reinserted. No other byte moves. Like <see cref="TemplateSettingsDocument"/>, every edit returns a
    ///     fresh parse rather than patching indexes.
    /// </remarks>
    public sealed class TemplateFilterDocument
    {
        /// <summary>
        ///     Starts the trailing comment on every line the picker writes. Stable for the life of the product: a
        ///     .tt written today must still be recognised by a picker years from now.
        /// </summary>
        public const string PickerMarker = "Reverse POCO object picker";

        private const string PickerComment = "// " + PickerMarker + ": right-click the .tt to change this";

        private static readonly Regex FlagLine = new Regex(
            @"^(?<indent>[ \t]*)(?<comment>//[ \t]*)?FilterSettings\.(?<name>IncludeViews|IncludeSynonyms|IncludeStoredProcedures|IncludeTableValuedFunctions|IncludeScalarValuedFunctions)(?<gap>[ \t]*=[ \t]*)(?<value>true|false)(?<tail>[ \t]*;.*)$");

        private static readonly Regex AddLine = new Regex(
            @"^(?<indent>[ \t]*)(?<comment>//[ \t]*)?FilterSettings\.(?<list>Schema|Table|Column|StoredProcedure)Filters\.Add\(");

        /// <summary>
        ///     The one shape that can be evaluated: a Regex(Include|Exclude)Filter built from a regular or verbatim
        ///     string literal, optionally followed by a line comment.
        /// </summary>
        private static readonly Regex RegexAddLine = new Regex(
            @"^[ \t]*FilterSettings\.(?<list>Schema|Table|Column|StoredProcedure)Filters\.Add\([ \t]*new[ \t]+(?<class>RegexIncludeFilter|RegexExcludeFilter)[ \t]*\([ \t]*(?:@""(?<verbatim>(?:[^""]|"""")*)""|""(?<regular>(?:[^""\\]|\\.)*)"")[ \t]*\)[ \t]*\)[ \t]*;[ \t]*(?://[ \t]*(?<trailing>.*?))?[ \t]*$");

        private static readonly Regex CallLine =
            new Regex(@"^[ \t]*FilterSettings\.(?<call>Reset|AddDefaults)\(\)[ \t]*;");

        private static readonly Regex SingleContextLine =
            new Regex(@"^[ \t]*Settings\.GenerateSingleDbContext[ \t]*=[ \t]*(?<value>true|false)[ \t]*;");

        private readonly List<Line> _lines;
        private readonly List<FlagAssignment> _flagLines = new List<FlagAssignment>();
        private readonly List<AddAnchor> _addLines = new List<AddAnchor>();
        private readonly List<FilterLine> _filters = new List<FilterLine>();
        private int _resetIndex = -1;
        private int _addDefaultsIndex = -1;

        private TemplateFilterDocument(string text)
        {
            Text   = text;
            _lines = Split(text);
            Scan();
        }

        public string Text { get; }

        public IReadOnlyList<FilterLine> Filters => _filters;

        /// <summary>
        ///     False when <c>Settings.GenerateSingleDbContext = false</c>: the multi-context filter ignores
        ///     FilterSettings entirely, so nothing the picker wrote would have any effect.
        /// </summary>
        public bool GeneratesSingleContext { get; private set; } = true;

        /// <summary>
        ///     Why the picker cannot work on this template, or null when it can.
        /// </summary>
        public string RefusalReason
        {
            get
            {
                if (!GeneratesSingleContext)
                    return "This template generates more than one DbContext (Settings.GenerateSingleDbContext = false). " +
                           "Those are filtered by the MultiContext tables in the database rather than by FilterSettings, " +
                           "so there is nothing here for the picker to write.";

                if (_resetIndex < 0 && _addDefaultsIndex < 0 && _flagLines.Count == 0 && _addLines.Count == 0)
                    return "This template has no FilterSettings block, so there is nowhere to write the choice.";

                return null;
            }
        }

        public static TemplateFilterDocument Parse(string templateText)
        {
            if (templateText == null)
                throw new ArgumentNullException(nameof(templateText));

            return new TemplateFilterDocument(templateText);
        }

        public IEnumerable<FilterLine> In(FilterList list)
        {
            return _filters.Where(f => f.List == list);
        }

        /// <summary>
        ///     The value the generator will see: the last live assignment, or the AddDefaults() value when there is
        ///     none.
        /// </summary>
        public bool Flag(FilterFlag flag)
        {
            var live = _flagLines.LastOrDefault(f => f.Flag == flag && !f.IsCommentedOut);

            return live != null ? live.Value : DefaultFor(flag);
        }

        /// <summary>What FilterSettings.AddDefaults() sets, which every shipped template calls first.</summary>
        public static bool DefaultFor(FilterFlag flag)
        {
            return flag == FilterFlag.IncludeViews || flag == FilterFlag.IncludeStoredProcedures;
        }

        /// <summary>
        ///     Sets a flag. Rewrites the last live assignment in place; failing that, uncomments the template's own
        ///     commented-out one; failing that, adds a line after the flag block or after AddDefaults().
        /// </summary>
        public TemplateFilterDocument WithFlag(FilterFlag flag, bool value)
        {
            var lines = new List<Line>(_lines);
            var word  = value ? "true" : "false";

            var live = _flagLines.LastOrDefault(f => f.Flag == flag && !f.IsCommentedOut);
            if (live != null)
                return Rebuild(Replace(lines, live.LineIndex, live.WithValue(word)));

            var commented = _flagLines.LastOrDefault(f => f.Flag == flag);
            if (commented != null)
                return Rebuild(Replace(lines, commented.LineIndex, commented.Uncommented(word)));

            var anchor = _flagLines.Count > 0 ? _flagLines.Max(f => f.LineIndex) : DefaultAnchor();
            if (anchor < 0)
                throw new InvalidOperationException(RefusalReason ?? "There is nowhere to write FilterSettings." + flag + ".");

            var indent = Indent(lines[anchor].Text);
            return Rebuild(InsertAfter(lines, anchor, new[] { indent + "FilterSettings." + flag + " = " + word + ";" }));
        }

        /// <summary>
        ///     Replaces every picker-owned line in one list with one include line per pattern, or removes them all
        ///     when there are no patterns. The user's own lines in the same list are left where they are.
        /// </summary>
        public TemplateFilterDocument WithPickerPatterns(FilterList list, IReadOnlyList<string> patterns)
        {
            return WithPickerPatterns(list, patterns, true);
        }

        /// <summary>
        ///     As above, writing include lines or exclude lines. Either way they carry the marker and are the
        ///     picker's to replace next time.
        /// </summary>
        public TemplateFilterDocument WithPickerPatterns(FilterList list, IReadOnlyList<string> patterns, bool include)
        {
            if (patterns == null)
                throw new ArgumentNullException(nameof(patterns));

            var lines = new List<Line>(_lines);
            var owned = _filters.Where(f => f.List == list && f.IsPickerOwned).Select(f => f.LineIndex).OrderBy(i => i).ToList();

            if (owned.Count == 0 && patterns.Count == 0)
                return this;

            // Where the new lines go: where the old ones were, else after the last line for this list (the
            // template's commented-out examples count, so the line lands where a human would have put it), else
            // after the last filter line of any list, else after the flags, else after AddDefaults().
            int anchor;
            if (owned.Count > 0)
            {
                anchor = owned[0] - 1;
                for (var i = owned.Count - 1; i >= 0; i--)
                    lines.RemoveAt(owned[i]);
            }
            else
            {
                var sameList = _addLines.Where(a => a.List == list).Select(a => a.LineIndex).DefaultIfEmpty(-1).Max();
                var anyList  = _addLines.Select(a => a.LineIndex).DefaultIfEmpty(-1).Max();
                var flags    = _flagLines.Select(f => f.LineIndex).DefaultIfEmpty(-1).Max();

                anchor = sameList >= 0 ? sameList : anyList >= 0 ? anyList : flags >= 0 ? flags : DefaultAnchor();
            }

            if (patterns.Count == 0)
                return Rebuild(lines);

            if (anchor < 0)
                throw new InvalidOperationException(RefusalReason ?? "There is nowhere to write the FilterSettings." + list + "Filters line.");

            var indent = Indent(lines[anchor].Text);
            var text   = patterns.Select(p =>
                indent + "FilterSettings." + list + "Filters.Add(new " + (include ? "RegexIncludeFilter" : "RegexExcludeFilter") +
                "(@\"" + p.Replace("\"", "\"\"") + "\")); " + PickerComment);

            return Rebuild(InsertAfter(lines, anchor, text));
        }

        private int DefaultAnchor()
        {
            return _addDefaultsIndex >= 0 ? _addDefaultsIndex : _resetIndex;
        }

        private void Scan()
        {
            for (var i = 0; i < _lines.Count; i++)
            {
                var text = _lines[i].Text;

                var flag = FlagLine.Match(text);
                if (flag.Success)
                {
                    _flagLines.Add(new FlagAssignment(i, flag));
                    continue;
                }

                var add = AddLine.Match(text);
                if (add.Success)
                {
                    var list = (FilterList) Enum.Parse(typeof(FilterList), add.Groups["list"].Value);
                    _addLines.Add(new AddAnchor(i, list));

                    if (!add.Groups["comment"].Success)
                        _filters.Add(ReadFilter(i, text, list));

                    continue;
                }

                var call = CallLine.Match(text);
                if (call.Success)
                {
                    if (call.Groups["call"].Value == "Reset")
                        _resetIndex = i;
                    else
                        _addDefaultsIndex = i;

                    continue;
                }

                var context = SingleContextLine.Match(text);
                if (context.Success)
                    GeneratesSingleContext = context.Groups["value"].Value == "true";
            }
        }

        private static FilterLine ReadFilter(int lineIndex, string text, FilterList list)
        {
            var match = RegexAddLine.Match(text);
            if (!match.Success)
                return new FilterLine(list, false, null, false, lineIndex, text);

            var pattern = match.Groups["verbatim"].Success
                ? match.Groups["verbatim"].Value.Replace("\"\"", "\"")
                : UnescapeRegular(match.Groups["regular"].Value);

            var trailing = match.Groups["trailing"].Success ? match.Groups["trailing"].Value : string.Empty;

            return new FilterLine(list,
                match.Groups["class"].Value == "RegexIncludeFilter",
                pattern,
                trailing.StartsWith(PickerMarker, StringComparison.Ordinal),
                lineIndex,
                text);
        }

        /// <summary>
        ///     Undoes C# regular-string escaping. Only the two escapes that change a regex's meaning are decoded:
        ///     everything else - <c>\t</c>, <c>\n</c>, <c>A</c> - means the same thing to the regex engine
        ///     whether decoded or not, so it is passed through.
        /// </summary>
        private static string UnescapeRegular(string value)
        {
            var result = new StringBuilder(value.Length);

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == '\\' && i + 1 < value.Length && (value[i + 1] == '\\' || value[i + 1] == '"'))
                {
                    result.Append(value[i + 1]);
                    i++;
                    continue;
                }

                result.Append(value[i]);
            }

            return result.ToString();
        }

        private static string Indent(string lineText)
        {
            var end = 0;
            while (end < lineText.Length && (lineText[end] == ' ' || lineText[end] == '\t'))
                end++;

            return lineText.Substring(0, end);
        }

        private static List<Line> Replace(List<Line> lines, int index, string newText)
        {
            lines[index] = new Line(newText, lines[index].Ending);
            return lines;
        }

        /// <summary>
        ///     New lines take the anchor's line ending. A file whose last line has no newline gets one there so the
        ///     inserted lines can follow, and the final inserted line inherits the missing ending instead.
        /// </summary>
        private List<Line> InsertAfter(List<Line> lines, int anchor, IEnumerable<string> texts)
        {
            var ending = lines[anchor].Ending;
            var last   = ending.Length == 0;

            if (last)
            {
                ending        = DominantEnding();
                lines[anchor] = new Line(lines[anchor].Text, ending);
            }

            var inserted = texts.Select(t => new Line(t, ending)).ToList();
            if (inserted.Count > 0 && last)
                inserted[inserted.Count - 1] = new Line(inserted[inserted.Count - 1].Text, string.Empty);

            lines.InsertRange(anchor + 1, inserted);
            return lines;
        }

        private string DominantEnding()
        {
            return _lines.Count(l => l.Ending == "\n") > _lines.Count(l => l.Ending == "\r\n") ? "\n" : "\r\n";
        }

        private static TemplateFilterDocument Rebuild(List<Line> lines)
        {
            var text = new StringBuilder();
            foreach (var line in lines)
                text.Append(line.Text).Append(line.Ending);

            return new TemplateFilterDocument(text.ToString());
        }

        private static List<Line> Split(string text)
        {
            var lines  = new List<Line>();
            var offset = 0;

            while (offset <= text.Length)
            {
                var newline = text.IndexOf('\n', offset);

                if (newline < 0)
                {
                    lines.Add(new Line(text.Substring(offset), string.Empty));
                    break;
                }

                var end = newline > offset && text[newline - 1] == '\r' ? newline - 1 : newline;
                lines.Add(new Line(text.Substring(offset, end - offset), text.Substring(end, newline - end + 1)));
                offset = newline + 1;
            }

            return lines;
        }

        private struct Line
        {
            public Line(string text, string ending)
            {
                Text   = text;
                Ending = ending;
            }

            public string Text { get; }
            public string Ending { get; }
        }

        private sealed class AddAnchor
        {
            public AddAnchor(int lineIndex, FilterList list)
            {
                LineIndex = lineIndex;
                List      = list;
            }

            public int LineIndex { get; }
            public FilterList List { get; }
        }

        private sealed class FlagAssignment
        {
            private readonly Match _match;

            public FlagAssignment(int lineIndex, Match match)
            {
                _match         = match;
                LineIndex      = lineIndex;
                Flag           = (FilterFlag) Enum.Parse(typeof(FilterFlag), match.Groups["name"].Value);
                Value          = match.Groups["value"].Value == "true";
                IsCommentedOut = match.Groups["comment"].Success;
            }

            public int LineIndex { get; }
            public FilterFlag Flag { get; }
            public bool Value { get; }
            public bool IsCommentedOut { get; }

            public string WithValue(string word)
            {
                return _match.Groups["indent"].Value + _match.Groups["comment"].Value + "FilterSettings." + Flag +
                       _match.Groups["gap"].Value + word + _match.Groups["tail"].Value;
            }

            /// <summary>Drops the comment marker and nothing else, so the line keeps the template's alignment.</summary>
            public string Uncommented(string word)
            {
                return _match.Groups["indent"].Value + "FilterSettings." + Flag +
                       _match.Groups["gap"].Value + word + _match.Groups["tail"].Value;
            }
        }
    }
}
