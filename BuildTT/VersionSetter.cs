using System.IO;
using System.IO.Compression;
using System.Text;

namespace BuildTT
{
    public class VersionSetter
    {
        private readonly string _root;
        private readonly string _version;

        public VersionSetter(string root, string version)
        {
            _root    = root;
            _version = version;
        }

        public void SetVersions()
        {
            UpdateVstemplate();
            UpdateVsixManifest();

            // Deliberately does NOT stamp the efrpg dotnet tool, which lives in its own repository and versions
            // independently of
            // version.txt - see the "Wire format contract" section of AGENTS.md.

            DeleteFiles(Path.Combine(_root, "ItemTemplate\\ItemTemplates"), "*");
            var zipFile = Path.Combine(_root, "ItemTemplate\\ItemTemplates\\efrpoco.zip");
            BuildEfrpocoZip(zipFile);
            File.Copy(Path.Combine(_root, "LICENSE"), Path.Combine(_root, "ItemTemplate\\license.txt"), true);

            File.Copy(zipFile, Path.Combine(_root, "EntityFramework Reverse POCO Generator\\ItemTemplates\\efrpoco.zip"), true);
            File.Copy(zipFile, Path.Combine(_root, "EntityFramework Reverse POCO Generator\\ItemTemplates\\CSharp\\Data\\1033\\efrpoco.zip"), true);
            File.Copy(zipFile, Path.Combine(_root, "EntityFramework Reverse POCO Generator\\ItemTemplates\\CSharp\\1033\\efrpoco.zip"), true);
        }

        private void UpdateVstemplate()
        {
            var filename = Path.Combine(_root, "ItemTemplate\\MyTemplate.vstemplate");
            using (var tt = File.CreateText(filename))
            {
                tt.WriteLine("<VSTemplate Version=\"3.0.0\" xmlns=\"http://schemas.microsoft.com/developer/vstemplate/2005\" Type=\"Item\">");
                tt.WriteLine("    <TemplateData>");
                tt.WriteLine("        <DefaultName>Database.tt</DefaultName>");
                tt.WriteLine("        <Name>EntityFramework Reverse POCO Code First Generator</Name>");
                tt.WriteLine("        <Description>Reverse engineers an existing database and generates EntityFramework Code First POCO classes, Configuration mappings and DbContext.</Description>");
                tt.WriteLine("        <ProjectType>CSharp</ProjectType>");
                tt.WriteLine("        <SortOrder>10</SortOrder>");
                tt.WriteLine("        <Icon>TemplateIcon.ico</Icon>");
                tt.WriteLine("        <PreviewImage>PreviewImage.png</PreviewImage>");
                tt.WriteLine("        <NumberOfParentCategoriesToRollUp>1</NumberOfParentCategoriesToRollUp>");
                tt.WriteLine($"        <Version>{_version}</Version>");
                tt.WriteLine("    </TemplateData>");
                tt.WriteLine("    <TemplateContent>");
                tt.WriteLine("        <ProjectItem SubType=\"\" TargetFileName=\"$fileinputname$.tt\" ReplaceParameters=\"false\">Database.tt</ProjectItem>");
                tt.WriteLine("        <ProjectItem SubType=\"\" TargetFileName=\"EF.Reverse.POCO.v4.ttinclude\" ReplaceParameters=\"false\">EF.Reverse.POCO.v4.ttinclude</ProjectItem>");
                tt.WriteLine("    </TemplateContent>");
                // Reached by Visual Studio when the user picks this template from Add - New Item. This is how the GUI
                // is invoked: no package, no pkgdef and no command table, all of which the .vsct route needed and none
                // of which ever produced a menu in VS 2026. The assembly name must match AssemblyInfo exactly - a wrong
                // strong name fails obscurely, with the template simply added and no wizard run.
                tt.WriteLine("    <WizardExtension>");
                tt.WriteLine($"        <Assembly>EntityFramework Reverse POCO Generator, Version={_version}.0, Culture=neutral, PublicKeyToken=null</Assembly>");
                tt.WriteLine("        <FullClassName>EntityFramework_Reverse_POCO_Generator.ReversePocoWizard</FullClassName>");
                tt.WriteLine("    </WizardExtension>");
                tt.Write("</VSTemplate>");
            }
        }

