using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Every setting the generator has, in a form, with a search box.
    /// </summary>
    /// <remarks>
    ///     There are 118 settings and nobody remembers their names, so the two things that make this usable are the
    ///     search - which covers the help text, not just the name - and the section list down the side, which is
    ///     the same grouping Database.tt already uses. Landing on "Settings" rather than an alphabetical wall means
    ///     the first thing anyone sees is the handful that actually matter.
    ///
    ///     Settings that cannot be edited are shown anyway, with their value and the reason. Hiding a lambda would
    ///     leave someone hunting the wiki for a setting that is right there in their file; showing it read-only
    ///     answers the question and makes clear the editor will not touch it.
    ///
    ///     The panel is rebuilt whenever anything changes rather than being bound. With at most a few dozen rows on
    ///     screen that costs nothing, and it removes every way for what is displayed to drift from what will be
    ///     written - which, for a dialog that rewrites a file in source control, is worth more than elegance.
    /// </remarks>
    public sealed class SettingsEditorDialog : DialogWindow
    {
        private readonly SettingsEditSession _session;
        private readonly TextBox _search;
        private readonly ListBox _sections;
        private readonly StackPanel _rows;
        private ScrollViewer _scroller;
        private readonly TextBlock _summary;
        private readonly Button _save;
        private readonly Button _discard;

        private const string AllSections = "All settings";
        private const string EnumsPage = "Enums";
        private const string CallbacksSection = "Callbacks";
        private const string FilterSection = "Filtering (read-only)";

        /// <summary>
        ///     The template sections that make up the Enums page: the tables to read enums from and the callbacks
        ///     that shape them. Together on one page because enum generation is a headline feature, not a footnote
        ///     inside a long list of callbacks.
        /// </summary>
        private static readonly string[] EnumSections = { "Enums", "Enum callbacks" };

        private static bool IsEnumSetting(SettingEditorItem item)
        {
            return EnumSections.Contains(item.Section);
        }

        private bool _rebuilding;
        private readonly List<CodeView> _codeViews = new List<CodeView>();

        /// <summary>Reads the database for the Add enumeration form, or null when the connection string cannot be resolved.</summary>
        private readonly Func<CancellationToken, Task<SchemaReadResult>> _readSchema;
        private readonly CancellationTokenSource _closing = new CancellationTokenSource();
        private DatabaseSchema _schema;
        private JoinableTask _reading;

        /// <summary>True when the user pressed Save. The caller then writes <see cref="Text"/> to the .tt.</summary>
        public bool Confirmed { get; private set; }

        /// <summary>The template with every change applied, valid once <see cref="Confirmed"/> is true.</summary>
        public string Text { get; private set; }

        /// <summary>
        ///     The setting whose "Open in .tt" button closed the dialog, or null. Opening the file at its statement
        ///     is also a save: the line only means anything in the file as written, so the caller applies
        ///     <see cref="Text"/> first and then opens the document at <see cref="SettingsEditSession.LineNumberOf"/>.
        /// </summary>
        public string OpenSetting { get; private set; }

        public SettingsEditorDialog(string fileName, SettingsEditSession session)
            : this(fileName, session, null)
        {
        }

        public SettingsEditorDialog(string fileName, SettingsEditSession session, Func<CancellationToken, Task<SchemaReadResult>> readSchema)
        {
            _session    = session ?? throw new ArgumentNullException(nameof(session));
            _readSchema = readSchema;

            Title                 = "Reverse POCO settings - " + fileName;
            Width                 = 1000;
            Height                = 720;
            MinWidth              = 640;
            MinHeight             = 420;
            ResizeMode            = ResizeMode.CanResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _search   = new TextBox { Padding = new Thickness(6, 4, 6, 4) };
            _sections = new ListBox { BorderThickness = new Thickness(0, 0, 1, 0) };
            _rows     = new StackPanel { Margin = new Thickness(16, 4, 16, 16) };
            _summary  = new TextBlock { VerticalAlignment = VerticalAlignment.Center };
            _save     = new Button { Content = "_Save", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true };
            _discard  = new Button { Content = "_Discard changes", MinWidth = 120, Padding = new Thickness(10, 4, 10, 4) };

            // Code settings live on their own page, so a section that holds nothing else drops out of the list;
            // everything enum-related, values and code alike, lives on the Enums page instead.
            var valueSections = session.Sections
                .Where(s => !EnumSections.Contains(s) && session.Items.Any(i => i.Section == s && !i.IsCode))
                .ToList();

            _sections.ItemsSource   = new[] { AllSections }
                .Concat(valueSections)
                .Concat(session.Items.Any(IsEnumSetting) ? new[] { EnumsPage } : new string[0])
                .Concat(session.Items.Any(i => i.IsCode && !IsEnumSetting(i)) ? new[] { CallbacksSection } : new string[0])
                .Concat(session.Document.FilterLines.Count > 0 ? new[] { FilterSection } : new string[0])
                .ToList();
            _sections.SelectedIndex = 1;   // The "Settings" group: connection string, context name, the essentials.

            _search.TextChanged      += (s, e) => Rebuild();
            _sections.SelectionChanged += (s, e) => Rebuild();
            _save.Click              += (s, e) => Commit();
            _discard.Click           += (s, e) => DiscardChanges();

            Content = Build();
            Rebuild();

            Loaded += (s, e) => _search.Focus();
            Closed += (s, e) => { CancelRead(); DisposeCodeViews(); };
        }

        private void CancelRead()
        {
            _closing.Cancel();

            if (_reading != null)
                _reading.Join();
        }

        /// <summary>
        ///     Opens the Add enumeration form, reading the database first when it has not been read yet. A read
        ///     that fails still opens the form, with free-text boxes and the reason shown in the summary line.
        /// </summary>
        private void AddEnumeration(Button button)
        {
            if (_schema != null || _readSchema == null)
            {
                ShowAddEnumeration();
                return;
            }

            button.IsEnabled = false;
            button.Content   = "Reading the database...";

#pragma warning disable VSSDK007
            _reading = ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                string problem = null;
                try
                {
                    await TaskScheduler.Default;
                    var result = await _readSchema(_closing.Token);
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(_closing.Token);

                    if (result.Succeeded)
                        _schema = result.Schema;
                    else
                        problem = result.Error;
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    problem = ex.Message;
                }

                if (problem != null)
                    _summary.Text = "Could not read the database, so the table and columns must be typed. " + problem;

                ShowAddEnumeration();
            });
