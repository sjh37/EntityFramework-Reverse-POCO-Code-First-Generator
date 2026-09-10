using System;
using System.IO;
using System.Linq;
using Efrpg;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Produces the snippets the Settings.* wiki pages show, and checks the fixture schema still supports them.
    /// </summary>
    /// <remarks>
    ///     <c>Write_all_samples_to_disk</c> is the authoring tool: run it, then paste the files it writes into the
    ///     wiki pages. The other tests guard the harness itself, so a broken harness fails here rather than
    ///     quietly producing a wrong snippet that somebody pastes.
    /// </remarks>
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class DocSampleTests
    {
        private StaticStateSnapshot _snapshot;

        // Generating a sample rewrites most of the static Settings class. Without this, ForeignKeyTests and
        // friends pass on their own and fail in a full run, because they read whatever Settings was left as.
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

        [Test]
        public void Fixture_schema_generates_the_three_documented_tables()
        {
            var generated = DocSampleRunner.Generate(() => { });

            StringAssert.Contains("public class Category", generated);
            StringAssert.Contains("public class Product", generated);
            StringAssert.Contains("public class sales_Order", generated); // Schema prepended verbatim - see Settings.PrependSchemaName
        }

        [Test]
        public void Fixture_schema_has_the_shapes_the_pages_rely_on()
        {
            var generated = DocSampleRunner.Generate(() => { });

            Assert.Multiple(() =>
            {
                StringAssert.Contains("private set;", generated, "a computed column, for UsePrivateSetterForComputedColumns");
                StringAssert.Contains("ValueGeneratedOnAdd()", generated, "an identity primary key");
                StringAssert.Contains("HasMaxLength(100)", generated, "a string column with a length");
                StringAssert.Contains("HasForeignKey", generated, "a foreign key, for the navigation properties");
                StringAssert.Contains("sales", generated, "a table outside the default schema");
            });
        }

        [Test]
        public void Generating_twice_with_the_same_settings_gives_the_same_output()
        {
            // Settings is static. If anything leaks between runs the snippets become order-dependent, and a
            // reader gets a diff that has nothing to do with the setting the page is about.
            var first = DocSampleRunner.Generate(() => { });
            var second = DocSampleRunner.Generate(() => { });

            Assert.AreEqual(first, second);
        }

        [Test]
        public void A_setting_change_is_not_leaked_into_the_next_run()
        {
            var baseline = DocSampleRunner.Generate(() => { });
            DocSampleRunner.Generate(() => Settings.UsePrivateSetterForComputedColumns = false);
            var afterwards = DocSampleRunner.Generate(() => { });

            Assert.AreEqual(baseline, afterwards);
        }

        [Test]
        public void Every_catalogue_sample_produces_something()
        {
            foreach (var key in DocSampleCatalogue.Keys)
            {
                var sample = DocSampleCatalogue.Get(key);
                Assert.IsNotNull(sample, key);
                Assert.IsNotEmpty(sample.Trim(), key);
            }
        }

        [Test]
        public void Every_before_and_after_pair_actually_differs()
        {
            // A pair that generates identical output means the page is showing a difference that is not there.
            foreach (var pair in PairedKeys())
            {
                Assert.AreNotEqual(
                    DocSampleCatalogue.Get(pair.Item1),
                    DocSampleCatalogue.Get(pair.Item2),
                    "'{0}' and '{1}' generate identical output, so the page claims a difference that does not exist.",
                    pair.Item1, pair.Item2);
            }
        }

        /// <summary>
        ///     Writes every catalogue sample to a folder so it can be pasted into the wiki. Not a test of anything;
        ///     it is the authoring step, and it is a test only so it can be run with the rest.
        /// </summary>
        [Test]
        [Category("DocSampleAuthoring")]
        public void Write_all_samples_to_disk()
        {
            var outputFolder = Path.Combine(Path.GetTempPath(), "efrpg-doc-samples", "output");
            if (Directory.Exists(outputFolder))
                Directory.Delete(outputFolder, true); // Or a renamed sample lingers and gets pasted by mistake
            Directory.CreateDirectory(outputFolder);

            foreach (var key in DocSampleCatalogue.Keys)
            {
                var filename = key.Replace('/', '.') + ".txt";
                File.WriteAllText(Path.Combine(outputFolder, filename), DocSampleCatalogue.Get(key));
            }

            TestContext.Out.WriteLine("Samples written to " + outputFolder);
            foreach (var key in DocSampleCatalogue.Keys)
            {
                TestContext.Out.WriteLine();
                TestContext.Out.WriteLine("===== " + key + " =====");
                TestContext.Out.WriteLine(DocSampleCatalogue.Get(key));
            }
        }

        private static System.Collections.Generic.IEnumerable<Tuple<string, string>> PairedKeys()
        {
            // Samples sharing a prefix before the '/' are alternative values of the same setting, so no two of
            // them should be identical - a page showing two identical blocks is claiming a difference that is
            // not there.
            //
            // Samples ending '-diff' or '-removes' are excluded: they are already the difference between two
            // runs rather than one run's output, so they can legitimately equal another sample. The Omit sample
            // is exactly that case - what Omit removes IS the block ConnectionString generates.
            var groups = DocSampleCatalogue.Keys
                .Where(k => !k.EndsWith("-diff", StringComparison.Ordinal) && !k.EndsWith("-removes", StringComparison.Ordinal))
                .GroupBy(k => k.Substring(0, k.IndexOf('/')))
                .Where(g => g.Count() > 1);

            foreach (var group in groups)
            {
                var keys = group.ToList();
                for (var i = 0; i < keys.Count - 1; i++)
                    yield return Tuple.Create(keys[i], keys[i + 1]);
            }
        }
    }
}
