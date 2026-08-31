using System;
using System.IO;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     Writes new text into a .tt and gets the T4 to run against it.
    /// </summary>
    /// <remarks>
    ///     Writing the file is the easy half. Getting the generated output to match it is not, and getting it wrong
    ///     looks identical to not writing the file at all: the .tt on disk is right, and the .cs beside it still
    ///     carries the previous run's error.
    ///
    ///     **When the document is open, going through the editor buffer is the only correct route.** The T4 custom
    ///     tool is an IVsSingleFileGenerator, and Visual Studio hands it the contents of the *editor buffer*, not the
    ///     file on disk. Writing straight to disk behind an open document therefore regenerates from the stale text
    ///     the buffer still holds - the template is added and immediately opened, so this is the normal case, not an
    ///     edge case. Replacing the buffer and saving is also exactly what the user does by hand, so it is the path
    ///     Visual Studio supports best: the save runs the generator on its own and nothing else has to be invoked.
    ///
    ///     When the document is not open there is no buffer to disagree with, so the file is written directly and
    ///     the generator asked to run. <c>VSProjectItem.RunCustomTool</c> is tried first; it is unavailable in some
    ///     project systems, where reassigning the CustomTool property has the same effect.
    /// </remarks>
    internal static class TemplateFileUpdater
    {
        /// <summary>
        ///     Replaces the whole content of the item's file and regenerates. Returns false with a reason when the
        ///     file was written but generation could not be triggered - the .tt is correct either way, and saving it
        ///     regenerates, so this is worth reporting but never worth undoing.
        /// </summary>
        public static bool Apply(ProjectItem item, string path, string newText, out string error)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            error = null;

            var document = OpenDocument(item);
            if (document != null)
                return SaveThroughEditor(document, newText, out error);

            File.WriteAllText(path, newText);
            return RunCustomTool(item, out error);
        }

        private static Document OpenDocument(ProjectItem item)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                return item.Document;
            }
            catch (Exception)
            {
                // Some item kinds have no document at all and throw rather than returning null.
                return null;
            }
        }

        /// <summary>
        ///     Replaces the buffer and saves. The save is what runs the T4 - the same thing that happens when the
        ///     user presses Ctrl+S, which is the mechanism the whole generator is built around.
        /// </summary>
        private static bool SaveThroughEditor(Document document, string newText, out string error)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            error = null;

            try
            {
                var text = (TextDocument) document.Object("TextDocument");
                var start = text.StartPoint.CreateEditPoint();

                start.ReplaceText(text.EndPoint, newText, (int) vsEPReplaceTextOptions.vsEPReplaceTextKeepMarkers);
                document.Save();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static bool RunCustomTool(ProjectItem item, out string error)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            error = null;

            try
            {
                var vsProjectItem = item.Object as VSProjectItem;
                if (vsProjectItem != null)
                {
                    vsProjectItem.RunCustomTool();
                    return true;
                }

                return ReassignCustomTool(item, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        ///     Clearing the CustomTool property and putting it back re-runs the generator. This is the fallback for
        ///     project systems that do not expose VSProjectItem - the SDK-style .NET projects most users of the
        ///     EF Core templates are on.
        /// </summary>
        private static bool ReassignCustomTool(ProjectItem item, out string error)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            error = null;

            try
            {
                var property = item.Properties.Item("CustomTool");
                var tool = property.Value as string;

                if (string.IsNullOrEmpty(tool))
                {
                    error = "The .tt file has no custom tool set, so nothing regenerates it.";
                    return false;
                }

                property.Value = string.Empty;
                property.Value = tool;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
