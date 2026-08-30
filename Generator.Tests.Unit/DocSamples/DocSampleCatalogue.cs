using System;
using System.Collections.Generic;
using System.Linq;
using Efrpg;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Every code snippet that appears on a Settings.* wiki page, keyed by the name the page cites.
    /// </summary>
    /// <remarks>
    ///     A wiki page marks each generated block with <c>&lt;!-- docsample: Key --&gt;</c> immediately above the
    ///     fence. <see cref="WikiSnippetDriftTests"/> looks the key up here, regenerates it, and fails if the page
    ///     has fallen behind. So a snippet on the wiki is never hand-maintained: change the templates, run the
    ///     tests, paste the new output.
    ///     Adding a sample: add an entry here, run <c>DocSampleTests.Write_all_samples_to_disk</c>, and paste the
    ///     file it writes into the page under the matching marker.
    /// </remarks>
    public static class DocSampleCatalogue
    {
        public static IEnumerable<string> Keys
        {
            get { return Samples().Keys.OrderBy(x => x, StringComparer.Ordinal); }
        }

        public static string Get(string key)
        {
            Func<string> sample;
            if (!Samples().TryGetValue(key, out sample))
                throw new KeyNotFoundException(string.Format(
                    "No doc sample named '{0}'. Either the wiki cites a sample that was renamed or deleted, or a new marker was added to a page without adding the sample here.", key));

            return sample();
        }

        private static Dictionary<string, Func<string>> Samples()
        {
            return new Dictionary<string, Func<string>>(StringComparer.Ordinal)
            {
                // ---- Settings.UsePrivateSetterForComputedColumns -------------------------------------------
                {
                    "UsePrivateSetterForComputedColumns/true", () => Product(
                        () => Settings.UsePrivateSetterForComputedColumns = true)
                },
                {
                    "UsePrivateSetterForComputedColumns/false", () => Product(
                        () => Settings.UsePrivateSetterForComputedColumns = false)
                },

                // ---- Settings.ElementsToGenerate ------------------------------------------------------------
                {
                    "ElementsToGenerate/all", () => Outline(Default())
                },
                {
                    "ElementsToGenerate/poco-only", () => Outline(
                        DocSampleRunner.Generate(() => Settings.ElementsToGenerate = Elements.Poco))
                },
                {
                    "ElementsToGenerate/context-and-interface", () => Outline(
                        DocSampleRunner.Generate(() => Settings.ElementsToGenerate = Elements.Context | Elements.Interface))
                },
                {
                    "ElementsToGenerate/poco-and-configuration", () => Outline(
                        DocSampleRunner.Generate(() => Settings.ElementsToGenerate = Elements.Poco | Elements.PocoConfiguration))
                },

                // ---- Settings.UpdateColumn -----------------------------------------------------------------
                {
                    "UpdateColumn/before", () => Product(() => { })
                },
                {
                    "UpdateColumn/after", () => Product(() =>
                        Settings.UpdateColumn = (column, table, enumDefinitions, jsonColumnMappings) =>
                        {
                            // Rename a primary key called <Table>Id to just Id
                            if (column.IsPrimaryKey && column.NameHumanCase == table.NameHumanCase + "Id")
                                column.NameHumanCase = "Id";

                            // Keep an internal column out of the model entirely
                            if (column.NameHumanCase == "Notes")
                                column.Hidden = true;

                            Settings.ApplyDataAnnotations(column);
                        })
                },

                // ---- Settings.PrependSchemaNameForTable ----------------------------------------------------
                {
                    "PrependSchemaNameForTable/default", () => Outline(Default())
                },
                {
                    "PrependSchemaNameForTable/sales-excluded", () => Outline(
                        DocSampleRunner.Generate(() => Settings.PrependSchemaNameForTable = table =>
                            !table.Schema.DbName.Equals("sales", StringComparison.OrdinalIgnoreCase)))
                },

                // ---- Settings.OnConfiguration --------------------------------------------------------------
                {
                    "OnConfiguration/ConnectionString", () => DocSampleExtractor.Section(
                        Default(), "protected override void OnConfiguring")
                },
                {
                    // What switching to Configuration adds and changes, rather than the whole DbContext
                    "OnConfiguration/Configuration-diff", () => DocSampleExtractor.ChangedRegion(
                        DocSampleRunner.Generate(() => Settings.OnConfiguration = OnConfiguration.Configuration),
                        Default(),
                        context: 1)
                },
                {
                    // What switching to Omit takes away
                    "OnConfiguration/Omit-removes", () => DocSampleExtractor.ChangedRegion(
                        Default(),
                        DocSampleRunner.Generate(() => Settings.OnConfiguration = OnConfiguration.Omit),
                        context: 0)
                },
            };
        }

        /// <summary>
        ///     Generation with nothing changed from the shipped Database.tt defaults.
        /// </summary>
        private static string Default()
        {
            return DocSampleRunner.Generate(() => { });
        }

        /// <summary>
        ///     The generated Product entity class, which is where most column-level settings show their effect.
        /// </summary>
        private static string Product(Action configure)
        {
            return DocSampleExtractor.Section(DocSampleRunner.Generate(configure), "public class Product");
        }

        /// <summary>
        ///     Reduces a generated file to its type declarations, for samples about which types get generated
        ///     rather than what is inside them.
        /// </summary>
        internal static string Outline(string generated)
        {
            var declarations = generated
                .Replace("\r\n", "\n")
                .Split('\n')
                .Select(line => line.Trim())
                .Where(IsTypeDeclaration)
                .ToList();

            return declarations.Count == 0
                ? "// Nothing is generated."
                : string.Join("\n", declarations);
        }

        private static bool IsTypeDeclaration(string line)
        {
            if (line.StartsWith("//") || line.Contains("("))
                return false;

            return (line.Contains(" class ") || line.Contains(" interface ") || line.Contains(" enum "))
                && (line.StartsWith("public") || line.StartsWith("internal"));
        }
    }
}
