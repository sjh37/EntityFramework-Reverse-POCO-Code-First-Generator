using System;
using System.Collections.Generic;
using System.IO;
using Community.VisualStudio.Toolkit;
using Efrpg.Gui;
using EnvDTE;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Offers the v4 upgrade the first time a v3 template is opened, as an information bar across the top of
    ///     that document rather than a dialog.
    /// </summary>
    /// <remarks>
    ///     The v3 installed base is the whole reason Phase 2b exists, and almost none of it will go looking for a
    ///     right-click command it does not know is there. The bar is the least intrusive thing Visual Studio has
    ///     that still cannot be missed: it does not steal focus, it does not block the editor, and it can be
    ///     dismissed for good in one click.
    ///
    ///     Once per file per session, and never again after "Don't ask again". The command on the right-click menu
    ///     stays either way.
    /// </remarks>
    internal static class V3UpgradeOffer
    {
        private const string Upgrade = "upgrade";
        private const string Never   = "never";

        private static readonly HashSet<string> Offered = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>Hooks document opening. Called once, from the package, on the main thread.</summary>
        public static void Start()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            VS.Events.DocumentEvents.Opened += path =>
            {
                // VSSDK007 wants this awaited, which an event handler cannot do. FileAndForget puts any fault in
                // the activity log rather than losing it, and nothing waits on an offer.
#pragma warning disable VSSDK007
                ThreadHelper.JoinableTaskFactory.RunAsync(() => OfferAsync(path)).FileAndForget("efrpg/gui/v3offer");
#pragma warning restore VSSDK007
            };
        }

        private static async Task OfferAsync(string path)
        {
            if (path == null || !path.EndsWith(".tt", StringComparison.OrdinalIgnoreCase) || !File.Exists(path))
                return;

            if (!Offered.Add(path) || !UpgradeOptions.Instance.OfferUpgradeToV4)
                return;

            string text;
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception)
            {
                return;
            }

            if (!TemplateUpgrade.IsV3(text))
                return;

            var model = new InfoBarModel(
                new[]
                {
                    new InfoBarTextSpan("This Reverse POCO template is version 3. Version 4 reads the database through the efrpg tool and is where new features land. ")
                },
                new InfoBarActionItem[]
                {
                    new InfoBarHyperlink("Upgrade to v4...", Upgrade),
                    new InfoBarHyperlink("Don't ask again", Never)
                },
                KnownMonikers.StatusInformation,
                true);

            var bar = await VS.InfoBar.CreateAsync(path, model);
            if (bar == null)
                return;

            bar.ActionItemClicked += (sender, args) =>
            {
                // Visual Studio raises this on the main thread; the analyser wants it stated before the action
                // item, a COM object, is touched.
                ThreadHelper.ThrowIfNotOnUIThread();
                OnAction(bar, path, args.ActionItem.ActionContext as string);
            };

            await bar.TryShowInfoBarUIAsync();
        }

        private static void OnAction(InfoBar bar, string path, string action)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            bar.Close();

            if (action == Never)
            {
                UpgradeOptions.Instance.OfferUpgradeToV4 = false;
                UpgradeOptions.Instance.Save();
                return;
            }

            if (action != Upgrade)
                return;

            // Not awaited: this is a click handler, and the flow shows a modal dialog of its own. Faults go to
            // the activity log through FileAndForget.
#pragma warning disable VSSDK007
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                await TemplateUpgradeFlow.RunAsync(FindItem(path), path);
            }).FileAndForget("efrpg/gui/v3offer/upgrade");
#pragma warning restore VSSDK007
        }

        private static ProjectItem FindItem(string path)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                var dte = Package.GetGlobalService(typeof(DTE)) as DTE;

                return dte == null ? null : dte.Solution.FindProjectItem(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
