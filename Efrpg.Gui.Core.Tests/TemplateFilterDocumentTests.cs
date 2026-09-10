using System;
using System.Linq;
using Efrpg.Gui;
using NUnit.Framework;

namespace Efrpg.Gui.Tests
{
    /// <summary>
    ///     Mostly against the real shipped Database.tt, because that is the file the picker will be writing into on
    ///     every newcomer's machine.
    /// </summary>
    [TestFixture]
    public class TemplateFilterDocumentTests
    {
        private static TemplateFilterDocument Shipped()
        {
            return TemplateFilterDocument.Parse(RepositoryFiles.DatabaseTemplate());
        }

        private static string[] Lines(string text)
        {
            return text.Split('\n');
        }

        [Test]
        public void Parse_ReadsTheShippedDefaults()
        {
            var document = Shipped();

            Assert.That(document.RefusalReason, Is.Null);
            Assert.That(document.Flag(FilterFlag.IncludeViews), Is.True);
            Assert.That(document.Flag(FilterFlag.IncludeStoredProcedures), Is.True);
            Assert.That(document.Flag(FilterFlag.IncludeTableValuedFunctions), Is.False);
            Assert.That(document.Flag(FilterFlag.IncludeScalarValuedFunctions), Is.False);
            Assert.That(document.Flag(FilterFlag.IncludeSynonyms), Is.False);
        }

        /// <summary>The two live excludes in the shipped template, and none of the commented-out examples.</summary>
        [Test]
        public void Parse_FindsOnlyTheLiveFilterLines()
        {
            var document = Shipped();

            Assert.That(document.In(FilterList.Table).Select(f => f.Pattern), Is.EqualTo(new[] { "AspNet.*", "__EFMigrationsHistory" }));
            Assert.That(document.In(FilterList.Table).All(f => !f.IsInclude && f.CanEvaluate && !f.IsPickerOwned), Is.True);
            Assert.That(document.In(FilterList.Schema), Is.Empty);
            Assert.That(document.In(FilterList.StoredProcedure), Is.Empty);
        }

        [Test]
        public void Parse_ReadsLiveFlagsWhereATemplateSetsThem()
        {
            var text = RepositoryFiles.TemplateFixtures().First(p => p.EndsWith("Tester.Integration.EFCore10\\EfrpgTest.tt", StringComparison.Ordinal) ||
                                                                       p.EndsWith("Tester.Integration.EFCore10/EfrpgTest.tt", StringComparison.Ordinal));
            var document = TemplateFilterDocument.Parse(System.IO.File.ReadAllText(text));

            Assert.That(document.Flag(FilterFlag.IncludeTableValuedFunctions), Is.True);
            Assert.That(document.Flag(FilterFlag.IncludeScalarValuedFunctions), Is.True);
        }

        [Test]
        public void Parse_LeavesTheTextExactlyAsItWas()
        {
            foreach (var path in RepositoryFiles.TemplateFixtures())
            {
                var text = System.IO.File.ReadAllText(path);

                Assert.That(TemplateFilterDocument.Parse(text).Text, Is.EqualTo(text), path);
            }
        }

        [Test]
        public void WithFlag_UncommentsTheTemplatesOwnLineAndChangesOnlyThat()
        {
            var before = Shipped();

            var after = before.WithFlag(FilterFlag.IncludeViews, false);

            var changed = Lines(before.Text).Zip(Lines(after.Text), (a, b) => new { a, b }).Where(x => x.a != x.b).ToList();
            Assert.That(Lines(after.Text).Length, Is.EqualTo(Lines(before.Text).Length));
            Assert.That(changed.Count, Is.EqualTo(1));
            Assert.That(changed[0].a.TrimEnd(), Is.EqualTo("    //FilterSettings.IncludeViews                 = true;"));
            Assert.That(changed[0].b.TrimEnd(), Is.EqualTo("    FilterSettings.IncludeViews                 = false;"));
            Assert.That(after.Flag(FilterFlag.IncludeViews), Is.False);
        }

        [Test]
        public void WithFlag_RewritesALiveLineInPlace()
        {
            var document = TemplateFilterDocument.Parse(
                "<#\r\n    FilterSettings.AddDefaults();\r\n    FilterSettings.IncludeViews = true; // keep\r\n#>\r\n");

            var after = document.WithFlag(FilterFlag.IncludeViews, false);

            Assert.That(after.Text, Is.EqualTo("<#\r\n    FilterSettings.AddDefaults();\r\n    FilterSettings.IncludeViews = false; // keep\r\n#>\r\n"));
        }

