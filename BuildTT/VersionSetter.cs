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
            UpdateVsixmanifest();

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
                tt.WriteLine("        <ProjectItem SubType=\"\" TargetFileName=\"EF.Reverse.POCO.v3.ttinclude\" ReplaceParameters=\"false\">EF.Reverse.POCO.v3.ttinclude</ProjectItem>");
                tt.WriteLine("    </TemplateContent>");
                tt.Write("</VSTemplate>");
            }
        }

        private void UpdateVsixmanifest()
        {
            var filename = Path.Combine(_root, "EntityFramework Reverse POCO Generator\\source.extension.vsixmanifest");
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
                tt.WriteLine("        <Tags>Reverse Poco, Data, Entity Framework, Code Generator, Database, reverse engineering, C#, SQL Server, POCO, Code First, CodeFirst</Tags>");
                tt.WriteLine("    </Metadata>");
                tt.WriteLine("    <Installation>");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.Community\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.Pro\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.Enterprise\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.VSWinExpress\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.VWDExpress\" />");
                tt.WriteLine("        <InstallationTarget Version=\"[14.0,17.0)\" Id=\"Microsoft.VisualStudio.VSWinDesktopExpress\" />");
                tt.WriteLine("    </Installation>");
                tt.WriteLine("    <Assets>");
                tt.WriteLine("        <Asset Type=\"Microsoft.VisualStudio.ItemTemplate\" d:Source=\"File\" Path=\"ItemTemplates\" d:TargetPath=\"ItemTemplates\\efrpoco.zip\" />");
                tt.WriteLine("    </Assets>");
                tt.WriteLine("    <Prerequisites>");
                tt.WriteLine("        <Prerequisite Id=\"Microsoft.VisualStudio.Component.TextTemplating\" Version=\"[14.0,17.0)\" DisplayName=\"Text Template Transformation\" />");
                tt.WriteLine("        <Prerequisite Id=\"Microsoft.VisualStudio.Component.CoreEditor\" Version=\"[14.0,17.0)\" DisplayName=\"Visual Studio core editor\" />");
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
                    CreateZipFile(archive, "EF.Reverse.POCO.v3.ttinclude", Path.Combine(_root, "EntityFramework.Reverse.POCO.Generator\\EF.Reverse.POCO.v3.ttinclude"));
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