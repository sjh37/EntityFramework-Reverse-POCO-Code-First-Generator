using System.Runtime.InteropServices;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;

namespace EntityFramework_Reverse_POCO_Generator
{
    /// <summary>
    ///     The extension's Visual Studio package. Until v4.0.1 this VSIX carried nothing but the item template and had
    ///     no assembly at all.
    /// </summary>
    /// <remarks>
    ///     Currently inert, and deliberately kept anyway: package registration is the one part of the VSIX plumbing
    ///     that is proven to work here, so it is a working anchor for anything later that genuinely needs a package.
    ///
    ///     It used to carry a Tools menu command declared in a .vsct. That never appeared in Visual Studio 2026 and
    ///     the whole route was removed. The package registered correctly - its GUID reaches
    ///     HKCU\...\18.0_&lt;hive&gt;_Config - but the command set GUID never reached the configuration at all, so no menu
    ///     was ever drawn, with no error logged anywhere. If a menu is wanted later, start by finding out why the
    ///     ctmenu resource is not merged; do not assume a context menu will fare better, because it is the same
    ///     mechanism pointed at a different parent.
    ///
    ///     The GUI reaches users through IWizard instead - see ReversePocoWizard - which VS instantiates straight from
    ///     the .vstemplate and which needs no package, no pkgdef and no command table.
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    public sealed class EfrpgPackage : ToolkitPackage
    {
        /// <summary>
        ///     Identifies the package to Visual Studio. Distinct from the VSIX identity in
        ///     source.extension.vsixmanifest, which is what makes an install upgrade rather than sit alongside the
        ///     existing extension. Neither may change once shipped.
        /// </summary>
        public const string PackageGuidString = "6f4b7d0e-6b1f-4a8e-9a1a-2b7c8d3e5f41";
    }
}