        private void UpdateVsixManifest()
        {
            var filename = Path.Combine(_root, "EntityFramework Reverse POCO Generator\\source.extension.vsixmanifest");

            // Tags.txt is the single source of truth for the marketplace tag list. It used to be repeated
            // here as a string literal as well, and the two drifted - SQLite, MySQL and Oracle reached
            // Tags.txt but not the manifest, which is the copy the marketplace actually reads.
            var tags = File.ReadAllText(Path.Combine(_root, "EntityFramework Reverse POCO Generator\\Tags.txt")).Trim();

            using (var tt = File.CreateText(filename))
            {
                tt.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                tt.WriteLine("<PackageManifest Version=\"2.0.0\" xmlns=\"http://schemas.microsoft.com/developer/vsx-schema/2011\" xmlns:d=\"http://schemas.microsoft.com/developer/vsx-schema-design/2011\">");
                tt.WriteLine("    <Metadata>");
                tt.WriteLine($"        <Identity Id=\"EntityFramework_Reverse_POCO_Generator..d542a934-8bd6-4136-b490-5f0049d62033\" Version=\"{_version}\" Language=\"en-US\" Publisher=\"Simon Hughes\" />");
                tt.WriteLine("        <DisplayName>EntityFramework Reverse POCO Generator</DisplayName>");
                tt.WriteLine("        <Description xml:space=\"preserve\">Reverse engineers an existing database and generates EntityFramework Code First POCO classes, Configuration mappings and DbContext.</Description>");
                tt.WriteLine("        <MoreInfo>https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator</MoreInfo>");
                tt.WriteLine("        <License>license.txt</License>");
                tt.WriteLine("        <Icon>TemplateIcon.ico</Icon>");
                tt.WriteLine("        <PreviewImage>PreviewImage.png</PreviewImage>");
                tt.WriteLine($"        <Tags>{tags}</Tags>");
                tt.WriteLine("    </Metadata>");
                tt.WriteLine("    <Installation>");
                // No upper bound on the 17.x entries. From Visual Studio 2026 compatibility is decided by API
                // version, not product version: VS supports API 17.x, reads only the lower bound and ignores
                // the upper one. An open range is what VS 2026 emits for new extensions, and it means this
                // never needs touching again for a new major release. VS 2022 still uses the old product-range
                // model, and an open range satisfies it too. The [15.0,17.0) entries below cover VS 2017/2019,
                // which predate all of this.
                tt.WriteLine("        <InstallationTarget Version=\"[15.0,17.0)\" Id=\"Microsoft.VisualStudio.Community\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[17.0,)\" Id=\"Microsoft.VisualStudio.Community\">");
                tt.WriteLine("            <ProductArchitecture>amd64</ProductArchitecture>");
                tt.WriteLine("        </InstallationTarget>");
                tt.WriteLine("        <InstallationTarget Version=\"[15.0,17.0)\" Id=\"Microsoft.VisualStudio.Pro\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[17.0,)\" Id=\"Microsoft.VisualStudio.Pro\">");
                tt.WriteLine("            <ProductArchitecture>amd64</ProductArchitecture>");
                tt.WriteLine("        </InstallationTarget>");
                tt.WriteLine("        <InstallationTarget Version=\"[15.0,17.0)\" Id=\"Microsoft.VisualStudio.Enterprise\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[17.0,)\" Id=\"Microsoft.VisualStudio.Enterprise\">");
                tt.WriteLine("            <ProductArchitecture>amd64</ProductArchitecture>");
                tt.WriteLine("        </InstallationTarget>");
                tt.WriteLine("    </Installation>");
                tt.WriteLine("    <Assets>");
                tt.WriteLine("        <Asset Type=\"Microsoft.VisualStudio.ItemTemplate\" d:Source=\"File\" Path=\"ItemTemplates\" d:TargetPath=\"ItemTemplates\\efrpoco.zip\" />");
                // Without this the package assembly ships but Visual Studio never loads it: the pkgdef is present
                // and inert. See EfrpgPackage in the VSIX project.
                tt.WriteLine("        <Asset Type=\"Microsoft.VisualStudio.VsPackage\" d:Source=\"Project\" d:ProjectName=\"%CurrentProject%\" Path=\"|%CurrentProject%;PkgdefProjectOutputGroup|\" />");
                // Registers the assembly by name so the template engine can resolve the IWizard named in
                // MyTemplate.vstemplate. Shipping the dll inside the VSIX is not enough on its own: without this the
                // user gets "this template attempted to load component assembly ..." when they add the item.
                tt.WriteLine("        <Asset Type=\"Microsoft.VisualStudio.Assembly\" d:Source=\"Project\" d:ProjectName=\"%CurrentProject%\" Path=\"|%CurrentProject%|\" AssemblyName=\"|%CurrentProject%;AssemblyName|\" />");
                tt.WriteLine("    </Assets>");
                tt.WriteLine("    <Prerequisites>");
                tt.WriteLine("        <Prerequisite Id=\"Microsoft.VisualStudio.Component.TextTemplating\" Version=\"[15.0,)\" DisplayName=\"Text Template Transformation\" />");
                tt.WriteLine("        <Prerequisite Id=\"Microsoft.VisualStudio.Component.CoreEditor\" Version=\"[15.0,)\" DisplayName=\"Visual Studio core editor\" />");
                tt.WriteLine("    </Prerequisites>");
                tt.Write("</PackageManifest>");
            }
        }

        private void BuildEfrpocoZip(string zipfile)
        {
            using (var zipToOpen = new FileStream(zipfile, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    CreateZipFile(archive, "Database.tt", Path.Combine(_root, "EntityFramework.Reverse.POCO.Generator\\Database.tt"));
                    CreateZipFile(archive, "EF.Reverse.POCO.v4.ttinclude", Path.Combine(_root, "EntityFramework.Reverse.POCO.Generator\\EF.Reverse.POCO.v4.ttinclude"));
                    CreateZipFile(archive, "MyTemplate.vstemplate", Path.Combine(_root, "ItemTemplate\\MyTemplate.vstemplate"));
                    CreateZipFile(archive, "PreviewImage.png", Path.Combine(_root, "EntityFramework Reverse POCO Generator\\PreviewImage.png"));
                    CreateZipFile(archive, "TemplateIcon.ico", Path.Combine(_root, "EntityFramework Reverse POCO Generator\\TemplateIcon.ico"));
                }
            }

            void CreateZipFile(ZipArchive archive, string zipFilename, string sourceFile)
            {
                var zipFile = archive.CreateEntry(zipFilename);
                using (var writer = new BinaryWriter(zipFile.Open(), Encoding.UTF8))
                {
                    writer.Write(File.ReadAllBytes(sourceFile));
                    writer.Flush();
                }
            }
        }

        private void DeleteFiles(string folder, string pattern)
        {
            foreach (var file in Directory.EnumerateFiles(folder, pattern))
            {
                File.Delete(file);
            }
        }

        /*private void CopyFiles(string sourcePath, string targetPath, string pattern)
        {
            var files = Directory.EnumerateFiles(sourcePath, pattern);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName == null)
                    continue;

                var destFile = Path.Combine(targetPath, fileName);
                File.Copy(file, destFile, true);
            }
        }*/
    }
}