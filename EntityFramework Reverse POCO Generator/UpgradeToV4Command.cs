using System;
using System.IO;
using Community.VisualStudio.Toolkit;
using Efrpg.Gui;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Right-click a v3 .tt to migrate it to v4: six mechanical edits, shown before any of them is written.
    /// </summary>
    /// <remarks>
    ///     The v3 base is large and the edits are the same ones in the upgrade guide, so doing them by hand is
    ///     error-prone work nobody enjoys. Getting that base onto v4 is also what makes the efrpg tool ubiquitous.
    ///
    ///     It refuses whenever the file is not the shape it knows how to edit. A half-applied migration leaves a
    ///     template that neither compiles nor matches the guide, which is worse than not offering the button.
    /// </remarks>
    [Command(PackageGuids.EfrpgCommandSetString, PackageIds.UpgradeToV4Command)]
    internal sealed class UpgradeToV4Command : BaseCommand<UpgradeToV4Command>
    {
        /// <summary>
        ///     Shown only for a template that actually includes the v3 file, so it never appears on the v4 files
        ///     the same menu serves.
        /// </summary>
        protected override void BeforeQueryStatus(EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Command.Visible = SolutionSelection.IsTemplate() && IsV3(SolutionSelection.Path(SolutionSelection.Item()));
        }

        private static bool IsV3(string path)
        {
            try
            {
                return path != null && File.Exists(path) && TemplateUpgrade.IsV3(File.ReadAllText(path));
            }
            catch (Exception)
            {
                // Unreadable for any reason - locked, gone since the menu opened. Not a file to offer this for.
                return false;
            }
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = SolutionSelection.Item();

            await TemplateUpgradeFlow.RunAsync(item, SolutionSelection.Path(item));
        }
    }
}
