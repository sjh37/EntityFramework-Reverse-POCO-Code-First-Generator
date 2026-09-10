using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Every row in the settings editor carries a link to the wiki. These check that each link lands somewhere:
    ///     the page exists, and where the link names a heading on a shared page, the heading exists too. They need
    ///     the wiki checked out beside this repository and are skipped otherwise.
    /// </summary>
    [TestFixture]
    public class WikiLinkTests
    {
        private static SettingsCatalogue V4 => SettingsCatalogue.Load(RepositoryFiles.SettingsMetadata("v4"));

        private static IReadOnlyList<SettingEditorItem> Listed()
        {
            return SettingsEditSession.Load(RepositoryFiles.DatabaseTemplate(), V4).Items;
        }

        private static string Wiki()
        {
            var wiki = RepositoryFiles.WikiFolder();
            if (wiki == null)
                Assert.Ignore("The wiki is not checked out beside this repository.");

            return wiki;
        }

        /// <summary>GitHub's anchor for a markdown heading: lower case, punctuation dropped, spaces to hyphens.</summary>
        private static string AnchorOf(string heading)
        {
            return Regex.Replace(heading.Trim().ToLowerInvariant(), @"[^\w\- ]", "").Replace(' ', '-');
        }

        private static ISet<string> HeadingAnchors(string path)
        {
            return new HashSet<string>(File.ReadLines(path)
                .Select(line => Regex.Match(line, @"^#{1,6}\s+(.*?)\s*$"))
                .Where(match => match.Success)
                .Select(match => AnchorOf(match.Groups[1].Value)), StringComparer.Ordinal);
        }

        [Test]
        public void EveryListedSetting_LinksToASettingsPage_NotTheIndex()
        {
            var onTheIndex = Listed().Where(i => i.Definition.WikiPage == "Settings-Reference").Select(i => i.Name).ToList();

            Assert.That(onTheIndex, Is.Empty, "settings with no page of their own");
        }

        [Test]
        public void EveryListedSetting_WikiPage_ExistsInTheWiki()
        {
            var wiki    = Wiki();
            var missing = Listed()
                .Select(i => i.Definition.WikiPage)
                .Distinct()
                .Where(page => !File.Exists(Path.Combine(wiki, page + ".md")))
                .ToList();

            Assert.That(missing, Is.Empty);
        }

        [Test]
        public void EveryListedSetting_WikiHeading_ExistsOnItsPage()
        {
            var wiki    = Wiki();
            var missing = new List<string>();

            foreach (var item in Listed())
            {
                var fragment = new Uri(item.Definition.WikiUrl).Fragment.TrimStart('#');
                if (fragment.Length == 0)
                    continue;

                var path = Path.Combine(wiki, item.Definition.WikiPage + ".md");
                if (!File.Exists(path) || !HeadingAnchors(path).Contains(fragment))
                    missing.Add(item.Name + " -> " + item.Definition.WikiPage + "#" + fragment);
            }

            Assert.That(missing, Is.Empty, "links whose heading is not on the page");
        }
    }
}
