using Efrpg;
using Efrpg.Generators;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class FileScopedNamespaceTests
    {
        [SetUp]
        public void SetUp()
        {
            Settings.UseNamespace = true;
            Settings.Namespace    = "MyApp.Models";
        }

        [TearDown]
        public void TearDown()
        {
            Settings.UseFileScopedNamespaces = false;
            Settings.UseNamespace            = true;
            Settings.Namespace               = string.Empty;
        }

        // Constructor — Namespace property ----------------------------------------

        [Test]
        public void Namespace_WhenBlockScoped_ContainsOpeningBrace()
        {
            Settings.UseFileScopedNamespaces = false;

            var fhf = new FileHeaderFooter(string.Empty);

            StringAssert.Contains("namespace MyApp.Models", fhf.Namespace);
            StringAssert.Contains("{", fhf.Namespace);
        }

        [Test]
        public void Namespace_WhenFileScoped_EndsWithSemicolon()
        {
            Settings.UseFileScopedNamespaces = true;

            var fhf = new FileHeaderFooter(string.Empty);

            StringAssert.Contains("namespace MyApp.Models;", fhf.Namespace);
            StringAssert.DoesNotContain("{", fhf.Namespace);
        }

        [Test]
        public void Namespace_WhenFileScoped_WithSubNamespace_EndsWithSemicolon()
        {
            Settings.UseFileScopedNamespaces = true;

            var fhf = new FileHeaderFooter(".Entities");

            StringAssert.Contains("namespace MyApp.Models.Entities;", fhf.Namespace);
            StringAssert.DoesNotContain("{", fhf.Namespace);
        }

        // Constructor — Footer property -------------------------------------------

        [Test]
        public void Footer_WhenBlockScoped_ContainsClosingBrace()
        {
            Settings.UseFileScopedNamespaces = false;

            var fhf = new FileHeaderFooter(string.Empty);

            StringAssert.Contains("}", fhf.Footer);
        }

        [Test]
        public void Footer_WhenFileScoped_DoesNotContainClosingBrace()
        {
            Settings.UseFileScopedNamespaces = true;

            var fhf = new FileHeaderFooter(string.Empty);

            StringAssert.DoesNotContain("}", fhf.Footer);
        }

        // UseNamespace = false — file-scoped setting has no effect ----------------

        [Test]
        public void Namespace_WhenUseNamespaceFalse_IsEmpty_RegardlessOfFileScopedSetting()
        {
            Settings.UseNamespace            = false;
            Settings.UseFileScopedNamespaces = true;

            var fhf = new FileHeaderFooter(string.Empty);

            Assert.AreEqual(string.Empty, fhf.Namespace);
        }

        [Test]
        public void Footer_WhenUseNamespaceFalse_DoesNotContainClosingBrace_RegardlessOfFileScopedSetting()
        {
            Settings.UseNamespace            = false;
            Settings.UseFileScopedNamespaces = false;

            var fhf = new FileHeaderFooter(string.Empty);

            StringAssert.DoesNotContain("}", fhf.Footer);
        }

        // GetNamespaceBlock -------------------------------------------------------

        [Test]
        public void GetNamespaceBlock_WhenBlockScoped_ContainsOpeningBrace()
        {
            Settings.UseFileScopedNamespaces = false;

            var fhf   = new FileHeaderFooter(string.Empty);
            var block = fhf.GetNamespaceBlock("Entities");

            StringAssert.Contains("namespace MyApp.Models.Entities", block);
            StringAssert.Contains("{", block);
        }

        [Test]
        public void GetNamespaceBlock_WhenFileScoped_EndsWithSemicolon()
        {
            Settings.UseFileScopedNamespaces = true;

            var fhf   = new FileHeaderFooter(string.Empty);
            var block = fhf.GetNamespaceBlock("Entities");

            StringAssert.Contains("namespace MyApp.Models.Entities;", block);
            StringAssert.DoesNotContain("{", block);
        }

        [Test]
        public void GetNamespaceBlock_WhenUseNamespaceFalse_ReturnsEmpty()
        {
            Settings.UseNamespace            = false;
            Settings.UseFileScopedNamespaces = true;

            var fhf   = new FileHeaderFooter(string.Empty);
            var block = fhf.GetNamespaceBlock("Entities");

            Assert.AreEqual(string.Empty, block);
        }
    }
}
