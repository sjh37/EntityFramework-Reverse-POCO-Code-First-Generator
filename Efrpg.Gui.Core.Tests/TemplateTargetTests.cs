using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The template dropdown has to offer every TemplateType, and each entry has to name the GeneratorType that
    ///     goes with it - the generator keeps those two settings independent, so getting the pairing wrong produces
    ///     generated code that does not compile.
    /// </summary>
    [TestFixture]
    public class TemplateTargetTests
    {
        [Test]
        public void EveryTemplateTypeTheGeneratorSupportsIsOffered()
        {
            var offered = TemplateTarget.All.Select(t => t.Name).OrderBy(n => n);

            Assert.That(offered, Is.EqualTo(RepositoryFiles.EnumMembers("TemplateType").OrderBy(n => n)),
                "TemplateTarget.All no longer matches the TemplateType enum. Add the missing entry, and give it "
                + "the GeneratorType it has to be paired with.");
        }

        [Test]
        public void TheDefaultIsTheNewestTemplateAsTheShippedTemplateAlreadySays()
        {
            Assert.That(TemplateTarget.Default.Name, Is.EqualTo("EfCore10"));
            Assert.That(TemplateTarget.Default.GeneratorTypeName, Is.EqualTo("EfCore"));
        }

        /// <summary>A generator type name that is not an enum member would be written into the .tt and not compile.</summary>
        [Test]
        public void EveryGeneratorTypeNamedIsARealGeneratorType()
        {
            var known = RepositoryFiles.EnumMembers("GeneratorType");

            Assert.That(TemplateTarget.All.Select(t => t.GeneratorTypeName).Distinct(), Is.SubsetOf(known));
        }

        /// <summary>
        ///     The pairing itself. An EF6 template driven by the EF Core generator, or the reverse, is the failure
        ///     this class exists to prevent.
        /// </summary>
        [TestCase("Ef6",             "Ef6")]
        [TestCase("FileBasedEf6",    "Ef6")]
        [TestCase("EfCore8",         "EfCore")]
        [TestCase("EfCore9",         "EfCore")]
        [TestCase("EfCore10",        "EfCore")]
        [TestCase("FileBasedCore8",  "EfCore")]
        [TestCase("FileBasedCore9",  "EfCore")]
        [TestCase("FileBasedCore10", "EfCore")]
        public void TheGeneratorTypeMatchesTheTemplate(string templateType, string expectedGeneratorType)
        {
            Assert.That(TemplateTarget.Find(templateType)!.GeneratorTypeName, Is.EqualTo(expectedGeneratorType));
        }

        /// <summary>
        ///     Only the file based templates need Settings.TemplateFolder, and the dialog shows a note for exactly
        ///     those - so the flag has to follow the name.
        /// </summary>
        [Test]
        public void OnlyTheFileBasedTemplatesNeedATemplateFolder()
        {
            foreach (var target in TemplateTarget.All)
                Assert.That(target.RequiresTemplateFolder, Is.EqualTo(target.Name.StartsWith("FileBased")),
                    target.Name + " has the wrong RequiresTemplateFolder flag.");
        }

        [Test]
        public void EveryTargetHasADisplayName()
        {
            Assert.That(TemplateTarget.All.Select(t => t.DisplayName).Distinct().Count(),
                Is.EqualTo(TemplateTarget.All.Count));
        }
    }
}
