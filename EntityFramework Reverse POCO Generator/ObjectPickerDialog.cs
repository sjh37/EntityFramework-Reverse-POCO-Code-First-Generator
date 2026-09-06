using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     A checkbox tree of everything in the database - tables, views, stored procedures, functions - showing what
    ///     the template generates today and letting the user narrow it.
    /// </summary>
    /// <remarks>
    ///     This is the screen EF Core Power Tools wins evaluations with, and the one a newcomer expects to see. It
    ///     opens immediately and reads the database while open, so a slow server shows progress rather than a
    ///     frozen Visual Studio; closing the dialog cancels the read.
    ///
    ///     Nothing is bound. After every click the whole tree is refreshed from <see cref="ObjectSelection"/>, which
    ///     is the only place the rules live - a locked row, a category switching itself off, a count changing - so
    ///     what is on screen cannot disagree with what will be written. The tree structure is rebuilt only when the
    ///     search changes, so expansion and scroll position survive a click.
    ///
    ///     Objects a hand-written filter decides are shown disabled with the filter as their tooltip. Hiding them
    ///     would send somebody hunting for a table that is "missing" when the answer is on line 54 of their .tt.
    /// </remarks>
    public sealed class ObjectPickerDialog : DialogWindow
    {
        private static readonly DatabaseObjectKind[] KindOrder =
        {
            DatabaseObjectKind.Table,
            DatabaseObjectKind.View,
            DatabaseObjectKind.StoredProcedure,
            DatabaseObjectKind.TableValuedFunction,
            DatabaseObjectKind.ScalarValuedFunction
        };

        /// <summary>Above this many objects the schema groups start collapsed, so the first screen is a summary.</summary>
        private const int ExpandEverythingBelow = 300;

        private readonly TemplateFilterDocument _document;
        private readonly Func<CancellationToken, Task<SchemaReadResult>> _read;
        private readonly bool _isNewTemplate;

        private readonly TextBox _search;
        private readonly TreeView _tree;
        private readonly TextBlock _status;
        private readonly TextBlock _lockedNote;
        private readonly StackPanel _bulk;
        private readonly Button _confirm;
        private readonly Button _retry;

        private readonly CancellationTokenSource _closing = new CancellationTokenSource();
        private readonly Dictionary<DatabaseObject, CheckBox> _boxes = new Dictionary<DatabaseObject, CheckBox>();
        private readonly List<GroupNode> _groups = new List<GroupNode>();

        private JoinableTask _reading;
        private DatabaseSchema _schema;
        private ObjectSelection _selection;
        private Dictionary<DatabaseObject, ObjectChoice> _current = new Dictionary<DatabaseObject, ObjectChoice>();

        /// <summary>True when the user confirmed. The caller then writes <see cref="Text"/> to the .tt.</summary>
        public bool Confirmed { get; private set; }

        /// <summary>The template with the choice written into it, valid once <see cref="Confirmed"/> is true.</summary>
        public string Text { get; private set; }

        /// <param name="schema">
        ///     A schema already read - the Test button's result, say - or null to read one now through
        ///     <paramref name="read"/>.
        /// </param>
        public ObjectPickerDialog(string fileName, TemplateFilterDocument document, DatabaseSchema schema,
            Func<CancellationToken, Task<SchemaReadResult>> read, bool isNewTemplate)
        {
            _document      = document ?? throw new ArgumentNullException(nameof(document));
            _read          = read ?? throw new ArgumentNullException(nameof(read));
            _schema        = schema;
            _isNewTemplate = isNewTemplate;

            Title                 = "Choose what to generate - " + fileName;
            Width                 = 760;
            Height                = 660;
            MinWidth              = 520;
            MinHeight             = 420;
            ResizeMode            = ResizeMode.CanResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _search     = new TextBox { Padding = new Thickness(6, 4, 6, 4), IsEnabled = false };
            _tree       = new TreeView { BorderThickness = new Thickness(0), IsEnabled = false, Padding = new Thickness(4) };
            _status     = new TextBlock { TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
            _lockedNote = new TextBlock { TextWrapping = TextWrapping.Wrap, Opacity = 0.75, Visibility = Visibility.Collapsed, Margin = new Thickness(16, 6, 16, 0) };
            _bulk       = new StackPanel { Orientation = Orientation.Horizontal, Visibility = Visibility.Collapsed, Margin = new Thickness(0, 6, 0, 0) };
            _confirm    = new Button { Content = isNewTemplate ? "OK" : "_Save", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true, IsEnabled = false };
            _retry      = new Button { Content = "_Try again", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), Visibility = Visibility.Collapsed };

            _search.TextChanged += (s, e) => RebuildTree();
            _confirm.Click      += (s, e) => Commit();
            _retry.Click        += (s, e) => BeginRead();

            _bulk.Children.Add(LinkButton("Tick everything shown", () => SelectShown(true)));
            _bulk.Children.Add(new Border { Width = 16 });
            _bulk.Children.Add(LinkButton("Untick everything shown", () => SelectShown(false)));

            Content = Build();

            // Closing cancels the read, which kills the child process, and then waits for it so no continuation
            // is left running against a window that has gone. Joining through the JoinableTaskFactory is what
            // makes that safe rather than a deadlock.
            Closed += (s, e) => CancelRead();
            Loaded += (s, e) =>
            {
                if (_schema != null)
                    Populate(_schema);
                else
                    BeginRead();
            };
        }

        private UIElement Build()
        {
            var cancel = new Button
            {
                Content = _isNewTemplate ? "_Skip" : "_Cancel",
                MinWidth = 90,
                Padding = new Thickness(10, 4, 10, 4),
                IsCancel = true
            };
            cancel.Click += (s, e) => { Confirmed = false; DialogResult = false; Close(); };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(_retry);
            buttons.Children.Add(_confirm);
            buttons.Children.Add(cancel);

            var footer = new DockPanel { Margin = new Thickness(16, 10, 16, 14), LastChildFill = true };
            DockPanel.SetDock(buttons, Dock.Right);
            footer.Children.Add(buttons);
            footer.Children.Add(_status);

            var header = new StackPanel { Margin = new Thickness(16, 14, 16, 10) };
            header.Children.Add(new TextBlock
            {
                Text = _isNewTemplate
                    ? "Tick what you want generated. Everything is ticked to start with; you can change this later by right-clicking the .tt file."
                    : "Tick what you want generated. This shows what the template produces today.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 6)
            });
            header.Children.Add(new TextBlock
            {
                Text = "Ticking everything keeps the template generating whatever the database holds. Ticking only some writes the " +
                       "shorter list into the .tt as a filter you can read: the ticked names to include, or the unticked names to " +
                       "exclude, and a schema with nothing ticked becomes a schema filter. With an exclude list, anything added to " +
                       "the database later is generated; with an include list it stays out until you tick it here.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 0, 0, 10)
            });
            header.Children.Add(_search);
            header.Children.Add(_bulk);

            var treeBorder = new Border
            {
                Child = _tree,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(16, 0, 16, 0)
            };
            treeBorder.SetResourceReference(Border.BorderBrushProperty, EnvironmentColors.ComboBoxBorderBrushKey);

            var body = new Grid();
            body.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            body.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Grid.SetRow(treeBorder, 0);
            Grid.SetRow(_lockedNote, 1);
            body.Children.Add(treeBorder);
            body.Children.Add(_lockedNote);

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

        private void BeginRead()
        {
            _retry.Visibility = Visibility.Collapsed;
            ShowStatus("Reading the database. A large one can take a minute...", false);

            // VSSDK007 wants this awaited at the call site, which a Loaded handler cannot do. It is kept in
            // _reading and joined on close, after cancellation, so nothing outlives the window.
#pragma warning disable VSSDK007
            _reading = ThreadHelper.JoinableTaskFactory.RunAsync(ReadAsync);
#pragma warning restore VSSDK007

            _reading.Task.FileAndForget("efrpg/gui/objectpicker");
        }

        private void CancelRead()
        {
            _closing.Cancel();

            if (_reading != null)
                _reading.Join();
        }

        private async Task ReadAsync()
        {
            try
            {
                await TaskScheduler.Default;

                var result = await _read(_closing.Token);

                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(_closing.Token);

                if (result.Succeeded)
                    Populate(result.Schema);
                else
                    ShowProblem("Could not read the database. " + result.Error);
            }
            catch (OperationCanceledException)
            {
                // The dialog was closed while the read was in flight. There is nobody left to tell.
            }
            catch (Exception ex)
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                ShowProblem(ex.Message);
            }
        }

        private void Populate(DatabaseSchema schema)
        {
            _schema = schema;

            try
            {
                _selection = ObjectSelection.Create(schema, _document);
            }
            catch (InvalidOperationException ex)
            {
                ShowProblem(ex.Message);
                return;
            }

            _tree.IsEnabled   = true;
            _search.IsEnabled = true;
            _search.Focus();
            RebuildTree();
        }

        private void ShowProblem(string message)
        {
            ShowStatus(message, true);
            _retry.Visibility  = Visibility.Visible;
            _tree.IsEnabled    = false;
            _confirm.IsEnabled = false;
        }

        private void ShowStatus(string message, bool isProblem)
        {
            _status.Text       = message;
            _status.FontWeight = isProblem ? FontWeights.SemiBold : FontWeights.Normal;
            _status.SetResourceReference(TextBlock.ForegroundProperty, EnvironmentColors.DialogTextBrushKey);
        }

        /// <summary>
        ///     Builds the tree for the current search. Kinds with nothing in them are left out entirely; a schema
        ///     level is added only when a kind spans more than one schema, because "dbo" repeated on every row of
        ///     a single-schema database is noise.
        /// </summary>
        private void RebuildTree()
        {
            if (_selection == null)
                return;

            _tree.Items.Clear();
            _boxes.Clear();
            _groups.Clear();

            var query   = _search.Text.Trim();
            var choices = _selection.Choices
                .Where(c => query.Length == 0 || c.Object.FullName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            var expandSchemas = choices.Count <= ExpandEverythingBelow || query.Length > 0;

            foreach (var kind in KindOrder)
            {
                var ofKind = choices.Where(c => c.Object.Kind == kind).ToList();
                if (ofKind.Count == 0)
                    continue;

                var schemas = ofKind.Select(c => c.Object.Schema).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToList();
                var suffix  = schemas.Count == 1 && schemas[0].Length > 0 ? " in " + schemas[0] : string.Empty;
                var kindItem = GroupItem(Label(kind), suffix, ofKind.Select(c => c.Object).ToList(), true);

                if (schemas.Count > 1)
                {
                    foreach (var schema in schemas)
                    {
                        var inSchema   = ofKind.Where(c => string.Equals(c.Object.Schema, schema, StringComparison.OrdinalIgnoreCase)).ToList();
                        var schemaItem = GroupItem(schema, string.Empty, inSchema.Select(c => c.Object).ToList(), expandSchemas);

                        foreach (var choice in inSchema)
                            schemaItem.Items.Add(ObjectItem(choice));

                        kindItem.Items.Add(schemaItem);
                    }
                }
                else
                {
                    foreach (var choice in ofKind)
                        kindItem.Items.Add(ObjectItem(choice));
                }

                _tree.Items.Add(kindItem);
            }

            if (_tree.Items.Count == 0)
                _tree.Items.Add(new TreeViewItem
                {
                    Header = new TextBlock { Text = query.Length > 0 ? "Nothing matches \"" + query + "\"." : "The database has no tables, views, procedures or functions.", Opacity = 0.75 },
                    Focusable = false
                });

            _bulk.Visibility = query.Length > 0 && choices.Any(c => c.CanChange) ? Visibility.Visible : Visibility.Collapsed;

            Refresh();
        }

        private TreeViewItem GroupItem(string label, string suffix, List<DatabaseObject> objects, bool expanded)
        {
            var box   = new CheckBox { IsThreeState = true, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 6, 0) };
            var name  = new TextBlock { Text = label, FontWeight = FontWeights.SemiBold, VerticalAlignment = VerticalAlignment.Center };
            var count = new TextBlock { Opacity = 0.7, Margin = new Thickness(6, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };

            var header = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2) };
            header.Children.Add(box);
            header.Children.Add(name);
            header.Children.Add(count);

            var node = new GroupNode(box, count, objects, suffix);
            _groups.Add(node);

            // A three-state box cycles on its own when clicked. The model decides instead: fully ticked means
            // untick the lot, anything else means tick the lot. Refresh then puts the box where the model says.
            box.Click += (s, e) =>
            {
                var allTicked = node.Objects.All(o => _current[o].IsSelected == true || !_current[o].CanChange);
                _selection.SelectAll(node.Objects, !allTicked);
                Refresh();
            };

            return new TreeViewItem { Header = header, IsExpanded = expanded };
        }

        private TreeViewItem ObjectItem(ObjectChoice choice)
        {
            var box = new CheckBox
            {
                Content = new TextBlock { Text = choice.Object.Name },
                IsThreeState = !choice.CanChange,
                Margin = new Thickness(0, 1, 0, 1),
                VerticalContentAlignment = VerticalAlignment.Center
            };

            if (!choice.CanChange)
            {
                box.IsEnabled = false;
                box.Opacity   = 0.7;
                box.ToolTip   = new TextBlock { Text = choice.Reason, TextWrapping = TextWrapping.Wrap, MaxWidth = 480 };
                ToolTipService.SetShowOnDisabled(box, true);
            }

            box.Click += (s, e) =>
            {
                _selection.Select(choice.Object, box.IsChecked == true);
                Refresh();
            };

            _boxes[choice.Object] = box;

            return new TreeViewItem { Header = box };
        }

        /// <summary>
        ///     Puts every box, count and button where the model says. Cheap, and the only way what is on screen is
        ///     guaranteed to be what gets written.
        /// </summary>
        private void Refresh()
        {
            _current = _selection.Choices.ToDictionary(c => c.Object);

            foreach (var pair in _boxes)
                pair.Value.IsChecked = _current[pair.Key].IsSelected;

            foreach (var node in _groups)
            {
                var ticked = node.Objects.Count(o => _current[o].IsSelected == true);

                node.Box.IsChecked = ticked == node.Objects.Count ? true : ticked == 0 ? (bool?) false : null;
                node.Box.IsEnabled = node.Objects.Any(o => _current[o].CanChange);
                node.Count.Text    = "(" + ticked + " of " + node.Objects.Count + node.Suffix + ")";
            }

            var locked = _current.Values.Count(c => !c.CanChange);
            _lockedNote.Text = locked == 0
                ? string.Empty
                : locked + (locked == 1 ? " object is" : " objects are") + " decided by filters written in the .tt and cannot be changed here. Hover over one to see which filter.";
            _lockedNote.Visibility = locked == 0 ? Visibility.Collapsed : Visibility.Visible;

            ShowStatus(Summary(), false);

            _confirm.IsEnabled = _isNewTemplate || _selection.HasChanges;
        }

        private string Summary()
        {
            var parts = new List<string>();

            foreach (var kind in KindOrder)
            {
                var all = _current.Values.Where(c => c.Object.Kind == kind).ToList();
                if (all.Count == 0)
                    continue;

                parts.Add(all.Count(c => c.IsSelected == true) + " of " + all.Count + " " + Label(kind).ToLowerInvariant());
            }

            return parts.Count == 0 ? string.Empty : "Generating " + string.Join(", ", parts.ToArray()) + ".";
        }

        private void SelectShown(bool selected)
        {
            _selection.SelectAll(_boxes.Keys.ToList(), selected);
            Refresh();
        }

        private void Commit()
        {
            Text      = _selection.Apply();
            Confirmed = true;

            DialogResult = true;
            Close();
        }

        private static Button LinkButton(string text, Action action)
        {
            var button = new Button
            {
                Content = text,
                Padding = new Thickness(0),
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            button.SetResourceReference(ForegroundProperty, EnvironmentColors.ControlLinkTextBrushKey);
            button.Click += (s, e) => action();
            return button;
        }

        private static string Label(DatabaseObjectKind kind)
        {
            switch (kind)
            {
                case DatabaseObjectKind.Table:                return "Tables";
                case DatabaseObjectKind.View:                 return "Views";
                case DatabaseObjectKind.StoredProcedure:      return "Stored procedures";
                case DatabaseObjectKind.TableValuedFunction:  return "Table-valued functions";
                default:                                      return "Scalar functions";
            }
        }

        private sealed class GroupNode
        {
            public GroupNode(CheckBox box, TextBlock count, List<DatabaseObject> objects, string suffix)
            {
                Box     = box;
                Count   = count;
                Objects = objects;
                Suffix  = suffix;
            }

            public CheckBox Box { get; }
            public TextBlock Count { get; }
            public List<DatabaseObject> Objects { get; }
            public string Suffix { get; }
        }
    }
}