        [Test]
        public void WithFlag_AddsALineAfterAddDefaultsWhenTheTemplateHasNone()
        {
            var document = TemplateFilterDocument.Parse("<#\n    FilterSettings.Reset();\n    FilterSettings.AddDefaults();\n#>\n");

            var after = document.WithFlag(FilterFlag.IncludeTableValuedFunctions, true);

            Assert.That(after.Text, Is.EqualTo("<#\n    FilterSettings.Reset();\n    FilterSettings.AddDefaults();\n    FilterSettings.IncludeTableValuedFunctions = true;\n#>\n"));
            Assert.That(after.Flag(FilterFlag.IncludeTableValuedFunctions), Is.True);
        }

        [Test]
        public void WithPickerPatterns_InsertsMarkedLinesAfterTheLastLineForThatList()
        {
            var before = Shipped();

            var after = before.WithPickerPatterns(FilterList.Table, new[] { "^(?:A|B)$", "^(?:C)$" });

            var lines    = Lines(after.Text).Select(l => l.TrimEnd()).ToList();
            var previous = lines.IndexOf("    FilterSettings.TableFilters.Add(new RegexExcludeFilter(\"__EFMigrationsHistory\")); // This excludes a table called '__EFMigrationsHistory'");
            Assert.That(previous, Is.GreaterThan(0));
            Assert.That(lines[previous + 1], Is.EqualTo("    FilterSettings.TableFilters.Add(new RegexIncludeFilter(@\"^(?:A|B)$\")); // " + TemplateFilterDocument.PickerMarker + ": right-click the .tt to change this"));
            Assert.That(lines[previous + 2], Does.StartWith("    FilterSettings.TableFilters.Add(new RegexIncludeFilter(@\"^(?:C)$\")); // " + TemplateFilterDocument.PickerMarker));
            Assert.That(Lines(after.Text).Length, Is.EqualTo(Lines(before.Text).Length + 2));

            var owned = after.In(FilterList.Table).Where(f => f.IsPickerOwned).ToList();
            Assert.That(owned.Select(f => f.Pattern), Is.EqualTo(new[] { "^(?:A|B)$", "^(?:C)$" }));
            Assert.That(owned.All(f => f.IsInclude && f.CanEvaluate), Is.True);
        }

        /// <summary>The stored procedure list has only commented-out examples in the shipped file; the line still lands beside them.</summary>
        [Test]
        public void WithPickerPatterns_UsesTheCommentedOutExamplesAsTheAnchor()
        {
            var after = Shipped().WithPickerPatterns(FilterList.StoredProcedure, new[] { "^(?:P)$" });

            var lines    = Lines(after.Text).Select(l => l.TrimEnd()).ToList();
            var previous = lines.FindIndex(l => l.StartsWith("    //FilterSettings.StoredProcedureFilters.Add(new RegexIncludeFilter(\"Pricing.*\"));", StringComparison.Ordinal));
            Assert.That(previous, Is.GreaterThan(0));
            Assert.That(lines[previous + 1], Does.StartWith("    FilterSettings.StoredProcedureFilters.Add(new RegexIncludeFilter(@\"^(?:P)$\"));"));
        }

        [Test]
        public void WithPickerPatterns_CanWriteExcludeLinesThatItStillOwns()
        {
            var after = Shipped().WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" }, false);

            var owned = after.In(FilterList.Table).Single(f => f.IsPickerOwned);
            Assert.That(owned.IsInclude, Is.False);
            Assert.That(after.Text, Does.Contain("Add(new RegexExcludeFilter(@\"^(?:A)$\")); // " + TemplateFilterDocument.PickerMarker));
            Assert.That(after.WithPickerPatterns(FilterList.Table, new string[0]).Text, Is.EqualTo(Shipped().Text));
        }

        [Test]
        public void WithPickerPatterns_RemovingEverythingRestoresTheOriginalBytes()
        {
            var original = Shipped();

            var roundTrip = original
                .WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" })
                .WithPickerPatterns(FilterList.StoredProcedure, new[] { "^(?:P)$" })
                .WithPickerPatterns(FilterList.Table, new string[0])
                .WithPickerPatterns(FilterList.StoredProcedure, new string[0]);

            Assert.That(roundTrip.Text, Is.EqualTo(original.Text));
        }

        [Test]
        public void WithPickerPatterns_ReplacesItsOwnLinesWhereTheyWere()
        {
            var first  = Shipped().WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$", "^(?:B)$" });
            var second = first.WithPickerPatterns(FilterList.Table, new[] { "^(?:Z)$" });

            var firstIndex  = Lines(first.Text).ToList().FindIndex(l => l.Contains("^(?:A)$"));
            var secondIndex = Lines(second.Text).ToList().FindIndex(l => l.Contains("^(?:Z)$"));

            Assert.That(secondIndex, Is.EqualTo(firstIndex));
            Assert.That(Lines(second.Text).Length, Is.EqualTo(Lines(first.Text).Length - 1));
            Assert.That(second.In(FilterList.Table).Count(f => f.IsPickerOwned), Is.EqualTo(1));
        }

