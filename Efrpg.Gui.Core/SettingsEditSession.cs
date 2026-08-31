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

        private SettingsEditSession(TemplateSettingsDocument document, SettingsCatalogue catalogue,
            List<SettingEditorItem> items)
        {
            Document  = document;
            Catalogue = catalogue;
            _items    = items;
        }

        public TemplateSettingsDocument Document { get; }

        public SettingsCatalogue Catalogue { get; }

        /// <summary>In the order the generator declares them, which is the order Database.tt writes them.</summary>
        public IReadOnlyList<SettingEditorItem> Items => _items;

        public IReadOnlyList<SettingEditorItem> Changed => _items.Where(i => i.IsChanged).ToList();

        public bool HasChanges => _items.Any(i => i.IsChanged);

        /// <summary>The section headings, in declaration order, for the editor's navigation.</summary>
        public IReadOnlyList<string> Sections =>
            _items.Select(i => i.Section).Distinct(StringComparer.Ordinal).ToList();

        public static SettingsEditSession Load(string templateText, SettingsCatalogue catalogue)
        {
            if (catalogue == null)
                throw new ArgumentNullException(nameof(catalogue));

            var document = TemplateSettingsDocument.Parse(templateText);

            var items = catalogue.Settings
                .Select(definition => new SettingEditorItem(definition, document.Find(definition.Name)))
                .ToList();

            return new SettingsEditSession(document, catalogue, items);
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
                if (assignment == null)
                    continue;

                document = document.WithValue(assignment, item.PendingValueText);
            }

            return document.Text;
        }
    }
}
