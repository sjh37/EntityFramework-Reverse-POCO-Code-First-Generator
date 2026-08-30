using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Fails when a generated code block on the wiki no longer matches what the generator produces.
    /// </summary>
    /// <remarks>
    ///     The August 2026 wiki audit found a documented setting that had never existed, a helper method that had
    ///     never existed, three wrong defaults and an example that did not compile. Every one of them was prose
    ///     written by reading the source, which then drifted. This test is the answer: a snippet on the wiki is
    ///     regenerated and compared, so it cannot drift silently.
    ///     A page opts in by putting <c>&lt;!-- docsample: Key --&gt;</c> immediately above a fenced block. Blocks
    ///     without a marker are hand-written examples and are not checked here.
    ///     The wiki is a separate repository, so this test skips when it is not checked out beside this one. That
    ///     makes it useless on a machine that has only one repo - which is the trade for not requiring both.
    /// </remarks>
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class WikiSnippetDriftTests
    {
        private StaticStateSnapshot _snapshot;

        // Regenerating a snippet rewrites the static Settings class - see DocSampleTests.
        [OneTimeSetUp]
        public void CaptureStaticState()
        {
            _snapshot = StaticStateSnapshot.Capture();
            DocSampleRunner.Pristine = _snapshot;
        }

        [OneTimeTearDown]
        public void RestoreStaticState()
        {
            _snapshot.Restore();
        }

        private const string MarkerPattern = @"<!--\s*docsample:\s*(?<key>[^\s>]+)\s*-->\s*\r?\n```[a-z]*\r?\n(?<body>.*?)\r?\n```";

        [Test]
        public void Every_marked_wiki_snippet_matches_freshly_generated_output()
        {
            var wiki = WikiFolder();
            if (wiki == null)
                Assert.Ignore("The wiki repository is not checked out beside this one, so there is nothing to check.");

            var failures = new List<string>();
            var checkedCount = 0;

            foreach (var page in Directory.GetFiles(wiki, "*.md"))
            {
                var text = File.ReadAllText(page);
                foreach (Match match in Regex.Matches(text, MarkerPattern, RegexOptions.Singleline))
                {
                    var key = match.Groups["key"].Value;
                    var onPage = Normalise(match.Groups["body"].Value);
                    checkedCount++;

                    string generated;
                    try
                    {
                        generated = Normalise(DocSampleCatalogue.Get(key));
                    }
                    catch (KeyNotFoundException ex)
                    {
                        failures.Add(string.Format("{0}: {1}", Path.GetFileName(page), ex.Message));
                        continue;
                    }

                    if (onPage != generated)
                        failures.Add(string.Format(
                            "{0} is stale for '{1}'.\n--- on the wiki ---\n{2}\n--- generated now ---\n{3}",
                            Path.GetFileName(page), key, onPage, generated));
                }
            }

            if (failures.Any())
                Assert.Fail(string.Join("\n\n", failures) +
                            "\n\nRun DocSampleTests.Write_all_samples_to_disk and paste the new output into the page.");

            Assert.Greater(checkedCount, 0, "No docsample markers found on the wiki. Either none have been added yet, or the marker format changed.");
        }

        [Test]
        public void Every_catalogue_sample_is_cited_by_a_wiki_page()
        {
            // The other direction: a sample nobody shows is a sample nobody maintains.
            var wiki = WikiFolder();
            if (wiki == null)
                Assert.Ignore("The wiki repository is not checked out beside this one, so there is nothing to check.");

            var cited = new HashSet<string>(StringComparer.Ordinal);
            foreach (var page in Directory.GetFiles(wiki, "*.md"))
                foreach (Match match in Regex.Matches(File.ReadAllText(page), MarkerPattern, RegexOptions.Singleline))
                    cited.Add(match.Groups["key"].Value);

            var orphans = DocSampleCatalogue.Keys.Where(k => !cited.Contains(k)).ToList();

            Assert.IsEmpty(orphans, "These samples are generated but no wiki page shows them: " + string.Join(", ", orphans));
        }

        /// <summary>
        ///     Locates the wiki checkout, which convention puts beside this repository.
        /// </summary>
        private static string WikiFolder()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "EF.Reverse.POCO.GeneratorV4.sln")))
                dir = dir.Parent;

            if (dir == null || dir.Parent == null)
                return null;

            var wiki = Path.Combine(dir.Parent.FullName, dir.Name + ".wiki");
            return Directory.Exists(wiki) ? wiki : null;
        }

        private static string Normalise(string text)
        {
            return string.Join("\n", text
                .Replace("\r\n", "\n")
                .Split('\n')
                .Select(line => line.TrimEnd()))
                .Trim('\n');
        }
    }
}
