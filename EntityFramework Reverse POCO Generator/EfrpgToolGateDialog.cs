using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Tells the user the efrpg tool is missing or too old, and offers the three ways out: install it, copy the
    ///     command to run elsewhere, or carry on regardless.
    /// </summary>
    /// <remarks>
    ///     "Copy command" is not a nicety. It is the escape hatch for a developer behind a proxy, on an internal NuGet
    ///     feed, or without permission to install, and for them it is the only button that works. So the exact command
    ///     is always on screen next to the button, selectable, whether or not Install is going to succeed.
    ///
    ///     Built in code rather than XAML on purpose. This is an old-style net48 project and adding XAML compilation
    ///     to it is a build-configuration exercise; the dialog is a message, a command and three buttons, which does
    ///     not justify that yet. Deriving from DialogWindow still gets WPF rendering and Visual Studio's theming.
    /// </remarks>
    public sealed class EfrpgToolGateDialog : DialogWindow
    {
        private readonly EfrpgToolGate _gate;
        private readonly TextBlock _message;
        private readonly Button _install;
        private readonly Button _copy;
        private readonly Button _continue;

        private EfrpgToolStatus _status;

        /// <summary>
        ///     True when the caller should go ahead and add the template: either the tool was fixed here, or the user
        ///     chose to continue without it. False means back out.
        /// </summary>
        public bool Proceed { get; private set; }

        public EfrpgToolGateDialog(EfrpgToolGate gate, EfrpgToolStatus status)
        {
            _gate   = gate;
            _status = status;

            Title                 = "EntityFramework Reverse POCO Generator";
            Width                 = 620;
            SizeToContent         = SizeToContent.Height;
            ResizeMode            = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            HasMaximizeButton     = false;
            HasMinimizeButton     = false;

            _message  = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 12) };
            _install  = new Button { MinWidth = 120, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true };
            _copy     = new Button { MinWidth = 120, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), Content = "_Copy command" };
            _continue = new Button { MinWidth = 120, Padding = new Thickness(10, 4, 10, 4), Content = "C_ontinue anyway", IsCancel = true };

            _install.Click  += OnInstall;
            _copy.Click     += OnCopy;
            _continue.Click += (s, e) => Close(true);

            Content = Build();
            Render();
        }

        private UIElement Build()
        {
            var command = new TextBox
            {
                IsReadOnly          = true,
                FontFamily          = new FontFamily("Consolas"),
                Padding             = new Thickness(6, 4, 6, 4),
                Margin              = new Thickness(0, 0, 0, 14),
                TextWrapping        = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            command.SetBinding(TextBox.TextProperty, new System.Windows.Data.Binding("CommandText") { Source = this });

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(_install);
            buttons.Children.Add(_copy);
            buttons.Children.Add(_continue);

            var body = new StackPanel { Margin = new Thickness(16) };
            body.Children.Add(_message);
            body.Children.Add(new TextBlock { Text = "Run this command:", Margin = new Thickness(0, 0, 0, 4), FontWeight = FontWeights.SemiBold });
            body.Children.Add(command);
            body.Children.Add(buttons);
            return body;
        }

        /// <summary>
        ///     The command the user would type. Bound rather than assigned so it follows a state change after an
        ///     install attempt.
        /// </summary>
        public string CommandText => _status.FixCommand ?? EfrpgToolGate.InstallCommand;

        private void Render()
        {
            _message.Text  = Describe(_status);
            _install.Content = _status.State == EfrpgToolState.NotFound ? "_Install efrpg" : "_Update efrpg";

            // 'dotnet tool install' needs the SDK, not just a runtime. Offering a button that cannot work would be
            // worse than not offering it, so it is disabled and the message says why.
            _install.IsEnabled = _status.DotnetSdkPresent;
        }

        private static string Describe(EfrpgToolStatus status)
        {
            string what;
            switch (status.State)
            {
                case EfrpgToolState.NotFound:
                    what = "The efrpg tool is not installed, so nothing can be generated yet.";
                    break;

                case EfrpgToolState.SchemaTooOld:
                    what = "The efrpg tool is too old for this version of the template. It reports wire format " +
                           status.SchemaVersion + ", and this template needs " + EfrpgToolGate.RequiredSchemaVersion + " or later.";
                    break;

                default:
                    what = "The efrpg tool was found but did not run.";
                    break;
            }

            if (!status.DotnetSdkPresent)
                what += Environment.NewLine + Environment.NewLine +
                        "No .NET SDK was found. Installing a dotnet tool needs the SDK, not just a runtime, so the " +
                        "button below is unavailable until one is installed.";

            return what;
        }

        private void OnCopy(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(CommandText);
                _copy.Content = "Copied";
            }
            catch (Exception)
            {
                // Another process can hold the clipboard open. The command is on screen and selectable regardless,
                // so there is nothing worth interrupting the user for.
            }
        }

        /// <summary>
        ///     async void is deliberate: this is an event handler, and the alternative - blocking the UI thread while
        ///     NuGet works - freezes Visual Studio for as long as the install takes.
        /// </summary>
        private async void OnInstall(object sender, RoutedEventArgs e)
        {
            SetBusy(true);
            try
            {
                var result = _status.State == EfrpgToolState.NotFound
                    ? await _gate.InstallAsync(CancellationToken.None)
                    : await _gate.UpdateAsync(CancellationToken.None);

                if (!result.Succeeded)
                {
                    // Verbatim, never summarised. The useful part of a failed install is usually the proxy or feed
                    // error buried inside it.
                    _message.Text = "That did not work:" + Environment.NewLine + Environment.NewLine +
                                    (string.IsNullOrEmpty(result.StandardError) ? result.StandardOutput : result.StandardError) +
                                    Environment.NewLine + Environment.NewLine +
                                    "Copy the command and run it yourself, or continue without the tool.";
                    return;
                }

                _status = await _gate.CheckAsync(CancellationToken.None);

                if (_status.State != EfrpgToolState.Ready)
                {
                    Render();
                    return;
                }

                // Visual Studio caches its environment at launch, so a tool installed just now is on disk but not on
                // the PATH this process inherited - and saving the .tt resolves the bare name. Saying so here is what
                // stops "it said it installed but generation still fails".
                var restart = _status.IsOnPath
                    ? string.Empty
                    : Environment.NewLine + Environment.NewLine +
                      "Restart Visual Studio before saving the .tt file, or generation will still fail: the tool is " +
                      "not on the PATH this Visual Studio process started with.";

                MessageBox.Show("efrpg " + _status.ToolVersion + " is installed and ready." + restart,
                    Title, MessageBoxButton.OK, MessageBoxImage.Information);

                Close(true);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void SetBusy(bool busy)
        {
            _install.IsEnabled  = !busy && _status.DotnetSdkPresent;
            _copy.IsEnabled     = !busy;
            _continue.IsEnabled = !busy;
            Cursor              = busy ? System.Windows.Input.Cursors.Wait : null;

            if (busy)
                _install.Content = "Working...";
            else
                Render();
        }

        private void Close(bool proceed)
        {
            Proceed = proceed;
            DialogResult = proceed;
            base.Close();
        }
    }
}
