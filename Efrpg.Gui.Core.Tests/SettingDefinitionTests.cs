using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    [TestFixture]
    public class SettingDefinitionTests
    {
        private static SettingDefinition WithWikiPage(string wikiPage)
        {
            return new SettingDefinition("UseNamespace", "bool", SettingKind.Boolean, "Settings", "", "true", false, false, null, wikiPage);
        }

        [Test]
        public void WikiUrl_PageWithHeading_KeepsTheHeadingInTheAddressOnly()
        {
            var definition = WithWikiPage("Settings.Namespace#settingsusenamespace");

            Assert.That(definition.WikiPage, Is.EqualTo("Settings.Namespace"));
            Assert.That(definition.WikiUrl, Does.EndWith("/wiki/Settings.Namespace#settingsusenamespace"));
        }

        [Test]
        public void WikiUrl_PageAlone_HasNoFragment()
        {
            var definition = WithWikiPage("Settings.TableSuffix");

            Assert.That(definition.WikiPage, Is.EqualTo("Settings.TableSuffix"));
            Assert.That(definition.WikiUrl, Does.EndWith("/wiki/Settings.TableSuffix"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void WikiPage_Unknown_FallsBackToTheIndex(string wikiPage)
        {
            Assert.That(WithWikiPage(wikiPage).WikiPage, Is.EqualTo("Settings-Reference"));
        }
    }
}
