using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using EnvDTE;
using Efrpg.Gui;
using Microsoft.VisualStudio.TemplateWizard;
using VSLangProj;

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

        /// <summary>
        ///     What the shipped template carries, so the user edits a database name rather than composing a
        ///     connection string from nothing.
        /// </summary>
        private const string DefaultConnectionString =
            "Data Source=(local);Initial Catalog=" + TemplateSettingWriter.Placeholder +
            ";Integrated Security=True;MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true";

        private ProjectItem _templateItem;
        private string _templatePath;
        private string _connectionString;
        private string _dbContextName;

        /// <summary>
        ///     Asks for the connection string and DbContext name. Skipping is always allowed: the template is a
        ///     perfectly good starting point with the placeholder still in it, and a wizard that will not let you
        ///     out is worse than one that asks nothing.
        /// </summary>
        private void Ask(string suggestedDbContextName)
        {
            var dialog = new ConnectionDialog(DefaultConnectionString, suggestedDbContextName);
            dialog.ShowModal();

            if (!dialog.Confirmed)
                return;

            _connectionString = dialog.ConnectionString;
            _dbContextName    = dialog.DbContextName;
        }

        /// <summary>
        ///     Writes the answers into the .tt on disk, one line each, leaving everything else byte for byte as it
        ///     was. Failing here must never break adding the file: the user still has a working template, just one
        ///     they have to edit themselves.
        /// </summary>
        private void ApplyAnswers()
        {
            if (_templatePath == null || _connectionString == null || !File.Exists(_templatePath))
                return;

            try
            {
                var writer = new TemplateSettingWriter(File.ReadAllText(_templatePath));

                writer.TrySetString("ConnectionString", _connectionString);

                if (!string.IsNullOrEmpty(_dbContextName))
                {
                    writer.TrySetString("DbContextName", _dbContextName);
                    writer.TrySetString("ConnectionStringName", _dbContextName);
                }

                File.WriteAllText(_templatePath, writer.Text);

                Regenerate();
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
            ApplyAnswers();
        }

        /// <summary>
        ///     Runs the T4 again, because Visual Studio already ran it once - on the template as unpacked, with the
        ///     placeholder still in the connection string.
        /// </summary>
        /// <remarks>
        ///     Adding a .tt to a project fires its custom tool immediately, well before RunFinished, so the first
        ///     generated output is always the efrpg tool's "the connection string still contains **TODO**" error.
        ///     Writing the real connection string afterwards fixes the .tt but leaves that error sitting in the
        ///     generated .cs, which is exactly the confusing first impression this wizard exists to remove.
        ///
        ///     Re-running is done rather than trying to suppress the first pass: there is no supported way to stop
        ///     the custom tool firing on add, and a second pass is cheap next to the schema read it performs.
        /// </remarks>
        private void Regenerate()
        {
            try
            {
                var vsProjectItem = _templateItem.Object as VSProjectItem;
                if (vsProjectItem != null)
                    vsProjectItem.RunCustomTool();
            }
            catch (Exception)
            {
                // The project system may not expose RunCustomTool, or the tool may fail for reasons of its own -
                // an unreachable database being the obvious one. The .tt is correct either way, and saving it
                // regenerates. Interrupting the user here would be worse than leaving them one keystroke.
            }
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
