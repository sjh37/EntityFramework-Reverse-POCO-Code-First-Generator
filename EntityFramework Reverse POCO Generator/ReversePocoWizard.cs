using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using EnvDTE;
using Efrpg.Gui;
using Microsoft.VisualStudio.TemplateWizard;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Runs when a user adds a Reverse POCO template through Add - New Item. This is the newcomer's first five
    ///     minutes, which is the problem the whole GUI exists to solve.
    /// </summary>
    /// <remarks>
    ///     IWizard is reached from the &lt;WizardExtension&gt; element in MyTemplate.vstemplate: Visual Studio loads this
    ///     assembly and constructs this class itself. It needs no package, no pkgdef and no command table - which is
    ///     the point, because the command table route was tried first and never produced a menu in VS 2026.
    ///
    ///     Everything decided here comes from Efrpg.Gui.Core, which has no Visual Studio reference and is unit
    ///     tested. This class is a shell: show, ask, and write the answers into replacementsDictionary.
    /// </remarks>
    public class ReversePocoWizard : IWizard
    {
        /// <summary>
        ///     Called once before the template is unpacked, which is the only useful moment to stop: throwing
        ///     WizardBackoutException here makes Visual Studio clean up the half-created item rather than leaving a
        ///     broken .tt behind.
        /// </summary>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            // A brand-new .tt cannot be reverse engineered: it ships with **TODO** as the database name and efrpg
            // refuses that outright. So the tool check is the only thing worth doing before the file exists; asking
            // for a connection string and reading schema belongs after, once there is a file to write settings into.
            var status = CheckTool();

            if (status == null)
                return; // The check itself failed; never block adding the file over that.

            if (status.State == EfrpgToolState.Ready)
                return;

            if (!Continue(status))
                throw new WizardBackoutException("The efrpg tool is not ready, so the template was not added.");
        }

        private static EfrpgToolStatus CheckTool()
        {
            try
            {
                var gate = new EfrpgToolGate(new ProcessRunner());
                return gate.CheckAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                // Whatever went wrong, it is not worth stopping the user adding a file over. They will get the
                // generator's own error when they save the .tt.
                return null;
            }
        }

        /// <summary>
        ///     Reports what is wrong and lets the user decide. The exact command is always shown, because it is the
        ///     escape hatch for anyone behind a proxy, on an internal feed, or without permission to install.
        /// </summary>
        private static bool Continue(EfrpgToolStatus status)
        {
            var message = Describe(status) + Environment.NewLine + Environment.NewLine +
                          "Add the template anyway?";

            return MessageBox.Show(message, "EntityFramework Reverse POCO Generator",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private static string Describe(EfrpgToolStatus status)
        {
            var what = status.State == EfrpgToolState.NotFound
                ? "The efrpg tool is not installed, so nothing can be generated yet."
                : status.State == EfrpgToolState.SchemaTooOld
                    ? "The efrpg tool is too old for this version of the template and must be updated."
                    : "The efrpg tool was found but did not run.";

            var sdk = status.DotnetSdkPresent
                ? string.Empty
                : Environment.NewLine +
                  "No .NET SDK was found either. 'dotnet tool install' needs the SDK, not just a runtime.";

            return what + Environment.NewLine + Environment.NewLine +
                   "To fix this, run:" + Environment.NewLine +
                   "    " + status.FixCommand + sdk;
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        /// <summary>
        ///     Every file in the template is wanted, so this never filters anything out.
        /// </summary>
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
