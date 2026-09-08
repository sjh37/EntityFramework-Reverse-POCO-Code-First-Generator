using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Gui
{
    /// <summary>
    ///     One pass through the settings editor: every setting the generator declares, paired with what this
    ///     template says, and the write-back that applies whatever the user changed.
    /// </summary>
    /// <remarks>
    ///     The dialog owns none of this. It shows rows and calls setters; the decisions about what is editable,
    ///     what a value means and what gets written are all here, where they can be tested against the real
    ///     Database.tt, the real Northwind.tt, and a v3 template out of git history.
    ///
    ///     Settings the template never mentions are still listed, showing the generator's own default and marked
    ///     as not set. Hiding them would leave a user hunting the wiki for a setting that simply is not in their
    ///     file; showing them is how you learn the setting exists.
    /// </remarks>
    public sealed class SettingsEditSession
    {
        private readonly List<SettingEditorItem> _items;
        private readonly List<EnumerationEntry> _enumerations = new List<EnumerationEntry>();

        private SettingsEditSession(TemplateSettingsDocument document, SettingsCatalogue catalogue,
            List<SettingEditorItem> items)
        {
            Document  = document;
            Catalogue = catalogue;
            _items    = items;
        }

        /// <summary>Entries queued for the end of the Settings.Enumerations block, written after every other edit.</summary>
        public IReadOnlyList<EnumerationEntry> PendingEnumerations => _enumerations;

        /// <summary>
        ///     Queues an enum to append. If the template has the block commented out or missing, it is switched on
        ///     first through the same edit the Callbacks page makes, so the append always has somewhere to land.
        /// </summary>
        public void AddEnumeration(EnumerationEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            if (!entry.IsValid)
                throw new InvalidOperationException(entry.Problem);

            var item = Find(EnumerationBlock.SettingName);
            if (item == null)
                throw new InvalidOperationException("This catalogue has no Settings." + EnumerationBlock.SettingName + ".");

            if (!item.IsAssigned)
                item.SetAssigned(true);

            _enumerations.Add(entry);
        }

        public void RemoveEnumeration(EnumerationEntry entry)
        {
            _enumerations.Remove(entry);
        }

        /// <summary>Every change the next <see cref="Apply"/> will write: edited settings plus queued enums.</summary>
        public int ChangeCount => Changed.Count + _enumerations.Count;

        public TemplateSettingsDocument Document { get; }

        public SettingsCatalogue Catalogue { get; }

        /// <summary>In the order the generator declares them, which is the order Database.tt writes them.</summary>
        public IReadOnlyList<SettingEditorItem> Items => _items;

        public IReadOnlyList<SettingEditorItem> Changed => _items.Where(i => i.IsChanged).ToList();

        public bool HasChanges => _items.Any(i => i.IsChanged) || _enumerations.Count > 0;

        /// <summary>The section headings, in declaration order, for the editor's navigation.</summary>
        public IReadOnlyList<string> Sections =>
            _items.Select(i => i.Section).Distinct(StringComparer.Ordinal).ToList();

        public static SettingsEditSession Load(string templateText, SettingsCatalogue catalogue)
        {
            if (catalogue == null)
                throw new ArgumentNullException(nameof(catalogue));

            var document = TemplateSettingsDocument.Parse(templateText);

            var items = catalogue.Settings
                .Select(definition =>
                {
                    var assignment = document.Find(definition.Name);
                    return new SettingEditorItem(definition, assignment, assignment == null ? null : document.StatementText(assignment));
                })
                .ToList();

            return new SettingsEditSession(document, catalogue, items);
        }

        /// <summary>
        ///     The line a setting's statement starts on in <paramref name="templateText"/>, or 0 when it has none.
        ///     For opening the .tt at a callback after the session's changes have been written: lines above it may
        ///     have moved, so the number is taken from the text that was saved rather than the one that was loaded.
        /// </summary>
        public static int LineNumberOf(string templateText, string settingName)
        {
            var assignment = TemplateSettingsDocument.Parse(templateText ?? string.Empty).Find(settingName);

            return assignment == null ? 0 : assignment.LineNumber;
        }

        public SettingEditorItem Find(string name)
        {
            return _items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.Ordinal));
        }

        /// <summary>
        ///     Rows matching a search, over the setting name, its section and its help text.
        /// </summary>
        /// <remarks>
        ///     Help text is searched because that is how somebody finds a setting whose name they do not know -
        ///     typing "pluralis" should reach it. There are 118 settings and nobody remembers the names.
        /// </remarks>
        public IReadOnlyList<SettingEditorItem> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _items;

            var terms = query.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return _items.Where(i => terms.All(t => Matches(i, t))).ToList();
        }

        private static bool Matches(SettingEditorItem item, string term)
        {
            return item.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                   || item.Section.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                   || item.Help.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        ///     Applies every change and returns the new template text. Nothing else in the file moves: one changed
        ///     setting means exactly one changed line.
        /// </summary>
        /// <remarks>
        ///     Each write re-scans, because replacing a value moves every span after it. Looking the assignment up
        ///     again by name on the freshly scanned document is what keeps a run of edits correct - patching the
        ///     offsets by hand would work right up until two settings on the same line, and then not.
        /// </remarks>
        public string Apply()
        {
            var document = Document;

            foreach (var item in _items.Where(i => i.IsChanged))
            {
                var assignment = document.Find(item.Name);

                if (item.PendingAssigned.HasValue)
                    document = Switch(document, item, assignment);
                else if (assignment == null)
                    document = Add(document, item);
                else if (assignment.IsCommentedOut)
                    document = document.WithUncommentedValue(assignment, item.PendingValueText);
                else
                    document = document.WithValue(assignment, item.PendingValueText);
            }

            // After the item edits, so a block switched on in this same session is there to append to.
            foreach (var entry in _enumerations)
                document = EnumerationBlock.Append(document, entry);

            return document.Text;
        }

        /// <summary>
        ///     Switches a code setting on or off: off comments its statement out, on uncomments one that is there
        ///     or writes the default body beside the setting's neighbours when the template has nothing to uncomment.
        /// </summary>
        private TemplateSettingsDocument Switch(TemplateSettingsDocument document, SettingEditorItem item, SettingAssignment assignment)
        {
            if (!item.PendingAssigned.Value)
                return assignment == null ? document : document.WithCommentedOut(assignment);

            if (assignment != null)
                return document.WithUncommented(assignment);

            return AddStatement(document, item);
        }

        /// <summary>
        ///     Adds a line for a setting the template lacks, beside the settings it belongs with.
        /// </summary>
        /// <remarks>
        ///     The catalogue is in Database.tt order, so the nearest setting of the same section that the file does
        ///     have - looking backwards first, then forwards - marks the spot the shipped template would have used.
        ///     A setting whose whole section is missing goes after whatever precedes it in the catalogue. Appending
        ///     to the end of the file is never an option: the settings block closes long before the end.
        /// </remarks>
        private TemplateSettingsDocument Add(TemplateSettingsDocument document, SettingEditorItem item)
        {
            var settings = Catalogue.Settings;
            var index    = IndexOf(settings, item.Name);

            // Assignments deeper than the block's usual indentation sit inside an if, as the sub-folder settings
            // do behind GenerateSeparateFiles. A neighbour there would put the new line under that condition.
            var baseIndent = document.Assignments.Count == 0 ? 0 : document.Assignments.Min(a => a.Indent.Length);

            var anchor = Nearest(document, settings, index, -1, item.Section, baseIndent)
                         ?? Nearest(document, settings, index, +1, item.Section, baseIndent);

            if (anchor != null)
                return document.WithNewAssignment(item.Name, item.PendingValueText, item.Help, anchor.Assignment, anchor.IsBefore);

            // No usable neighbour in the section: under its heading, which is where Database.tt would have it.
            var heading = document.FindCommentLine(item.Section);
            if (heading > 0)
                return document.WithNewAssignmentAfterLine(item.Name, item.PendingValueText, item.Help, heading);

            anchor = Nearest(document, settings, index, -1, null, baseIndent)
                     ?? Nearest(document, settings, index, +1, null, baseIndent);

            if (anchor == null)
                throw new InvalidOperationException("This template has no Settings block to add Settings." + item.Name + " to.");

            return document.WithNewAssignment(item.Name, item.PendingValueText, item.Help, anchor.Assignment, anchor.IsBefore);
        }

        /// <summary>The block form of <see cref="Add"/>, placed by the same rules, for a callback the template lacks.</summary>
        private TemplateSettingsDocument AddStatement(TemplateSettingsDocument document, SettingEditorItem item)
        {
            var settings   = Catalogue.Settings;
            var index      = IndexOf(settings, item.Name);
            var baseIndent = document.Assignments.Count == 0 ? 0 : document.Assignments.Min(a => a.Indent.Length);
            var body       = item.Definition.DefaultValue;

            var anchor = Nearest(document, settings, index, -1, item.Section, baseIndent)
                         ?? Nearest(document, settings, index, +1, item.Section, baseIndent);

            if (anchor != null)
                return document.WithNewStatement(item.Name, body, anchor.Assignment, anchor.IsBefore);

            var heading = document.FindCommentLine(item.Section);
            if (heading > 0)
                return document.WithNewStatementAfterLine(item.Name, body, heading);

            anchor = Nearest(document, settings, index, -1, null, baseIndent)
                     ?? Nearest(document, settings, index, +1, null, baseIndent);

            if (anchor == null)
                throw new InvalidOperationException("This template has no Settings block to add Settings." + item.Name + " to.");

            return document.WithNewStatement(item.Name, body, anchor.Assignment, anchor.IsBefore);
        }

        private static int IndexOf(IReadOnlyList<SettingDefinition> settings, string name)
        {
            for (var i = 0; i < settings.Count; i++)
                if (string.Equals(settings[i].Name, name, StringComparison.Ordinal))
                    return i;

            return -1;
        }

        /// <summary>
        ///     The nearest setting in one direction that the file actually has, optionally restricted to a section.
        /// </summary>
        private static Anchor Nearest(TemplateSettingsDocument document, IReadOnlyList<SettingDefinition> settings,
            int from, int step, string section, int baseIndent)
        {
            for (var i = from + step; i >= 0 && i < settings.Count; i += step)
            {
                if (section != null && !string.Equals(settings[i].Section, section, StringComparison.Ordinal))
                    continue;

                var assignment = document.Find(settings[i].Name);
                if (assignment != null && assignment.Indent.Length <= baseIndent)
                    return new Anchor(assignment, step < 0);
            }

            return null;
        }

        private sealed class Anchor
        {
            public Anchor(SettingAssignment assignment, bool isBefore)
            {
                Assignment = assignment;
                IsBefore   = isBefore;
            }

            public SettingAssignment Assignment { get; }

            /// <summary>True when the anchor precedes the new line, so the line goes after it.</summary>
            public bool IsBefore { get; }
        }
    }
}
