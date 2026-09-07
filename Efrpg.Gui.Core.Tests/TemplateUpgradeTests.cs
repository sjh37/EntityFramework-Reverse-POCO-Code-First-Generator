using System;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Run against a real v3.14.1 Database.tt recovered from the commit before database reading moved into the
    ///     efrpg tool, rather than a reconstruction of one.
    /// </summary>
    [TestFixture]
    public class TemplateUpgradeTests
    {
        private static TemplateUpgradeResult Upgraded()
        {
            return TemplateUpgrade.Upgrade(RepositoryFiles.V3Template());
        }

        [Test]
        public void IsV3_RecognisesTheRealV3TemplateAndNotTheV4One()
        {
            Assert.That(TemplateUpgrade.IsV3(RepositoryFiles.V3Template()), Is.True);
            Assert.That(TemplateUpgrade.IsV3(RepositoryFiles.DatabaseTemplate()), Is.False);
        }

        [Test]
        public void TheRealV3TemplateUpgrades()
        {
            var result = Upgraded();

            Assert.That(result.Blockers, Is.Empty);
            Assert.That(result.Succeeded, Is.True);
        }

        /// <summary>
        ///     v4 removed multi-context generation, so the eight settings behind it are compile errors in a v4
        ///     template. A stock v3 file assigns all eight, four of them as multi-line delegates; every one goes,
        ///     whole, and the prose above them stays.
        /// </summary>
        [Test]
        public void TheMultiContextSettingsAreDeletedWholeIncludingTheDelegates()
        {
            var result = Upgraded();
            var code   = result.Text.Replace("\r\n", "\n").Split('\n')
                .Where(l => !l.TrimStart().StartsWith("//", StringComparison.Ordinal))
                .ToList();

            Assert.That(code, Has.None.Contains("Settings.GenerateSingleDbContext"));
            Assert.That(code, Has.None.Contains("Settings.MultiContext"));
            Assert.That(code, Has.None.Contains("allFields"), "a delegate body was left behind");
            Assert.That(result.Changes.Count(c => c.Description.Contains("multi-context generation was removed")), Is.EqualTo(8));
            Assert.That(result.Text, Does.Contain("// If GenerateSingleDbContext = true"), "prose is not rewritten");
        }

        [Test]
        public void ATemplateThatGeneratesMultipleContextsIsRefusedAndPointedAtV3()
        {
            var template = RepositoryFiles.V3Template().Replace(
                "Settings.GenerateSingleDbContext              = true;",
                "Settings.GenerateSingleDbContext              = false;");

            var result = TemplateUpgrade.Upgrade(template);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Blockers.Single(), Does.Contain("multiple DbContexts").And.Contain("v3"));
        }

        [Test]
        public void TheTemplateFolderSettingIsDeleted()
        {
            // The stock v3 prose still mentions it, in a comment line and in the trailing comment on the
            // TemplateType line; only code counts.
            var code = Upgraded().Text.Replace("\r\n", "\n").Split('\n')
                .Select(l => l.IndexOf("//", StringComparison.Ordinal) is var i && i >= 0 ? l.Substring(0, i) : l)
                .ToList();

            Assert.That(code, Has.None.Contains("Settings.TemplateFolder"));
        }

        [Test]
        public void TheGeneratorTypeSettingIsDeleted()
        {
            var code = Upgraded().Text.Replace("\r\n", "\n").Split('\n')
                .Select(l => l.IndexOf("//", StringComparison.Ordinal) is var i && i >= 0 ? l.Substring(0, i) : l)
                .ToList();

            Assert.That(code, Has.None.Contains("Settings.GeneratorType"));
        }

        [TestCase("Settings.TemplateType                 = TemplateType.EfCore10;", "Settings.TemplateType                 = TemplateType.FileBasedCore10;", "file-based")]
        [TestCase("Settings.GeneratorType                = GeneratorType.EfCore;", "Settings.GeneratorType                = GeneratorType.Custom;", "GeneratorType.Custom")]
        public void ATemplateUsingARemovedTemplateOrGeneratorTypeIsRefused(string stock, string replaced, string reason)
        {
            var template = RepositoryFiles.V3Template().Replace(stock, replaced);
            Assert.That(template, Is.Not.EqualTo(RepositoryFiles.V3Template()), "the fixture line must exist");

            var result = TemplateUpgrade.Upgrade(template);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Blockers.Single(), Does.Contain(reason).And.Contain("v3"));
        }

        /// <summary>A removed setting used somewhere the upgrade does not rewrite is a refusal, not a compile error later.</summary>
        [Test]
        public void AMultiContextSettingUsedInCodeElsewhereIsRefused()
        {
            var template = RepositoryFiles.V3Template().Replace(
                "    Settings.GenerateSingleDbContext              = true;",
                "    Settings.GenerateSingleDbContext              = true;\r\n    if (Settings.MultiContextAttributeDelimiter == '~') { }");

            var result = TemplateUpgrade.Upgrade(template);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Blockers.Single(), Does.Contain("Settings.MultiContext"));
        }

        /// <summary>The six edits, each of which the template does not compile or does not run without.</summary>
        [Test]
        public void AllSixRequiredEditsAreMade()
        {
            var text = Upgraded().Text;

            Assert.Multiple(() =>
            {
                Assert.That(text, Does.StartWith("<#@ include file=\"" + TemplateUpgrade.V4Include + "\" #>"),
                    "the include directive");
                Assert.That(text, Does.Not.Contain("Settings.FileManagerType"), "FileManagerType is gone");
                Assert.That(text, Does.Not.Contain("Settings.DatabaseReaderPlugin"), "DatabaseReaderPlugin is gone");
                Assert.That(text, Does.Contain("if (Settings.GenerateSeparateFiles)"), "the sub-folder condition");
                Assert.That(text, Does.Contain("NamingHelper.CleanUp(fkName)"), "CleanUp moved to NamingHelper");
                Assert.That(text, Does.Contain("EfrpgToolRunner.ReadDatabase("), "the entry point");
            });
        }

        [Test]
        public void EveryEditIsReportedSoTheUserCanSeeItBeforeItIsWritten()
        {
            var changes = Upgraded().Changes;

            // The six v3-to-v4 edits, the eight multi-context settings v4 removed, Settings.TemplateFolder and
            // Settings.GeneratorType.
            Assert.That(changes.Count, Is.EqualTo(16));
            Assert.That(changes.Select(c => c.Description), Has.All.Not.Empty);
        }

        /// <summary>
        ///     The twelve cosmetic differences between a stock v3 and a stock v4 file are deliberately left alone.
        ///     A customer will have edited some of them, and rewriting their comments is the over-reach the
        ///     refusal rule exists to prevent.
        /// </summary>
        [Test]
        public void TheCosmeticDifferencesAreLeftAlone()
        {
            var text = Upgraded().Text;

            Assert.Multiple(() =>
            {
                Assert.That(text, Does.Contain("// v3.14.1"), "the version header is the user's, not ours");
                Assert.That(text, Does.Contain("SqlCe"), "trailing comments are left as written");
                Assert.That(text.Split(new[] { TemplateUpgrade.V3Include }, StringSplitOptions.None).Length - 1,
                    Is.EqualTo(2), "the two prose mentions of the v3 include stay");
            });
        }

        /// <summary>
        ///     Only complete lines are removed, so the settings above and below keep their alignment and the file
        ///     shrinks by exactly the deleted statements: the two one-line settings, and the eight multi-context
        ///     statements however many lines each spans in the fixture.
        /// </summary>
        [Test]
        public void OnlyTheDeletedSettingsChangeTheLineCountBeforeTheEntryPoint()
        {
            var before = RepositoryFiles.V3Template();
            var after  = Upgraded().Text;

            var beforeHead = before.Substring(0, before.IndexOf("var outer =", StringComparison.Ordinal));
            var afterHead  = after.Substring(0, after.IndexOf("var outer =", StringComparison.Ordinal));

            var multiContextLines = TemplateSettingsDocument.Parse(before).Assignments
                .Where(a => !a.IsCommentedOut && (a.Name == "GenerateSingleDbContext" || a.Name.StartsWith("MultiContext", StringComparison.Ordinal)))
                .Sum(a => a.EndLineNumber - a.LineNumber + 1);

            Assert.That(multiContextLines, Is.GreaterThan(8), "the four delegates span several lines each");
            // FileManagerType, DatabaseReaderPlugin, TemplateFolder and GeneratorType are one line each
            Assert.That(Lines(afterHead), Is.EqualTo(Lines(beforeHead) - 4 - multiContextLines));
        }

        [Test]
        public void TheLineEndingsAreNotChanged()
        {
            var text = Upgraded().Text;

            Assert.That(text.Replace("\r\n", string.Empty), Does.Not.Contain("\n"),
                "a CRLF template must stay CRLF, or the next commit shows every line as changed.");
        }

        [Test]
        public void AnLfTemplateStaysLf()
        {
            var text = TemplateUpgrade.Upgrade(RepositoryFiles.V3Template().Replace("\r\n", "\n")).Text;

            Assert.That(text, Does.Not.Contain("\r"));
        }

        /// <summary>
        ///     The block this replaces wholesale is the one most likely to vary between customer files, and it must
        ///     be exactly what the current Database.tt ships or the upgrade emits last year's code.
        /// </summary>
        [Test]
        public void TheReplacementEntryPointIsWhatTheShippedTemplateActuallyCarries()
        {
            var shipped = RepositoryFiles.DatabaseTemplate();
            var tail    = shipped.Substring(shipped.IndexOf("    var outer = (GeneratedTextTransformation)", StringComparison.Ordinal));

            Assert.That(tail.TrimEnd('\r', '\n'), Is.EqualTo(TemplateUpgrade.V4EntryPoint),
                "BuildTT's footer has changed. Update TemplateUpgrade.V4EntryPoint to match.");
        }

        /// <summary>
        ///     Migrating 24 in-repo templates by script took two passes because some carried an extra commented-out
        ///     line inside this block. Those must still upgrade.
        /// </summary>
        [Test]
        public void AnExtraCommentInsideTheEntryPointDoesNotStopTheUpgrade()
        {
            var text = RepositoryFiles.V3Template()
                .Replace("    var fileManagement = new FileManagementService(outer);",
                         "    // a note the user left themselves\r\n    var fileManagement = new FileManagementService(outer);");

            Assert.That(TemplateUpgrade.Upgrade(text).Succeeded, Is.True);
        }

        /// <summary>
        ///     A half-applied migration leaves a template that neither compiles nor matches the guide, which is
        ///     worse than not offering the button.
        /// </summary>
        [Test]
        public void ARestructuredEntryPointIsRefused()
        {
            var text = RepositoryFiles.V3Template()
                .Replace("        generator.GenerateCode();", "        generator.GenerateCode();\r\n        MyOwnPostProcessing();");

            var result = TemplateUpgrade.Upgrade(text);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Text, Is.Null);
            Assert.That(result.Blockers.Single(), Does.Contain("entry point"));
        }

        [Test]
        public void AMissingEntryPointIsRefused()
        {
            var before = RepositoryFiles.V3Template();
            var text   = before.Substring(0, before.IndexOf("    var outer =", StringComparison.Ordinal));

            Assert.That(TemplateUpgrade.Upgrade(text).Succeeded, Is.False);
        }

        /// <summary>
        ///     Anything still naming a v4-removed type will not compile, so leaving it for the user to find at
        ///     generation time is not an option.
        /// </summary>
        [Test]
        public void ALeftoverReferenceToARemovedTypeIsRefused()
        {
            var text = RepositoryFiles.V3Template()
                .Replace("    FilterSettings.Reset();",
                         "    if (Settings.FileManagerType == FileManagerType.Null) { }\r\n    FilterSettings.Reset();");

            var result = TemplateUpgrade.Upgrade(text);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Blockers.Single(), Does.Contain("FileManagerType"));
        }

        [Test]
        public void AV4TemplateIsRefusedRatherThanUpgradedAgain()
        {
            var result = TemplateUpgrade.Upgrade(RepositoryFiles.DatabaseTemplate());

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Blockers.Single(), Does.Contain("nothing to upgrade"));
        }

        /// <summary>
        ///     Somebody who has already deleted one of the two settings by hand has done half the upgrade, which is
        ///     not a reason to refuse the other half.
        /// </summary>
        [Test]
        public void ASettingAlreadyRemovedByHandIsNotAProblem()
        {
            var lines = RepositoryFiles.V3Template()
                .Split(new[] { "\r\n" }, StringSplitOptions.None)
                .Where(l => !l.TrimStart().StartsWith("Settings.DatabaseReaderPlugin", StringComparison.Ordinal));

            var result = TemplateUpgrade.Upgrade(string.Join("\r\n", lines));

            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.Changes.Count, Is.EqualTo(15));
        }

        private static int Lines(string text)
        {
            return text.Split('\n').Length;
        }
    }
}
