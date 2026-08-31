using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.PlatformUI;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Asks for the connection string and the DbContext name when a template is first added, so the file the user
    ///     ends up with is ready to save rather than carrying a placeholder they have to find and edit.
    /// </summary>
    /// <remarks>
    ///     This is the piece the whole GUI exists for. A newcomer today gets a .tt containing
    ///     <c>Initial Catalog=**TODO**</c> and a wall of settings, and has to work out which line matters; the same
    ///     developer installing EF Core Power Tools gets asked and is done.
    ///
    ///     Nothing here reads the database. A brand-new template cannot be reverse engineered at all - efrpg rejects
    ///     the placeholder without connecting - so the connection string has to be collected before any schema call
    ///     can be worth making. The table picker comes after this, not instead of it.
    /// </remarks>
    public sealed class ConnectionDialog : DialogWindow
    {
        private readonly TextBox _connectionString;
        private readonly TextBox _dbContextName;
        private readonly TextBlock _validation;
        private readonly Button _ok;

        public string ConnectionString => _connectionString.Text.Trim();

        public string DbContextName => _dbContextName.Text.Trim();

        /// <summary>
        ///     True when the user pressed OK. On false the caller leaves the template exactly as the item template
        ///     produced it, placeholder and all, which is still a working starting point.
        /// </summary>
        public bool Confirmed { get; private set; }

        public ConnectionDialog(string suggestedConnectionString, string suggestedDbContextName)
        {
            Title                 = "EntityFramework Reverse POCO Generator";
            Width                 = 660;
            SizeToContent         = SizeToContent.Height;
            ResizeMode            = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            HasMaximizeButton     = false;
            HasMinimizeButton     = false;

            _connectionString = new TextBox { Text = suggestedConnectionString, FontFamily = new FontFamily("Consolas"), Padding = new Thickness(6, 4, 6, 4), TextWrapping = TextWrapping.Wrap };
            _dbContextName    = new TextBox { Text = suggestedDbContextName, Padding = new Thickness(6, 4, 6, 4) };
            _validation       = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 10), Foreground = Brushes.OrangeRed };
            _ok               = new Button { Content = "OK", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true };

            _connectionString.TextChanged += (s, e) => Validate();
            _ok.Click += (s, e) => { Confirmed = true; DialogResult = true; Close(); };

            Content = Build();
            Validate();
        }

        private UIElement Build()
        {
            var skip = new Button { Content = "_Skip", MinWidth = 90, Padding = new Thickness(10, 4, 10, 4), IsCancel = true };
            skip.Click += (s, e) => { Confirmed = false; DialogResult = false; Close(); };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(_ok);
            buttons.Children.Add(skip);

            var body = new StackPanel { Margin = new Thickness(16) };
            body.Children.Add(new TextBlock
            {
                Text = "Point the template at your database. You can change any of this later by editing the .tt file.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 14)
            });
            body.Children.Add(Label("Connection string"));
            body.Children.Add(_connectionString);
            body.Children.Add(new TextBlock
            {
                Text = "Stored in the .tt file, which is usually in source control. Prefer integrated security over a password.",
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.75,
                Margin = new Thickness(0, 4, 0, 12)
            });
            body.Children.Add(Label("DbContext name"));
            body.Children.Add(_dbContextName);
            body.Children.Add(new Border { Height = 14 });
            body.Children.Add(_validation);
            body.Children.Add(buttons);
            return body;
        }

        private static TextBlock Label(string text)
        {
            return new TextBlock { Text = text, FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 0, 0, 4) };
        }

        /// <summary>
        ///     The one thing worth blocking on: leaving the placeholder in place produces a template that cannot
        ///     generate anything, and the error arrives much later when the user saves the file.
        /// </summary>
        private void Validate()
        {
            var stillPlaceholder = _connectionString.Text.IndexOf(
                Efrpg.Gui.TemplateSettingWriter.Placeholder, StringComparison.Ordinal) >= 0;

            _validation.Text = stillPlaceholder
                ? "Replace " + Efrpg.Gui.TemplateSettingWriter.Placeholder + " with your database name, or press Skip to edit the .tt yourself."
                : string.Empty;

            _ok.IsEnabled = !stillPlaceholder && _connectionString.Text.Trim().Length > 0;
        }
    }
}
