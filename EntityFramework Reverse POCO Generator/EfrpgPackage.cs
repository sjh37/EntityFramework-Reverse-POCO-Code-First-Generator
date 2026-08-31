using System;
using System.Runtime.InteropServices;
using System.Threading;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     The extension's Visual Studio package: it owns the command table behind the .tt file's right-click menu.
    /// </summary>
    /// <remarks>
    ///     Getting a menu to appear at all took seven attempts. Two things are load bearing and neither produces an
    ///     error when missing. The project must set &lt;RegisterWithCodebase&gt;, or the pkgdef says Assembly= rather
    ///     than CodeBase= and Visual Studio cannot load this unsigned assembly to read the managed ctmenu resource
    ///     out of it. And VSPackage.resx must be marked MergeWithCTO, or the command table lands in a placeholder
    ///     resource that ProvideMenuResource never looks in.
    ///
    ///     **The autoload is not optional either.** The commands are DynamicVisibility and decide for themselves
    ///     whether to appear, which needs BeforeQueryStatus to run, which needs this package loaded. Without the
    ///     autoload it only loads when a command is first invoked - and a command nobody can see is never invoked.
    ///     SolutionExists is the right trigger because everything here acts on a file in a solution.
    ///
    ///     The wizard is separate and needs none of this: IWizard is instantiated straight from the .vstemplate,
    ///     with no package, no pkgdef and no command table. See ReversePocoWizard.
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class EfrpgPackage : ToolkitPackage
    {
        /// <summary>
        ///     Identifies the package to Visual Studio. Distinct from the VSIX identity in
        ///     source.extension.vsixmanifest, which is what makes an install upgrade rather than sit alongside the
        ///     existing extension. Neither may change once shipped.
        /// </summary>
        public const string PackageGuidString = "6f4b7d0e-6b1f-4a8e-9a1a-2b7c8d3e5f41";

        /// <summary>
        ///     Binds the commands. Not a wrapper around base that could be deleted: RegisterCommandsAsync discovers
        ///     the [Command]-attributed BaseCommand classes in this assembly and wires each to the ID it declares in
        ///     VSCommandTable.vsct. Without it the menu item appears but clicking it does nothing.
        /// </summary>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);
            await this.RegisterCommandsAsync();
        }
    }
}
