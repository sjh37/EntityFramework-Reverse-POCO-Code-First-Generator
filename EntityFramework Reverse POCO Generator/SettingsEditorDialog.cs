using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;

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
        private readonly TextBlock _summary;
        private readonly Button _save;
        private readonly Button _discard;

        private const string AllSections = "All settings";
        private const string FilterSection = "Filtering (read-only)";

        private bool _rebuilding;

        /// <summary>True when the user pressed Save. The caller then writes <see cref="Text"/> to the .tt.</summary>
        public bool Confirmed { get; private set; }

        /// <summary>The template with every change applied, valid once <see cref="Confirmed"/> is true.</summary>
        public string Text { get; private set; }

        public SettingsEditorDialog(string fileName, SettingsEditSession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));

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

            _sections.ItemsSource   = new[] { AllSections }
                .Concat(session.Sections)
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
        }

        private UIElement Build()
        {
            var close = new Button { Content = "_Close", MinWidth = 90, Padding = new Thickness(10, 4, 10, 4), IsCancel = true };
            close.Click += (s, e) => { Confirmed = false; DialogResult = false; Close(); };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(_save);
            buttons.Children.Add(close);

            var footer = new DockPanel { Margin = new Thickness(16, 10, 16, 14), LastChildFill = false };
            var footerLeft = new StackPanel { Orientation = Orientation.Horizontal };
            footerLeft.Children.Add(_summary);
            footerLeft.Children.Add(new Border { Width = 12 });
            footerLeft.Children.Add(_discard);
            DockPanel.SetDock(footerLeft, Dock.Left);
            DockPanel.SetDock(buttons, Dock.Right);
            footer.Children.Add(footerLeft);
            footer.Children.Add(buttons);

            var header = new StackPanel { Margin = new Thickness(16, 14, 16, 10) };
            header.Children.Add(new TextBlock
            {
                Text = "Search " + _session.Items.Count + " settings by name, section or description. " +
                       "Only the line you change is rewritten - comments and formatting are left alone.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 0, 0, 8)
            });
            header.Children.Add(_search);

            var body = new Grid();
            body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(240) });
            body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var scroller = new ScrollViewer
            {
                Content = _rows,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };

            Grid.SetColumn(_sections, 0);
            Grid.SetColumn(scroller, 1);
            body.Children.Add(_sections);
            body.Children.Add(scroller);

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
                // hide the match somebody just went looking for.
                var visible = _session.Search(_search.Text)
                    .Where(i => searching || section == AllSections || i.Section == section)
                    .ToList();

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

                var showHeadings = searching || section == AllSections;
                string heading = null;

                foreach (var item in visible)
                {
                    if (showHeadings && item.Section != heading)
                    {
                        heading = item.Section;
                        _rows.Children.Add(SectionHeading(heading));
                    }

                    _rows.Children.Add(Row(item));
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
            _rows.Children.Clear();
            _rows.Children.Add(new TextBlock
            {
                Text = "These decide which schemas, tables, columns and stored procedures are generated. " +
                       "They are code rather than values, so edit them in the .tt itself.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 14, 0, 12)
            });

            foreach (var filter in _session.Document.FilterLines)
                _rows.Children.Add(new TextBlock
                {
                    Text = filter,
                    FontFamily = new FontFamily("Consolas"),
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 6)
                });

            UpdateSummary();
        }

        private void UpdateSummary()
        {
            var changes = _session.Changed.Count;

            _summary.Text = changes == 0
                ? "No changes"
                : changes + (changes == 1 ? " change: " : " changes: ") +
                  string.Join(", ", _session.Changed.Select(c => c.Name).Take(4).ToArray()) +
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

            if (item.Help.Length > 0)
                right.Children.Add(new TextBlock
                {
                    Text = item.Help,
                    TextWrapping = TextWrapping.Wrap,
                    Opacity = 0.7,
                    Margin = new Thickness(0, 4, 0, 0)
                });

            if (item.IsChanged)
                right.Children.Add(RevertLink(item));

            var grid = new Grid { Margin = new Thickness(0, 0, 0, 14) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(230) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid.SetColumn(name, 0);
            Grid.SetColumn(right, 1);
            grid.Children.Add(name);
            grid.Children.Add(right);
            return grid;
        }

        private UIElement RevertLink(SettingEditorItem item)
        {
            var was = item.Assignment == null ? string.Empty : item.Assignment.ValueText.Trim();

            var revert = new Button
            {
                Content = "Undo - was " + Shorten(was),
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

            // Committed on losing focus rather than on every keystroke: rebuilding mid-type would move the caret,
            // and a half-typed number is not a value anybody meant.
            box.LostFocus += (s, e) =>
            {
                int value;
                if (!int.TryParse(box.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                {
                    Rebuild();
                    return;
                }

                if (value == item.NumberValue)
                    return;

                item.SetNumber(value);
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

            box.LostFocus += (s, e) =>
            {
                if (box.Text.Length == 0 || box.Text == item.CharacterValue)
                    return;

                item.SetCharacter(box.Text);
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

            box.LostFocus += (s, e) =>
            {
                if (box.Text == item.TextValue)
                    return;

                item.SetText(box.Text);
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
