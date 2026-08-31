using System;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     What the user has selected in Solution Explorer, as a DTE <see cref="ProjectItem"/> and its path.
    /// </summary>
    /// <remarks>
    ///     DTE rather than the toolkit's async solution API, because <c>BeforeQueryStatus</c> cannot await anything -
    ///     it has to answer while the menu is being built - and the regeneration afterwards needs the same
    ///     ProjectItem. One synchronous accessor for both keeps the two answers consistent.
    /// </remarks>
    internal static class SolutionSelection
    {
        /// <summary>The single selected item, or null when the selection is empty or is more than one thing.</summary>
        public static ProjectItem Item()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                if (dte == null || dte.SelectedItems.Count != 1)
                    return null;

                return dte.SelectedItems.Item(1).ProjectItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>The path of the selected item, or null when it has none.</summary>
        public static string Path(ProjectItem item)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (item == null)
                return null;

            try
            {
                return item.FileNames[1];
            }
            catch (Exception)
            {
                // Item kinds with no path on disk throw rather than returning null.
                return null;
            }
        }

        /// <summary>
        ///     True when the selection is a .tt file. Both commands sit on the generic item node menu, which Visual
        ///     Studio shows for every file in the solution, so without this they would clutter the right-click menu
        ///     of every file in every project.
        /// </summary>
        public static bool IsTemplate()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var path = Path(Item());

            return path != null && path.EndsWith(".tt", StringComparison.OrdinalIgnoreCase);
        }
    }
}