#pragma warning restore VSSDK007

            _reading.Task.FileAndForget("efrpg/gui/addenumeration");
        }

        private void ShowAddEnumeration()
        {
            var dialog = new AddEnumerationDialog(_schema, _session.Document);
            dialog.ShowModal();

            if (dialog.Confirmed)
                _session.AddEnumeration(dialog.Result);

            Rebuild();
        }

        private void DisposeCodeViews()
        {
            foreach (var view in _codeViews)
                view.Dispose();

            _codeViews.Clear();
        }

        private UIElement Build()
        {
            var close = new Button { Content = "_Close", MinWidth = 90, Padding = new Thickness(10, 4, 10, 4), IsCancel = true };
            close.Click += (s, e) => { Confirmed = false; DialogResult = false; Close(); };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(_save);
            buttons.Children.Add(close);

            // The summary takes whatever width is left and trims with an ellipsis, so a long list of changed
            // names can never push Discard over Save and Close; the buttons keep their own columns.
            _summary.TextTrimming = TextTrimming.CharacterEllipsis;
            _summary.Margin       = new Thickness(0, 0, 12, 0);

            var footer = new Grid { Margin = new Thickness(16, 10, 16, 14) };
            footer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            _discard.Margin = new Thickness(0, 0, 24, 0);
            Grid.SetColumn(_summary, 0);
            Grid.SetColumn(_discard, 1);
            Grid.SetColumn(buttons, 2);
            footer.Children.Add(_summary);
            footer.Children.Add(_discard);
            footer.Children.Add(buttons);

            var header = new StackPanel { Margin = new Thickness(16, 14, 16, 10) };
            header.Children.Add(new TextBlock
            {
                Text = "Search " + _session.Items.Count + " settings by name, section or description. " +
                       "Only the lines you change are rewritten, and a setting the file does not have is added beside its " +
                       "neighbours. Comments and formatting are left alone.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 0, 0, 8)
            });
            header.Children.Add(_search);

            var body = new Grid();
            body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(240) });
            body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            _scroller = new ScrollViewer
            {
                Content = _rows,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };

            Grid.SetColumn(_sections, 0);
            Grid.SetColumn(_scroller, 1);
            body.Children.Add(_sections);
            body.Children.Add(_scroller);

            var layout = new Grid();
            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(header, 0);
            Grid.SetRow(body, 1);
            Grid.SetRow(footer, 2);
            layout.Children.Add(header);
            layout.Children.Add(body);
            layout.Children.Add(footer);
            return layout;
        }

        /// <summary>
        ///     Redraws the visible rows. Called after every edit so what is on screen is always what would be
        ///     written, with no binding layer able to disagree.
        /// </summary>
        private void Rebuild()
        {
            if (_rebuilding)
                return;

            _rebuilding = true;
            try
            {
                var searching = !string.IsNullOrWhiteSpace(_search.Text);
                var section   = _sections.SelectedItem as string ?? AllSections;

                if (!searching && section == FilterSection)
                {
                    ShowFilters();
                    return;
                }

                // Searching looks everywhere. Filtering the results down to the selected section as well would
                // hide the match somebody just went looking for. Code settings show on the Callbacks page and
                // at the end of "All settings", under their own heading rather than scattered through the rest.
                var visible = _session.Search(_search.Text)
                    .Where(i => searching
                                || (section == EnumsPage && IsEnumSetting(i))
                                || (section == CallbacksSection && i.IsCode && !IsEnumSetting(i))
                                || (section == AllSections && !i.IsCode)
                                || (section != CallbacksSection && section != AllSections && section != EnumsPage && i.Section == section && !i.IsCode))
                    .ToList();

                if (!searching && section == AllSections)
                    visible.AddRange(_session.Items.Where(i => i.IsCode));

                DisposeCodeViews();
                _rows.Children.Clear();

                if (visible.Count == 0)
                {
                    _rows.Children.Add(new TextBlock
                    {
                        Text = "Nothing matches \"" + _search.Text.Trim() + "\".",
                        Opacity = 0.75,
                        Margin = new Thickness(0, 16, 0, 0)
                    });
                }

                // Code settings are grouped under the template's own banners - Enum data, Enum callbacks,
                // Call-backs, Table renaming - so a list of tables to read enums from does not look like a callback.
                var showHeadings = searching || section == AllSections || section == CallbacksSection || section == EnumsPage;
                string heading = null;

                if (section == EnumsPage && !searching)
                    _rows.Children.Add(new TextBlock
                    {
                        Text = "Enums generated from lookup tables. List the tables to read under Enumerations, or add one with the " +
                               "button; the callbacks below decide which tables become enums and how their members are named.",
                        TextWrapping = TextWrapping.Wrap,
                        Opacity = 0.8,
                        Margin = new Thickness(0, 8, 0, 14)
                    });

                if (section == CallbacksSection && !searching)
                    _rows.Children.Add(new TextBlock
                    {
                        Text = "Each of these is code in the .tt rather than a value. Ticked means the template sets it; " +
                               "unticking comments the statement out so the generator's built-in default runs and your code stays " +
                               "in the file. Ticking a setting the template lacks writes the default body, ready to edit.",
                        TextWrapping = TextWrapping.Wrap,
                        Opacity = 0.8,
                        Margin = new Thickness(0, 8, 0, 14)
                    });

                foreach (var item in visible)
                {
                    if (showHeadings && item.Section != heading)
                    {
                        heading = item.Section;
                        _rows.Children.Add(SectionHeading(heading));
                    }

                    _rows.Children.Add(item.IsCode ? CallbackRow(item) : Row(item));
                }

                UpdateSummary();
            }
            finally
            {
                _rebuilding = false;
            }
        }

        /// <summary>
        ///     The FilterSettings lines, verbatim. Nothing here is editable - they are regexes and calls, not
        ///     values - but they decide which tables reach the generated code, so somebody wondering why a table
        ///     is missing should not have to leave the dialog to find out.
        /// </summary>
        private void ShowFilters()
        {
            DisposeCodeViews();
            _rows.Children.Clear();

            var intro = new TextBlock
            {
                Text = "These decide which schemas, tables, columns and stored procedures are generated. " +
                       "They are code rather than values, so edit them in the .tt itself, or use \"Reverse POCO: Choose tables and procedures...\" " +
                       "to pick tables and procedures by ticking them.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 14, 0, 12)
            };
            _rows.Children.Add(intro);

            var code = CodeView.Create(string.Join(Environment.NewLine, _session.Document.FilterLines));
            _codeViews.Add(code);
            _rows.Children.Add(code.Element);

            // Nothing sits below this box, so it takes every pixel between the intro and the buttons and follows
            // the window when it is resized, rather than leaving the lower half of the page empty.
            SizeChangedEventHandler fill = (s, e) => FillRemainingHeight(code.Element, intro);
            _scroller.SizeChanged += fill;
            intro.SizeChanged     += fill;
            code.Element.Unloaded += (s, e) => { _scroller.SizeChanged -= fill; intro.SizeChanged -= fill; };
            FillRemainingHeight(code.Element, intro);

            UpdateSummary();
        }

        private void FillRemainingHeight(FrameworkElement element, FrameworkElement above)
        {
            var available = _scroller.ViewportHeight > 0 ? _scroller.ViewportHeight : _scroller.ActualHeight;
            if (available <= 0)
                return;

            var used = above.ActualHeight + above.Margin.Top + above.Margin.Bottom + _rows.Margin.Top + _rows.Margin.Bottom +
                       element.Margin.Top + element.Margin.Bottom;

            element.Height = Math.Max(120, available - used);
        }

        private void UpdateSummary()
        {
            var changes = _session.ChangeCount;
            var names   = _session.Changed.Select(c => c.Name)
                .Concat(_session.PendingEnumerations.Select(e => "enum " + e.Name))
                .ToList();

            _summary.Text = changes == 0
                ? "No changes"
                : changes + (changes == 1 ? " change: " : " changes: ") +
                  string.Join(", ", names.Take(4).ToArray()) +
                  (changes > 4 ? ", ..." : string.Empty);

            _summary.FontWeight  = changes == 0 ? FontWeights.Normal : FontWeights.SemiBold;
            _save.IsEnabled      = changes > 0;
            _discard.IsEnabled   = changes > 0;
        }

        private void DiscardChanges()
        {
            foreach (var item in _session.Changed.ToList())
                item.Revert();

            Rebuild();
        }

        private void Commit()
        {
            // Text boxes take their value on every keystroke, so nothing is pending here; taking focus is kept so
            // a box that lost focus rebuilds its row consistently whichever way Save was reached.
            _save.Focus();

            Text      = _session.Apply();
            Confirmed = true;

            DialogResult = true;
            Close();
        }

        private static UIElement SectionHeading(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 18, 0, 8),
                Opacity = 0.85
            };
        }

        /// <summary>
        ///     One setting: its name, an editor or its read-only value, and the generator's own help text beneath.
        /// </summary>
        private UIElement Row(SettingEditorItem item)
        {
            var name = new TextBlock
            {
                Text = item.Name,
                FontWeight = item.IsChanged ? FontWeights.Bold : FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center
            };

            var right = new StackPanel();
            right.Children.Add(item.IsEditable ? Editor(item) : ReadOnlyValue(item));

            if (item.Hint != null)
                right.Children.Add(new TextBlock
                {
                    Text = item.Hint,
                    TextWrapping = TextWrapping.Wrap,
                    FontStyle = FontStyles.Italic,
                    Opacity = 0.7,
                    Margin = new Thickness(0, 2, 0, 0)
                });

            if (item.Help.Length > 0)
                right.Children.Add(new TextBlock
                {
                    Text = item.Help,
                    TextWrapping = TextWrapping.Wrap,
                    Opacity = 0.7,
                    Margin = new Thickness(0, 4, 0, 0)
                });

            var links = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 0) };
            links.Children.Add(WikiLink(item));
            if (item.IsChanged)
                links.Children.Add(RevertLink(item));
            right.Children.Add(links);

            var grid = new Grid { Margin = new Thickness(0, 0, 0, 14) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(230) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid.SetColumn(name, 0);
            Grid.SetColumn(right, 1);
            grid.Children.Add(name);
            grid.Children.Add(right);
            return grid;
        }

        /// <summary>
        ///     A code setting: a tick for whether the template sets it, the statement itself in the editor's own
        ///     colouring, and the two ways out - the .tt at that line, and the wiki page that explains it.
        /// </summary>
        private UIElement CallbackRow(SettingEditorItem item)
        {
            var canWrite = item.Assignment != null || item.Definition.DefaultValue != null;

            var tick = new CheckBox
            {
                IsChecked = item.IsAssigned,
                IsEnabled = canWrite,
                Content = new TextBlock { Text = item.Name, FontWeight = item.IsChanged ? FontWeights.Bold : FontWeights.SemiBold, TextWrapping = TextWrapping.Wrap },
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 2, 0, 0)
            };
            tick.Checked   += (s, e) => { if (!_rebuilding) { item.SetAssigned(true);  Rebuild(); } };
            tick.Unchecked += (s, e) => { if (!_rebuilding) { item.SetAssigned(false); Rebuild(); } };

            var right = new StackPanel();

            if (item.Help.Length > 0)
                right.Children.Add(new TextBlock { Text = item.Help, TextWrapping = TextWrapping.Wrap, Opacity = 0.7 });

            right.Children.Add(new TextBlock
            {
                Text = State(item),
                TextWrapping = TextWrapping.Wrap,
                FontStyle = FontStyles.Italic,
                Opacity = 0.7,
                Margin = new Thickness(0, 2, 0, 0)
            });

            var code = CodeView.Create(item.Code ?? "// No default body is known for this setting.");
            _codeViews.Add(code);
            right.Children.Add(code.Element);

            var links = new StackPanel { Orientation = Orientation.Horizontal };

            var open = new Button
            {
                Content = "Open in .tt",
                IsEnabled = item.IsAssigned || item.Assignment != null,
                Padding = new Thickness(10, 3, 10, 3),
                Margin = new Thickness(0, 0, 12, 0),
                ToolTip = "Saves any changes, then opens the .tt at this statement."
            };
            open.Click += (s, e) => { OpenSetting = item.Name; Commit(); };
            links.Children.Add(open);

            links.Children.Add(WikiLink(item));

            if (item.IsChanged)
                links.Children.Add(RevertLink(item));

            right.Children.Add(links);

            if (item.Name == EnumerationBlock.SettingName)
                right.Children.Add(EnumerationAdder(item));

            var grid = new Grid { Margin = new Thickness(0, 0, 0, 18) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(230) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid.SetColumn(tick, 0);
            Grid.SetColumn(right, 1);
            grid.Children.Add(tick);
            grid.Children.Add(right);
            return grid;
        }

        /// <summary>
        ///     Under the Enumerations row: the enums queued to be appended on save, each with a way to drop it again,
        ///     and the button that adds another. Appending is the only edit offered - the block is never rewritten.
        /// </summary>
        private UIElement EnumerationAdder(SettingEditorItem item)
        {
            var panel = new StackPanel { Margin = new Thickness(0, 8, 0, 0) };

            foreach (var entry in _session.PendingEnumerations)
            {
                var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4) };
                row.Children.Add(new TextBlock { Text = "Will add " + entry, VerticalAlignment = VerticalAlignment.Center, FontWeight = FontWeights.SemiBold });

                var remove = new Button
                {
                    Content = "remove",
                    Padding = new Thickness(0),
                    Margin = new Thickness(10, 0, 0, 0),
                    Cursor = Cursors.Hand,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center
                };
                remove.SetResourceReference(ForegroundProperty, EnvironmentColors.ControlLinkTextBrushKey);
                var captured = entry;
                remove.Click += (s, e) => { _session.RemoveEnumeration(captured); Rebuild(); };
                row.Children.Add(remove);
                panel.Children.Add(row);
            }

            var reason = item.IsAssigned ? EnumerationBlock.CannotAppendReason(_session.Document) : null;
            if (reason != null && reason.Contains("Switch it on first"))
                reason = null;   // Adding switches the block on itself.

            var add = new Button
            {
                Content = "+ Add enumeration...",
                IsEnabled = reason == null,
                Padding = new Thickness(10, 3, 10, 3),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 4, 0, 0),
                ToolTip = reason ?? "Choose a lookup table and its name and value columns; the entry is appended to the block on save."
            };
            add.Click += (s, e) => AddEnumeration(add);
            panel.Children.Add(add);

            if (reason != null)
                panel.Children.Add(new TextBlock { Text = reason, TextWrapping = TextWrapping.Wrap, FontStyle = FontStyles.Italic, Opacity = 0.7, Margin = new Thickness(0, 4, 0, 0) });

            return panel;
        }

        private static string State(SettingEditorItem item)
        {
            if (item.IsAssignmentChanged)
                return item.IsAssigned
                    ? (item.Assignment == null ? "Will be added with the default body shown below." : "Will be switched back on.")
                    : "Will be commented out; the generator's built-in default applies.";

            if (item.Assignment == null)
                return item.Definition.DefaultValue == null
                    ? "Not in this template, and no default body is known to add."
                    : "Not in this template; the generator's built-in default applies. This is what ticking writes.";

            return item.Assignment.IsCommentedOut
                ? "Commented out in this template; the generator's built-in default applies."
                : "Set in this template, on line " + item.LineNumber + ".";
        }

        /// <summary>
        ///     The wiki page for a setting, opened in the browser. A setting documented alongside others lands on
        ///     its own heading; the label shows the page only.
        /// </summary>
        private static UIElement WikiLink(SettingEditorItem item)
        {
            var wiki = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 12, 0) };
            var link = new Hyperlink(new Run("Wiki: " + item.Definition.WikiPage)) { NavigateUri = new Uri(item.Definition.WikiUrl) };
            link.RequestNavigate += (s, e) => { Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true }); e.Handled = true; };
            wiki.Inlines.Add(link);
            return wiki;
        }

        private UIElement RevertLink(SettingEditorItem item)
        {
            var was = item.Assignment == null
                ? "not set"
                : item.Assignment.IsCommentedOut ? "commented out" : item.IsCode ? "set" : Shorten(item.Assignment.ValueText.Trim());

            var revert = new Button
            {
                Content = "Undo - was " + was,
                Padding = new Thickness(0),
                Margin = new Thickness(0, 4, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                Cursor = Cursors.Hand,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent
            };

            revert.SetResourceReference(ForegroundProperty, EnvironmentColors.ControlLinkTextBrushKey);
            revert.Click += (s, e) => { item.Revert(); Rebuild(); };
            return revert;
        }

        /// <summary>
        ///     A control that can hold the value without losing anything: a checkbox, a dropdown, a checklist for a
        ///     flags enum, or a text box.
        /// </summary>
        private UIElement Editor(SettingEditorItem item)
        {
            switch (item.Kind)
            {
                case SettingKind.Boolean:     return BooleanEditor(item);
                case SettingKind.Enumeration: return item.Definition.IsFlags ? FlagsEditor(item) : EnumEditor(item);
                case SettingKind.Number:      return NumberEditor(item);
                case SettingKind.Character:   return CharacterEditor(item);
                case SettingKind.StringList:  return StringListEditor(item);
                default:                      return TextEditor(item);
            }
        }

        private UIElement BooleanEditor(SettingEditorItem item)
        {
            var check = new CheckBox
            {
                IsChecked = item.BooleanValue,
                Content = item.BooleanValue ? "On" : "Off",
                VerticalAlignment = VerticalAlignment.Center
            };

            check.Click += (s, e) =>
            {
                item.SetBoolean(check.IsChecked == true);
                Rebuild();
            };

            return check;
        }

        private UIElement EnumEditor(SettingEditorItem item)
        {
            var combo = new ComboBox
            {
                ItemsSource = item.Definition.EnumMembers.Select(m => m.Name).ToList(),
                SelectedItem = item.SelectedMembers.FirstOrDefault(),
                Padding = new Thickness(6, 4, 6, 4),
                HorizontalAlignment = HorizontalAlignment.Left,
                MinWidth = 260
            };

            combo.SelectionChanged += (s, e) =>
            {
                var chosen = combo.SelectedItem as string;
                if (_rebuilding || chosen == null || item.SelectedMembers.FirstOrDefault() == chosen)
                    return;

                item.SetMembers(new[] { chosen });
                Rebuild();
            };

            return combo;
        }

        /// <summary>
        ///     A flags setting is a combination, so it gets a checkbox each rather than a dropdown that can only
        ///     say one thing.
        /// </summary>
        private UIElement FlagsEditor(SettingEditorItem item)
        {
            var panel    = new WrapPanel();
            var selected = new HashSet<string>(item.SelectedMembers, StringComparer.Ordinal);

            foreach (var member in item.Definition.EnumMembers)
            {
                // The zero member means "none of the others", so it is what an empty selection writes rather than
                // something to tick alongside them.
                if (member.Value == 0)
                    continue;

                var name  = member.Name;
                var check = new CheckBox
                {
                    Content = name,
                    IsChecked = selected.Contains(name),
                    Margin = new Thickness(0, 0, 16, 4)
                };

                check.Click += (s, e) =>
                {
                    var chosen = new HashSet<string>(item.SelectedMembers, StringComparer.Ordinal);

                    if (check.IsChecked == true)
                        chosen.Add(name);
                    else
                        chosen.Remove(name);

                    item.SetMembers(item.Definition.EnumMembers
                        .Where(m => chosen.Contains(m.Name))
                        .Select(m => m.Name)
                        .ToList());

                    Rebuild();
                };

                panel.Children.Add(check);
            }

            return panel;
        }

        private UIElement NumberEditor(SettingEditorItem item)
        {
            var box = new TextBox
            {
                Text = item.NumberValue.ToString(CultureInfo.InvariantCulture),
                Padding = new Thickness(6, 4, 6, 4),
                HorizontalAlignment = HorizontalAlignment.Left,
                MinWidth = 120
            };

            // The value is taken on every keystroke so Save lights up as soon as the number is changed; a half-typed
            // number that does not parse is simply not taken yet. The row itself is only rebuilt on losing focus,
            // because rebuilding mid-type would move the caret.
            var shown = box.Text;
            box.TextChanged += (s, e) =>
            {
                int value;
                if (int.TryParse(box.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value) && value != item.NumberValue)
                {
                    item.SetNumber(value);
                    UpdateSummary();
                }
            };
            box.LostFocus += (s, e) =>
            {
                if (box.Text != shown)
                    Rebuild();
            };

            return box;
        }

        private UIElement CharacterEditor(SettingEditorItem item)
        {
            var box = new TextBox
            {
                Text = item.CharacterValue,
                MaxLength = 1,
                Padding = new Thickness(6, 4, 6, 4),
                HorizontalAlignment = HorizontalAlignment.Left,
                MinWidth = 60
            };

            var shown = box.Text;
            box.TextChanged += (s, e) =>
            {
                if (box.Text.Length == 0 || box.Text == item.CharacterValue)
                    return;

                item.SetCharacter(box.Text);
                UpdateSummary();
            };
            box.LostFocus += (s, e) =>
            {
                if (box.Text != shown)
                    Rebuild();
            };

            return box;
        }

        /// <summary>One item per line. Blank lines are dropped, so the box can be cleared to empty the list.</summary>
        private UIElement StringListEditor(SettingEditorItem item)
        {
            var box = new TextBox
            {
                Text = string.Join(Environment.NewLine, item.StringListValue),
                Padding = new Thickness(6, 4, 6, 4),
                FontFamily = new FontFamily("Consolas"),
                AcceptsReturn = true,
                MinLines = 3,
                MaxLines = 12,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                TextWrapping = TextWrapping.NoWrap
            };

            var shown = box.Text;
            box.TextChanged += (s, e) =>
            {
                var items = box.Text.Replace("\r\n", "\n").Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0).ToList();
                if (!items.SequenceEqual(item.StringListValue))
                {
                    item.SetStringList(items);
                    UpdateSummary();
                }
            };
            box.LostFocus += (s, e) =>
            {
                if (box.Text != shown)
                    Rebuild();
            };

            return box;
        }

        private UIElement TextEditor(SettingEditorItem item)
        {
            var box = new TextBox
            {
                Text = item.TextValue,
                Padding = new Thickness(6, 4, 6, 4),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Consolas")
            };

            var shown = box.Text;
            box.TextChanged += (s, e) =>
            {
                if (box.Text == item.TextValue)
                    return;

                item.SetText(box.Text);
                UpdateSummary();
            };
            box.LostFocus += (s, e) =>
            {
                if (box.Text != shown)
                    Rebuild();
            };

            return box;
        }

        /// <summary>
        ///     What the file says, plus why the editor will not change it. Shown in code font because it is code.
        /// </summary>
        private static UIElement ReadOnlyValue(SettingEditorItem item)
        {
            var panel = new StackPanel();

            var value = new TextBlock
            {
                Text = Shorten(item.CurrentValueText ?? "(not set)"),
                FontFamily = new FontFamily("Consolas"),
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.9
            };

            var reason = new TextBlock
            {
                Text = item.ReadOnlyReason,
                TextWrapping = TextWrapping.Wrap,
                FontStyle = FontStyles.Italic,
                Opacity = 0.7,
                Margin = new Thickness(0, 2, 0, 0)
            };

            panel.Children.Add(value);
            panel.Children.Add(reason);
            return panel;
        }

        /// <summary>Keeps a lambda from turning one row into forty.</summary>
        private static string Shorten(string text)
        {
            var single = (text ?? string.Empty).Replace("\r\n", " ").Replace("\n", " ").Trim();

            return single.Length <= 160 ? single : single.Substring(0, 157) + "...";
        }
    }
}
