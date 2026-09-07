using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     The template dropdown has to offer every TemplateType the generator has, and the one it opens on has to
    ///     be the one the shipped Database.tt already names.
    /// </summary>
    [TestFixture]
    public class TemplateTargetTests
    {
        [Test]
        public void EveryTemplateTypeTheGeneratorSupportsIsOffered()
        {
            var offered = TemplateTarget.All.Select(t => t.Name).OrderBy(n => n);

            Assert.That(offered, Is.EqualTo(RepositoryFiles.EnumMembers("TemplateType").OrderBy(n => n)),
                "TemplateTarget.All no longer matches the TemplateType enum. Add the missing entry.");
        }

        [Test]
        public void TheDefaultIsTheNewestTemplateAsTheShippedTemplateAlreadySays()
        {
            Assert.That(TemplateTarget.Default.Name, Is.EqualTo("EfCore10"));
        }

        [Test]
        public void EveryTargetHasADisplayName()
        {
            Assert.That(TemplateTarget.All.Select(t => t.DisplayName).Distinct().Count(),
                Is.EqualTo(TemplateTarget.All.Count));
        }
    }
}
