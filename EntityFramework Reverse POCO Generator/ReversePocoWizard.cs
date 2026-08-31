using System;
using System.Collections.Generic;
using System.Threading;
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
            var gate   = Gate();
            var status = CheckTool(gate);

            if (status == null)
                return; // The check itself failed; never block adding the file over that.

            if (status.State == EfrpgToolState.Ready)
                return;

            if (!Continue(gate, status))
                throw new WizardBackoutException("The efrpg tool is not ready, so the template was not added.");
        }

        private static EfrpgToolGate Gate()
        {
            return new EfrpgToolGate(new ProcessRunner());
        }

        private static EfrpgToolStatus CheckTool(EfrpgToolGate gate)
        {
            try
            {
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
        ///     Shows the gate dialog and returns whether to carry on adding the template.
        /// </summary>
        private static bool Continue(EfrpgToolGate gate, EfrpgToolStatus status)
        {
            var dialog = new EfrpgToolGateDialog(gate, status);
            dialog.ShowModal();
            return dialog.Proceed;
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