        /// <summary>A user's own line is never touched, even when it sits between two of the picker's.</summary>
        [Test]
        public void WithPickerPatterns_LeavesTheUsersLinesAlone()
        {
            var original = Shipped();

            var after = original
                .WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" })
                .WithPickerPatterns(FilterList.Table, new string[0]);

            Assert.That(after.In(FilterList.Table).Select(f => f.Pattern), Is.EqualTo(new[] { "AspNet.*", "__EFMigrationsHistory" }));
            Assert.That(after.Text, Is.EqualTo(original.Text));
        }

        [Test]
        public void Patterns_SurviveAQuoteAndABackslash()
        {
            var pattern = "^(?:Say\\ \"Hi\")$";

            var after = Shipped().WithPickerPatterns(FilterList.Table, new[] { pattern });

            Assert.That(after.Text, Does.Contain("@\"^(?:Say\\ \"\"Hi\"\")$\""));
            Assert.That(after.In(FilterList.Table).Single(f => f.IsPickerOwned).Pattern, Is.EqualTo(pattern));
        }

        [Test]
        public void Parse_UnescapesARegularStringLiteral()
        {
            var document = TemplateFilterDocument.Parse(
                "<#\n    FilterSettings.AddDefaults();\n    FilterSettings.TableFilters.Add(new RegexExcludeFilter(\"^A\\\\.B\\\"$\"));\n#>\n");

            var filter = document.In(FilterList.Table).Single();

            Assert.That(filter.Pattern, Is.EqualTo("^A\\.B\"$"));
            Assert.That(filter.CanEvaluate, Is.True);
            Assert.That(filter.Matches("A.B\""), Is.True);
        }

        [Test]
        public void Parse_MarksAFilterItCannotReadAsUnevaluable()
        {
            var document = TemplateFilterDocument.Parse(
                "<#\n    FilterSettings.AddDefaults();\n" +
                "    FilterSettings.TableFilters.Add(new RegexIncludeFilter(new Regex(\"^x$\", RegexOptions.IgnoreCase)));\n" +
                "    FilterSettings.StoredProcedureFilters.Add(new StoredProcedureFilter());\n#>\n");

            Assert.That(document.In(FilterList.Table).Single().CanEvaluate, Is.False);
            Assert.That(document.In(FilterList.StoredProcedure).Single().CanEvaluate, Is.False);
            Assert.That(document.In(FilterList.Table).Single().Text, Does.Contain("RegexOptions.IgnoreCase"));
        }

        [Test]
        public void Parse_RefusesATemplateWithNoFilterBlock()
        {
            var document = TemplateFilterDocument.Parse("<#\n    Settings.ConnectionString = \"x\";\n#>\n");

            Assert.That(document.RefusalReason, Does.Contain("no FilterSettings block"));
            Assert.That(() => document.WithFlag(FilterFlag.IncludeViews, false), Throws.InvalidOperationException);
        }

        [Test]
        public void Edits_KeepTheFilesOwnLineEnding()
        {
            var lf   = "<#\n    FilterSettings.AddDefaults();\n#>\n";
            var crlf = lf.Replace("\n", "\r\n");

            var lfResult   = TemplateFilterDocument.Parse(lf).WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" }).Text;
            var crlfResult = TemplateFilterDocument.Parse(crlf).WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" }).Text;

            Assert.That(lfResult, Does.Not.Contain("\r"));
            Assert.That(crlfResult.Replace("\r\n", "\n"), Is.EqualTo(lfResult));
            Assert.That(crlfResult.Count(c => c == '\r'), Is.EqualTo(crlfResult.Count(c => c == '\n')));
        }

        /// <summary>A file with no final newline gains one where the insertion happens, and still ends without one.</summary>
        [Test]
        public void Edits_HandleAFileWithNoTrailingNewline()
        {
            var text = "<#\r\n    FilterSettings.AddDefaults();";

            var after = TemplateFilterDocument.Parse(text).WithPickerPatterns(FilterList.Table, new[] { "^(?:A)$" }).Text;

            Assert.That(after, Is.EqualTo("<#\r\n    FilterSettings.AddDefaults();\r\n    FilterSettings.TableFilters.Add(new RegexIncludeFilter(@\"^(?:A)$\")); // " + TemplateFilterDocument.PickerMarker + ": right-click the .tt to change this"));
        }
    }
}
