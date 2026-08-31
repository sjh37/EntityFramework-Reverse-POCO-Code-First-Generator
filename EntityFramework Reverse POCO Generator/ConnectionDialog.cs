using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Asks for the database type, the template flavour, the connection string and the DbContext name - when a
    ///     template is first added, and again afterwards from the .tt file's right-click menu.
    /// </summary>
    /// <remarks>
    ///     This is the piece the whole GUI exists for. A newcomer today gets a .tt containing
    ///     <c>Initial Catalog=**TODO**</c> and a wall of settings, and has to work out which line matters; the same
    ///     developer installing EF Core Power Tools gets asked and is done.
    ///
    ///     The database dropdown sits above the connection string because it decides what the connection string
    ///     looks like: Oracle, PostgreSQL, MySQL and SQL Server share no keywords, so an Oracle user handed a SQL
    ///     Server connection string is no better off than with the placeholder. Choosing the database first and
    ///     being given the right skeleton is the point.
    ///
    ///     Nothing here reads the database. A brand-new template cannot be reverse engineered at all - efrpg rejects
    ///     the placeholder without connecting - so the connection string has to be collected before any schema call
    ///     can be worth making. The table picker comes after this, not instead of it.
    /// </remarks>
    public sealed class ConnectionDialog : DialogWindow
    {
        private readonly ComboBox _database;
        private readonly ComboBox _template;
        private readonly TextBox _connectionString;
        private readonly TextBox _dbContextName;
        private readonly TextBlock _connectionHint;
        private readonly TextBlock _templateHint;
        private readonly TextBlock _validation;
        private readonly Button _ok;
        private readonly bool _isNewTemplate;

        /// <summary>The answers, valid once <see cref="Confirmed"/> is true.</summary>
        public TemplateConfiguration Result
        {
            get
            {
                return new TemplateConfiguration(SelectedDatabase, SelectedTemplate,
                    _connectionString.Text.Trim(), _dbContextName.Text.Trim());
            }
        }

        private DatabaseTarget SelectedDatabase => (DatabaseTarget) _database.SelectedItem;

        private TemplateTarget SelectedTemplate => (TemplateTarget) _template.SelectedItem;

        /// <summary>
        ///     True when the user pressed OK. On false the caller leaves the .tt exactly as it found it, which for a
        ///     new template means the placeholder is still there - a working starting point.
        /// </summary>
        public bool Confirmed { get; private set; }

        /// <summary>
        ///     Opens showing <paramref name="current"/>, which for an existing .tt is what that file already says.
        /// </summary>
        /// <remarks>
        ///     Starting from the file's own values is not a nicety. A user reopening this to change one field would
        ///     otherwise press OK and have their connection string replaced by the SQL Server default.
        /// </remarks>
        public ConnectionDialog(TemplateConfiguration current, bool isNewTemplate)
        {
            if (current == null)
                throw new ArgumentNullException(nameof(current));

            _isNewTemplate = isNewTemplate;

            Title                 = "EntityFramework Reverse POCO Generator";
            Width                 = 680;
            MinWidth              = 520;
            SizeToContent         = SizeToContent.Height;

            // Widening is worth allowing: connection strings are long, and the box wraps rather than scrolls.
            // Height stays tied to the content, so there is never dead space below the buttons.
            ResizeMode            = ResizeMode.CanResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            HasMaximizeButton     = false;
            HasMinimizeButton     = false;

            _database         = new ComboBox { ItemsSource = DatabaseTarget.All, SelectedItem = current.Database, Padding = new Thickness(6, 4, 6, 4) };
            _template         = new ComboBox { ItemsSource = TemplateTarget.All, SelectedItem = current.Template, Padding = new Thickness(6, 4, 6, 4) };
            _connectionString = new TextBox { Text = current.ConnectionString, FontFamily = new FontFamily("Consolas"), Padding = new Thickness(6, 4, 6, 4), TextWrapping = TextWrapping.Wrap };
            _dbContextName    = new TextBox { Text = current.DbContextName, Padding = new Thickness(6, 4, 6, 4) };
            _connectionHint   = Hint(current.Database.Hint);
            _templateHint     = Hint(string.Empty);
            _validation       = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 10) };
            _ok               = new Button { Content = "OK", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true };

            // A literal colour would be wrong in two of the three Visual Studio themes. The resource reference
            // re-resolves when the user switches theme with the dialog open, which FindResource would not.
            _validation.SetResourceReference(TextBlock.ForegroundProperty,
                EnvironmentColors.ToolWindowValidationErrorTextBrushKey);

            _database.SelectionChanged    += (s, e) => DatabaseChanged();
            _template.SelectionChanged    += (s, e) => TemplateChanged();
            _connectionString.TextChanged += (s, e) => Validate();
            _ok.Click                     += (s, e) => { Confirmed = true; DialogResult = true; Close(); };

            Content = Build();
            TemplateChanged();
            Validate();
        }

        /// <summary>
        ///     Swaps in the connection string for the newly chosen database, but only while the box still holds one
        ///     of the untouched defaults.
        /// </summary>
        /// <remarks>
        ///     Overwriting what someone has typed is the worse failure of the two, and picking the database is
        ///     almost always the first thing a non-SQL-Server user does - the box is still pristine at that point,
        ///     so the swap happens exactly when it is wanted. Someone who fills in a SQL Server connection string
        ///     and then switches to Oracle keeps their text and has to rewrite it, which at least loses no work.
        ///     Reopening the dialog on a configured template never swaps at all, for the same reason.
        /// </remarks>
        private void DatabaseChanged()
        {
            var target = SelectedDatabase;
            if (target == null)
                return;

            if (DatabaseTarget.IsUntouchedDefault(_connectionString.Text))
                _connectionString.Text = target.ConnectionString;

            _connectionHint.Text = target.Hint;
            Validate();
        }

        /// <summary>
        ///     The file based templates read mustache files from Settings.TemplateFolder, which this dialog does not
        ///     set, so say so here rather than letting the next save fail.
        /// </summary>
        private void TemplateChanged()
        {
            var target = SelectedTemplate;

            _templateHint.Text = target != null && target.RequiresTemplateFolder
                ? "File based templates read from Settings.TemplateFolder in the .tt. Point it at your mustache folder before saving."
                : string.Empty;

            _templateHint.Visibility = _templateHint.Text.Length == 0 ? Visibility.Collapsed : Visibility.Visible;
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
            buttons.Children.Add(_ok);
            buttons.Children.Add(cancel);

            var body = new StackPanel { Margin = new Thickness(16) };
            body.Children.Add(new TextBlock
            {
                Text = _isNewTemplate
                    ? "Point the template at your database. You can change any of this later by editing the .tt file, or by right-clicking it."
                    : "Changing any of these rewrites that one line of the .tt and regenerates the output. Everything else in the file is left alone.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 14)
            });
            body.Children.Add(TwoColumns(
                Label("Database"), _database,
                Label("Template"), _template));
            body.Children.Add(_templateHint);
            body.Children.Add(new Border { Height = 12 });
            body.Children.Add(Label("Connection string"));
            body.Children.Add(_connectionString);
            body.Children.Add(_connectionHint);
            body.Children.Add(new TextBlock
            {
                Text = "Stored in the .tt file, which is usually in source control. Prefer integrated security over a password.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 0, 0, 12)
            });
            body.Children.Add(Label("DbContext name"));
            body.Children.Add(_dbContextName);
            body.Children.Add(new Border { Height = 14 });
            body.Children.Add(_validation);
            body.Children.Add(buttons);
            return body;
        }

        /// <summary>Two labelled controls side by side, each taking half the width.</summary>
        private static UIElement TwoColumns(UIElement leftLabel, UIElement left, UIElement rightLabel, UIElement right)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(16) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Place(grid, leftLabel,  0, 0);
            Place(grid, rightLabel, 0, 2);
            Place(grid, left,       1, 0);
            Place(grid, right,      1, 2);
            return grid;
        }

        private static void Place(Grid grid, UIElement element, int row, int column)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            grid.Children.Add(element);
        }

        private static TextBlock Label(string text)
        {
            return new TextBlock { Text = text, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 0, 0, 4) };
        }

        private static TextBlock Hint(string text)
        {
            return new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, Opacity = 0.75, Margin = new Thickness(0, 4, 0, 4) };
        }

        /// <summary>
        ///     The one thing worth blocking on: leaving a placeholder in place produces a template that cannot
        ///     generate anything, and the error arrives much later when the user saves the file.
        /// </summary>
        private void Validate()
        {
            var stillPlaceholder = _connectionString.Text.IndexOf(
                TemplateSettingsFile.Placeholder, StringComparison.Ordinal) >= 0;

            _validation.Text = stillPlaceholder
                ? "Replace every " + TemplateSettingsFile.Placeholder + " above" +
                  (_isNewTemplate ? ", or press Skip to edit the .tt yourself." : ".")
                : string.Empty;

            _ok.IsEnabled = !stillPlaceholder && _connectionString.Text.Trim().Length > 0;
        }
    }
}
