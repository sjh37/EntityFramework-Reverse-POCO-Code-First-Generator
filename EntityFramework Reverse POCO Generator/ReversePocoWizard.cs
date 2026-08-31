using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using EnvDTE;
using Efrpg.Gui;
using Microsoft.VisualStudio.Shell;
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

            // The gate only gates itself. A tool that is missing, stale or unrunnable is worth stopping for, but
            // when it is fine - the normal case - there is nothing to say and the user goes straight to the
            // questions. A null status means the check itself fell over, which is never worth blocking on.
            if (status != null && status.State != EfrpgToolState.Ready && !Continue(gate, status))
                throw new WizardBackoutException("The efrpg tool is not ready, so the template was not added.");

            Ask(SuggestedDbContextName(replacementsDictionary));
        }

        /// <summary>
        ///     Turns the name the user typed in Add - New Item into a DbContext name: "Northwind.tt" becomes
        ///     "NorthwindDbContext". Falls back to the template's own default when there is nothing useful to use.
        /// </summary>
        private static string SuggestedDbContextName(IDictionary<string, string> replacements)
        {
            string safeName;
            if (replacements == null || !replacements.TryGetValue("$safeitemname$", out safeName) || string.IsNullOrEmpty(safeName))
                return "MyDbContext";

            return safeName.EndsWith("DbContext", StringComparison.OrdinalIgnoreCase) ? safeName : safeName + "DbContext";
        }

        private ProjectItem _templateItem;
        private string _templatePath;
        private TemplateConfiguration _answers;

        /// <summary>
        ///     Asks which database and template to target, for a connection string and for a DbContext name.
        ///     Skipping is always allowed: the template is a perfectly good starting point with the placeholder
        ///     still in it, and a wizard that will not let you out is worse than one that asks nothing.
        /// </summary>
        private void Ask(string suggestedDbContextName)
        {
            var dialog = new ConnectionDialog(TemplateConfiguration.ForNewTemplate(suggestedDbContextName), true);
            dialog.ShowModal();

            if (dialog.Confirmed)
                _answers = dialog.Result;
        }

        /// <summary>
        ///     Writes the answers into the .tt, one line each, leaving everything else byte for byte as it was, and
        ///     regenerates.
        /// </summary>
        /// <remarks>
        ///     Adding a .tt fires its custom tool immediately, well before this runs, so the output sitting beside
        ///     the file at this point is always the efrpg tool's "the connection string still contains **TODO**"
        ///     error. Regenerating is therefore part of applying the answers, not an optional extra - see
        ///     <see cref="TemplateFileUpdater"/> for why it has to go through the editor.
        ///
        ///     Failing here must never break adding the file: the user still has a working template, just one they
        ///     have to save themselves.
        /// </remarks>
        private void ApplyAnswers()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (_templatePath == null || _answers == null || !File.Exists(_templatePath))
                return;

            try
            {
                var settings = new TemplateSettingsFile(File.ReadAllText(_templatePath));
                string error;

                TemplateFileUpdater.Apply(_templateItem, _templatePath, _answers.ApplyTo(settings), out error);
            }
            catch (Exception)
            {
                // A read-only file, a virus scanner holding a lock, anything. The template is already added and
                // usable; interrupting the user now would be worse than leaving them to edit one line.
            }
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

        /// <summary>
        ///     The .tt now exists on disk, so this is the first moment the answers can be written into it.
        /// </summary>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // Every IWizard callback runs on the UI thread, so this always holds - the analyser just
            // wants it stated before anything touches the DTE object model.
            ThreadHelper.ThrowIfNotOnUIThread();

            if (projectItem == null)
                return;

            try
            {
                var path = projectItem.FileNames[1];
                if (path != null && path.EndsWith(".tt", StringComparison.OrdinalIgnoreCase))
                {
                    _templateItem = projectItem;
                    _templatePath = path;
                }
            }
            catch (Exception)
            {
                // FileNames throws for item kinds that have no path. Nothing to write to, nothing to report.
            }
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ApplyAnswers();
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
