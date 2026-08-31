using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Efrpg.Gui
{
    /// <summary>
    ///     Upgrades a v3 <c>Database.tt</c> to v4, or refuses and says why.
    /// </summary>
    /// <remarks>
    ///     Six edits are required, because without them the template does not compile or does not run. Twelve other
    ///     blocks differ between a stock v3 and a stock v4 file and are **deliberately not touched**: the version
    ///     header, two mentions of the v3 include inside comment prose, and the trailing-comment improvements made
    ///     in v4. A customer's file will already differ there, and rewriting comments they may have edited
    ///     themselves is exactly the over-reach the refusal rule exists to prevent.
    ///
    ///     **Refusing matters more than upgrading.** When 24 in-repo templates were migrated by script it took two
    ///     passes, because some carried an extra commented-out line inside the block the first pattern expected -
    ///     and those were files under one person's control. Customer files vary more. A half-applied migration
    ///     leaves a template that neither compiles nor matches the upgrade guide, which is worse than not offering
    ///     the button at all.
    /// </remarks>
    public sealed class TemplateUpgrade
    {
        public const string V3Include = "EF.Reverse.POCO.v3.ttinclude";
        public const string V4Include = "EF.Reverse.POCO.v4.ttinclude";

        /// <summary>
        ///     The entry point block v4 requires, which replaces the v3 one wholesale rather than being patched:
        ///     <c>fileManagement</c> moves above the try block, so the whole span has to go.
        /// </summary>
        /// <remarks>
        ///     <c>TemplateUpgradeTests</c> asserts this is byte for byte what the shipped Database.tt carries, so a
        ///     change to BuildTT's footer fails the build rather than leaving the upgrade emitting last year's code.
        /// </remarks>
        public const string V4EntryPoint =
            "    var outer = (GeneratedTextTransformation) this;\r\n" +
            "    var fileManagement = new FileManagementService(outer);\r\n" +
            "\r\n" +
            "    EfrpgResult toolResult = null;\r\n" +
            "    var efrpgToolOk = true;\r\n" +
            "    try\r\n" +
            "    {\r\n" +
            "        // Connection strings are passed to the tool over stdin, never on the command line, so they stay out of\r\n" +
            "        // process listings and command-line audit logs. See SecretsXml and EfrpgToolRunner.\r\n" +
            "        var efrpgMultiContext = !Settings.GenerateSingleDbContext && string.IsNullOrWhiteSpace(Settings.MultiContextSettingsPlugin);\r\n" +
            "        toolResult = EfrpgToolRunner.ReadDatabase(\r\n" +
            "            FilterSettings.IncludeStoredProcedures || FilterSettings.IncludeTableValuedFunctions || FilterSettings.IncludeScalarValuedFunctions,\r\n" +
            "            FilterSettings.IncludeSynonyms,\r\n" +
            "            efrpgMultiContext);\r\n" +
            "    }\r\n" +
            "    catch (Exception efrpgEx)\r\n" +
            "    {\r\n" +
            "        fileManagement.Error(\"// -----------------------------------------------------------------------------------------\");\r\n" +
            "        if (efrpgEx is System.ComponentModel.Win32Exception)\r\n" +
            "            fileManagement.Error(\"// efrpg tool not found. Install it with: dotnet tool install -g Efrpg\");\r\n" +
            "        else\r\n" +
            "            fileManagement.Error(\"// efrpg tool reported an error:\");\r\n" +
            "        fileManagement.Error(\"// \" + efrpgEx.Message.Replace(\"\\r\\n\", \" \").Replace(\"\\n\", \" \"));\r\n" +
            "        fileManagement.Error(\"// -----------------------------------------------------------------------------------------\");\r\n" +
            "        efrpgToolOk = false;\r\n" +
            "    }\r\n" +
            "\r\n" +
            "    if (efrpgToolOk)\r\n" +
            "    {\r\n" +
            "        var generator = GeneratorFactory.Create(toolResult, fileManagement);\r\n" +
            "        if (generator != null && generator.InitialisationOk)\r\n" +
            "        {\r\n" +
            "            generator.ReadDatabase();\r\n" +
            "            generator.GenerateCode();\r\n" +
            "        }\r\n" +
            "        fileManagement.Process(true);\r\n" +
            "    }\r\n" +
            "#>";

        /// <summary>
        ///     The v3 entry point, once blank lines and comments are stripped. Comparing against this rather than
        ///     against the literal text is what lets a file carrying its own commented-out notes still upgrade.
        /// </summary>
        private static readonly string[] V3EntryPointStatements =
        {
            "var outer = (GeneratedTextTransformation) this;",
            "var fileManagement = new FileManagementService(outer);",
            "var generator = GeneratorFactory.Create(fileManagement, FileManagerFactory.GetFileManagerType());",
            "if (generator != null && generator.InitialisationOk)",
            "{",
            "generator.ReadDatabase();",
            "generator.GenerateCode();",
            "}",
            "fileManagement.Process(true);#>"
        };

        private static readonly Regex IncludeDirective =
            new Regex(@"^<#@\s*include\s+file\s*=\s*""(?<include>[^""]+)""\s*#>", RegexOptions.Multiline);

        private static readonly Regex EntryPoint =
            new Regex(@"^[ \t]*var\s+outer\s*=\s*\(GeneratedTextTransformation\)", RegexOptions.Multiline);

        private readonly string _text;
        private readonly List<TemplateUpgradeChange> _changes = new List<TemplateUpgradeChange>();
        private readonly List<string> _blockers = new List<string>();

        private TemplateUpgrade(string text)
        {
            _text = text;
        }

        /// <summary>
        ///     True when the include directive names the v3 file. This is the only reliable marker: the version
        ///     comment underneath it is prose a user may have edited or removed.
        /// </summary>
        public static bool IsV3(string templateText)
        {
            var match = IncludeDirective.Match(templateText ?? string.Empty);

            return match.Success &&
                   match.Groups["include"].Value.IndexOf(V3Include, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static TemplateUpgradeResult Upgrade(string templateText)
        {
            if (templateText == null)
                throw new ArgumentNullException(nameof(templateText));

            return new TemplateUpgrade(templateText).Run();
        }

        private TemplateUpgradeResult Run()
        {
            if (!IsV3(_text))
                return TemplateUpgradeResult.Refused(new[]
                {
                    "This template does not include " + V3Include + ", so there is nothing to upgrade."
                });

            var text = _text;

            text = SwapInclude(text);
            text = DeleteSetting(text, "FileManagerType",
                "Settings.FileManagerType no longer exists in v4 - the file manager is chosen automatically.");
            text = DeleteSetting(text, "DatabaseReaderPlugin",
                "Settings.DatabaseReaderPlugin no longer exists in v4 - database reading moved into the efrpg tool.");
            text = SimplifySeparateFilesCondition(text);
            text = RenameCleanUp(text);
            text = ReplaceEntryPoint(text);

            // Only when everything else worked. A refused entry point still contains
            // FileManagerFactory.GetFileManagerType(), so checking here anyway would add a second blocker
            // that is a consequence of the first rather than an independent problem to fix.
            //
            // One check per removed name, not per spelling of it, so a line mentioning both
            // Settings.FileManagerType and FileManagerType.Null does not produce two identical blockers.
            if (_blockers.Count == 0)
            {
                LeftoverCheck(text, "FileManagerType");
                LeftoverCheck(text, "DatabaseReaderPlugin");
                LeftoverCheck(text, "DatabaseReader.");
            }

            return _blockers.Count > 0
                ? TemplateUpgradeResult.Refused(_blockers)
                : TemplateUpgradeResult.Upgraded(text, _changes);
        }

        private string SwapInclude(string text)
        {
            var match = IncludeDirective.Match(text);
            var before = match.Value;
            var after = before.Replace(V3Include, V4Include);

            Record("Point the include directive at the v4 template.", before, after);

            return text.Substring(0, match.Index) + after + text.Substring(match.Index + match.Length);
        }

        /// <summary>
        ///     Removes a whole setting line, including its line ending, so nothing is left behind but the settings
        ///     around it - which keep their alignment because only complete lines are removed.
        /// </summary>
        private string DeleteSetting(string text, string settingName, string why)
        {
            var pattern = new Regex(@"^[ \t]*Settings\." + Regex.Escape(settingName) + @"[ \t]*=[^\r\n]*\r?\n",
                RegexOptions.Multiline);
            var match = pattern.Match(text);

            // Already absent is not a problem. A user who deleted it themselves has done half the upgrade.
            if (!match.Success)
                return text;

            Record(why, match.Value.TrimEnd('\r', '\n'), string.Empty);

            return text.Substring(0, match.Index) + text.Substring(match.Index + match.Length);
        }

        private string SimplifySeparateFilesCondition(string text)
        {
            var pattern = new Regex(
                @"if[ \t]*\([ \t]*Settings\.GenerateSeparateFiles[ \t]*&&[ \t]*Settings\.FileManagerType[ \t]*==[ \t]*FileManagerType\.\w+[ \t]*\)");
            var match = pattern.Match(text);

            if (!match.Success)
                return text;

            const string after = "if (Settings.GenerateSeparateFiles)";
            Record("Drop the FileManagerType half of the sub-folder condition.", match.Value, after);

            return text.Substring(0, match.Index) + after + text.Substring(match.Index + match.Length);
        }

        private string RenameCleanUp(string text)
        {
            if (text.IndexOf("DatabaseReader.CleanUp", StringComparison.Ordinal) < 0)
                return text;

            Record("DatabaseReader moved into the efrpg tool; CleanUp now lives on NamingHelper.",
                "DatabaseReader.CleanUp", "NamingHelper.CleanUp");

            return text.Replace("DatabaseReader.CleanUp", "NamingHelper.CleanUp");
        }

        /// <summary>
        ///     Replaces the whole tail of the file, because in v3 <c>fileManagement</c> is created after the
        ///     commented-out machine.config lines and in v4 it moves above the try block.
        /// </summary>
        private string ReplaceEntryPoint(string text)
        {
            var match = EntryPoint.Match(text);

            if (!match.Success)
            {
                _blockers.Add("The entry point block could not be found. It normally starts with " +
                              "'var outer = (GeneratedTextTransformation) this;' near the end of the file.");
                return text;
            }

            var tail = text.Substring(match.Index);

            if (!IsRecognisedV3EntryPoint(tail))
            {
                _blockers.Add("The entry point block has been changed from the standard v3 one, so it cannot be " +
                              "replaced safely. Upgrade this file by hand using the v3 to v4 guide.");
                return text;
            }

            // The replacement is written with CRLF; a file that uses bare LF keeps it, because a whole-file line
            // ending change would show up as every line differing in the user's next commit.
            var replacement = text.IndexOf("\r\n", StringComparison.Ordinal) >= 0
                ? V4EntryPoint
                : V4EntryPoint.Replace("\r\n", "\n");

            Record("Replace the entry point with the version that calls the efrpg tool.", tail, replacement);

            return text.Substring(0, match.Index) + replacement;
        }

        /// <summary>
        ///     Compares the statements only. Blank lines and comments are dropped first, so a file carrying its own
        ///     notes inside the block still upgrades, while one that has been genuinely restructured does not.
        /// </summary>
        private static bool IsRecognisedV3EntryPoint(string tail)
        {
            var statements = tail
                .Replace("\r\n", "\n")
                .Split('\n')
                .Select(line => line.Trim())
                .Where(line => line.Length > 0 && !line.StartsWith("//", StringComparison.Ordinal))
                .ToList();

            return statements.SequenceEqual(V3EntryPointStatements, StringComparer.Ordinal);
        }

        /// <summary>
        ///     Anything still naming a type or setting that v4 removed will not compile, so it is a refusal rather
        ///     than something to leave for the user to find at generation time.
        /// </summary>
        private void LeftoverCheck(string text, string fragment)
        {
            if (text.IndexOf(fragment, StringComparison.Ordinal) < 0)
                return;

            _blockers.Add("This template still refers to '" + fragment +
                          "', which v4 removed, in a place this upgrade does not know how to change.");
        }

        private void Record(string description, string before, string after)
        {
            _changes.Add(new TemplateUpgradeChange(description, before, after));
        }
    }
}
