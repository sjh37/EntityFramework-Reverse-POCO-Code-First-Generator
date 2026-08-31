using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Efrpg.Gui;
using Microsoft.VisualStudio.PlatformUI;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Shows every edit the v3 to v4 upgrade wants to make, and asks before any of them is written.
    /// </summary>
    /// <remarks>
    ///     The file being changed is usually in source control and often customised, and the user is the only one
    ///     who knows whether their template is stock. Showing the edits costs one dialog and turns an irreversible
    ///     surprise into a decision.
    /// </remarks>
    public sealed class UpgradePreviewDialog : DialogWindow
    {
        public bool Confirmed { get; private set; }

        public UpgradePreviewDialog(string fileName, IReadOnlyList<TemplateUpgradeChange> changes)
        {
            Title                 = "Upgrade " + fileName + " to v4";
            Width                 = 820;
            Height                = 620;
            MinWidth              = 560;
            MinHeight             = 400;
            ResizeMode            = ResizeMode.CanResize;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            HasMinimizeButton     = false;

            Content = Build(changes);
        }

        private UIElement Build(IReadOnlyList<TemplateUpgradeChange> changes)
        {
            var upgrade = new Button { Content = "_Upgrade", MinWidth = 90, Margin = new Thickness(0, 0, 8, 0), Padding = new Thickness(10, 4, 10, 4), IsDefault = true };
            upgrade.Click += (s, e) => { Confirmed = true; DialogResult = true; Close(); };

            var cancel = new Button { Content = "_Cancel", MinWidth = 90, Padding = new Thickness(10, 4, 10, 4), IsCancel = true };
            cancel.Click += (s, e) => { Confirmed = false; DialogResult = false; Close(); };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 12, 0, 0) };
            buttons.Children.Add(upgrade);
            buttons.Children.Add(cancel);

            var list = new StackPanel();
            foreach (var change in changes)
                list.Children.Add(Card(change));

            var grid = new Grid { Margin = new Thickness(16) };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var heading = new TextBlock
            {
                Text = changes.Count + (changes.Count == 1 ? " change" : " changes") +
                       " will be made. Comments, formatting and every other setting are left exactly as they are.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 12)
            };

            var scroller = new ScrollViewer
            {
                Content = list,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };

            Place(grid, heading, 0);
            Place(grid, scroller, 1);
            Place(grid, buttons, 2);
            return grid;
        }

        private static UIElement Card(TemplateUpgradeChange change)
        {
            var panel = new StackPanel { Margin = new Thickness(0, 0, 0, 16) };

            panel.Children.Add(new TextBlock
            {
                Text = change.Description,
                FontWeight = FontWeights.SemiBold,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 6)
            });

            panel.Children.Add(Code(change.Before, "Remove"));

            if (change.After.Length > 0)
                panel.Children.Add(Code(change.After, "Add"));

            return panel;
        }

        private static UIElement Code(string text, string label)
        {
            var block = new TextBlock
            {
                Text = text,
                FontFamily = new FontFamily("Consolas"),
                TextWrapping = TextWrapping.NoWrap,
                Margin = new Thickness(0, 0, 0, 4)
            };

            var scroller = new ScrollViewer
            {
                Content = block,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                MaxHeight = 220
            };

            var heading = new TextBlock { Text = label, Opacity = 0.75, Margin = new Thickness(0, 0, 0, 2) };

            var panel = new StackPanel { Margin = new Thickness(12, 0, 0, 8) };
            panel.Children.Add(heading);
            panel.Children.Add(scroller);
            return panel;
        }

        private static void Place(Grid grid, UIElement element, int row)
        {
            Grid.SetRow(element, row);
            grid.Children.Add(element);
        }
    }
}
