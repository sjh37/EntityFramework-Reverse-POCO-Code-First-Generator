using System;
using System.IO;
using Community.VisualStudio.Toolkit;
using Efrpg.Gui;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Right-click a .tt to edit every setting the generator has, then regenerate.
    /// </summary>
    /// <remarks>
    ///     This one serves people who already bought: the connection dialog gets somebody started, and this is
    ///     where they live afterwards. It deliberately does not hijack double-click or Open - the installed base
    ///     works in the text editor and expects the file to open there.
    /// </remarks>
    [Command(PackageGuids.EfrpgCommandSetString, PackageIds.SettingsEditorCommand)]
    internal sealed class SettingsEditorCommand : BaseCommand<SettingsEditorCommand>
    {
        protected override void BeforeQueryStatus(EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Command.Visible = SolutionSelection.IsTemplate();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var item = SolutionSelection.Item();
            var path = SolutionSelection.Path(item);

            if (path == null || !File.Exists(path))
                return;

            var template = File.ReadAllText(path);

            string error;
            var catalogue = SettingsMetadataFiles.For(template, out error);

            if (catalogue == null)
            {
                await VS.MessageBox.ShowErrorAsync("EntityFramework Reverse POCO Generator", error);
                return;
            }

            var dialog = new SettingsEditorDialog(Path.GetFileName(path),
                SettingsEditSession.Load(template, catalogue));

            dialog.ShowModal();

            if (!dialog.Confirmed)
                return;

            if (!TemplateFileUpdater.Apply(item, path, dialog.Text, out error))
                await VS.MessageBox.ShowWarningAsync("EntityFramework Reverse POCO Generator",
                    "The .tt file was updated, but the generated code could not be refreshed automatically. " +
                    "Save the .tt file to regenerate it." + Environment.NewLine + Environment.NewLine + error);
        }
    }
}
