using System.Threading;
using Community.VisualStudio.Toolkit;
using Efrpg.Gui;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Right-click command that reports whether the efrpg dotnet tool on this machine can serve the generator,
    ///     and offers to install or update it.
    /// </summary>
    /// <remarks>
    ///     The same gate the wizard runs on Add - New Item, reachable again afterwards. A user who chose "Continue
    ///     anyway" during the wizard, or who installed the tool later, needs a way back to it that does not involve
    ///     deleting and re-adding the .tt file.
    /// </remarks>
    [Command(PackageGuids.EfrpgCommandSetString, PackageIds.CheckEfrpgToolCommand)]
    internal sealed class CheckEfrpgToolCommand : BaseCommand<CheckEfrpgToolCommand>
    {
        protected override void BeforeQueryStatus(System.EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Command.Visible = SolutionSelection.IsTemplate();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var gate   = new EfrpgToolGate(new ProcessRunner());
            var status = await gate.CheckAsync(CancellationToken.None);

            if (status.State == EfrpgToolState.Ready)
            {
                await VS.MessageBox.ShowAsync("EntityFramework Reverse POCO Generator",
                    "efrpg " + status.ToolVersion + " is installed and up to date (wire format " +
                    status.SchemaVersion + ").");
                return;
            }

            new EfrpgToolGateDialog(gate, status).ShowModal();
        }
    }
}
