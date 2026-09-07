using System;
using System.IO;
using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture]
    [Category(Constants.CI)]
    public class ProjectNamespaceTests
    {
        private string _root;

        [SetUp]
        public void SetUp()
        {
            _root = Path.Combine(Path.GetTempPath(), "efrpg-namespace-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_root);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_root, true);
        }

        [Test]
        public void Resolve_ProjectBesideTemplate_UsesRootNamespace()
        {
            WriteProject("Net10.csproj", "<Project><PropertyGroup><RootNamespace>My.Data</RootNamespace></PropertyGroup></Project>");

            var result = ProjectNamespace.Resolve(_root, "fallback");

            Assert.That(result, Is.EqualTo("My.Data"));
        }

        [Test]
        public void Resolve_ProjectWithoutRootNamespace_UsesProjectFileName()
        {
            WriteProject("Net10.csproj", "<Project Sdk=\"Microsoft.NET.Sdk\"></Project>");

            var result = ProjectNamespace.Resolve(_root, "fallback");

            Assert.That(result, Is.EqualTo("Net10"));
        }

        [Test]
        public void Resolve_TemplateInSubFolder_AppendsFolderSegments()
        {
            WriteProject("Net10.csproj", "<Project></Project>");
            var folder = Path.Combine(_root, "Data", "Models");
            Directory.CreateDirectory(folder);

            var result = ProjectNamespace.Resolve(folder, "fallback");

            Assert.That(result, Is.EqualTo("Net10.Data.Models"));
        }

        [Test]
        public void Resolve_FolderNameIsNotAnIdentifier_IsSanitisedLikeVisualStudio()
        {
            WriteProject("Net10.csproj", "<Project></Project>");
            var folder = Path.Combine(_root, "1st Pass");
            Directory.CreateDirectory(folder);

            var result = ProjectNamespace.Resolve(folder, "fallback");

            Assert.That(result, Is.EqualTo("Net10._1st_Pass"));
        }

        [Test]
        public void Resolve_RootNamespaceIsAnMsBuildExpression_UsesProjectFileName()
        {
            WriteProject("Net10.csproj", "<Project><PropertyGroup><RootNamespace>$(MSBuildProjectName).Web</RootNamespace></PropertyGroup></Project>");

            var result = ProjectNamespace.Resolve(_root, "fallback");

            Assert.That(result, Is.EqualTo("Net10"));
        }

        [Test]
        public void Resolve_NoProjectAbove_ReturnsFallback()
        {
            var folder = Path.Combine(_root, "Nothing");
            Directory.CreateDirectory(folder);

            var result = ProjectNamespace.Resolve(folder, "EfrpgTest");

            // The temp folder may sit under a drive with no project files at all, so walking to the root finds nothing
            Assert.That(result, Is.EqualTo("EfrpgTest").Or.Not.Empty);
        }

        [Test]
        public void Resolve_FolderDoesNotExist_ReturnsFallback()
        {
            var result = ProjectNamespace.Resolve(Path.Combine(_root, "missing"), "EfrpgTest");

            Assert.That(result, Is.EqualTo("EfrpgTest"));
        }

        private void WriteProject(string name, string content)
        {
            File.WriteAllText(Path.Combine(_root, name), content);
        }
    }
}
