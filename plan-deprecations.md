# Plan: remove three features before v4 ships

Working checklist. Tick items as they are done. Companion to `plan.md`, which covers the GUI.

## Decision

Remove, before the v4 release, with v3 remaining downloadable as the home for anyone who needs them:

1. **Multi-context generation** - `Settings.GenerateSingleDbContext = false`, the `MultiContext*` settings, the
   `MultiContext.*` settings tables, `IMultiDbContextSettingsPlugin`, and the tool's `--multi-context` reading.
2. **File-based templates** - `TemplateType.FileBased*`, `Settings.TemplateFolder`, `TemplateFileBased`, and the
   `_File based templates` folder BuildTT generates.
3. **`GeneratorType.Custom`** and `GeneratorCustom`.

**Keep EF6.** It is 84 issues in the tracker against multi-context's 4, the returning base that is 87% of
revenue is the population most likely to be on it, and it touches nothing in the wire format, so dropping it
can wait for its own question after v4 has shipped.

**No survey.** The tracker already answers it: over 735 issues, multi-context is mentioned in 4, file-based
templates in about 10, the custom generator in fewer once the loose matches are discounted. The cost of being
wrong is a user staying on v3 for that project, which is frozen and works.

**Why now and not later.** Multi-context reaches into the tool: the `--multi-context` flag and the
`<MultiContextSettings>` wire element. The wire contract is additive-only *after* release, because an older
template must keep working with a newer tool. The only templates that ever read that element are the
pre-release v4 ones in this repository, so this is the one moment removal is free. After release it is a
major-version event. Order the work so the wire change lands first.

## Rules for the removal

- **The mustache engine stays.** Every inline template is a mustache string; only reading them from a folder goes.
- **No `SchemaVersion` bump.** The template stops *sending* `--multi-context` and stops *reading*
  `<MultiContextSettings>`; the reader already ignores elements it does not know. Nothing new is required of the
  tool. Record this exception in the wire-contract section of `CLAUDE.md`, so nobody later reads "never remove"
  and thinks the rule was broken.
- **The v3 to v4 upgrade must remove what v4 no longer compiles.** A v3 template assigns
  `Settings.GenerateSingleDbContext`, the `MultiContext*` settings and `Settings.TemplateFolder`; after this work
  those are compile errors in v4. The upgrade deletes them, whole statements including the multi-line
  `MultiContextAllFields*Processing` lambdas, through `StatementScanner`. A v3 template that *uses* a removed
  feature - `GenerateSingleDbContext = false`, a `FileBased*` template type, `GeneratorType.Custom` - is refused
  with the reason and a pointer to v3, the same way a restructured entry point is refused today.
- **Minimal diffs everywhere else.** Delete what the three features own; do not tidy around them.
- **BuildTT after every change under `Generator/`**, and the regenerated `.ttinclude`, `Database.tt` and
  `settings-metadata.v4.json` go in the same commit. `settings-metadata.v3.json` is frozen and keeps listing
  everything, correctly.

## 1. Multi-context

### Generator

- [x] `Generator/Settings.cs` - delete `GenerateSingleDbContext`, `MultiContextSettingsConnectionString`,
      `MultiContextSettingsPlugin`, `MultiContextAttributeDelimiter`, and the four `MultiContextAllFields*Processing`
      delegates (lines 936-1000 region)
- [x] `Generator/Filtering/` - delete `MultiContextFilter.cs`, `MultiContextSettings.cs`,
      `MultiContextNameNormalisation.cs`; collapse `DbContextFilterList.cs` and `IDbContextFilterList.cs` to a
      single `SingleContextFilter` rather than deleting them, because `Generator.cs` iterates the filter
      dictionary in a dozen places and keeping the shape kept those diffs to zero; remove the multi-context
      prose from `FilterSettings.cs`; remove the reserved-`MultiContext`-schema rule from `SchemaFilter.cs`.
      `EnumerationSettings` moved out of the deleted `MultiContextSettings.cs` into its own file, in both
      repositories, because the enum pass still uses it
- [x] `Generator/Util/MultiContextSettingsCopy.cs` - delete
- [x] `Generator/Generators/Generator.cs` - the `GenerateSingleDbContext` branches at ~323, ~725, ~739, ~1637 and
      ~2016 collapse to the single-context path; the filter dictionary becomes one `SingleContextFilter`
- [x] `Generator/FileManagement/FileManagementService.cs` - lines 44 and 51 lose the `GenerateSingleDbContext` term
- [x] `Generator/AssemblyHelper.cs` - plugin loading existed for `IMultiDbContextSettingsPlugin`; delete it if
      nothing else uses it, and the `IMultiDbContextSettingsPlugin` interface with it
- [x] `Generator/Readers/EfrpgResult.cs` - delete `MultiContextSettings` and the DTOs behind it
- [x] `Generator/Readers/EfrpgResultXmlReader.cs` - delete the `<MultiContextSettings>` reading (~line 286 and
      the `ReadContext*` methods)
- [x] `Generator/Readers/EfrpgToolRunner.cs` - `ReadDatabase` loses the `multiContext` parameter and stops
      passing `--multi-context` and the second connection
- [x] `Generator/Readers/SecretsXml.cs` - stop writing `MultiContextConnection`. This file is linked into
      `Efrpg.Gui.Core`; `EfrpgSchemaReader` already passes null for it

### BuildTT and the shipped template

- [x] `BuildTT/BuildTT.cs` footer - delete the "Generate multiple db contexts in a single go" block, and in the
      entry point the `efrpgMultiContext` line and the third argument to `ReadDatabase`
- [x] Run BuildTT; `Database.tt`, the `.ttinclude` and `settings-metadata.v4.json` regenerate. The v4 metadata
      loses eight settings and the "Generate multiple db contexts in a single go" section

### GUI

- [x] `Efrpg.Gui.Core/TemplateUpgrade.cs` - `V4EntryPoint` must be byte for byte the new `Database.tt` tail; the
      guard test says when it is not. Add the statement deletions listed under Rules, and the refusal for
      `GenerateSingleDbContext = false`
- [x] `Efrpg.Gui.Core/TemplateFilterDocument.cs` - delete `GeneratesSingleContext` and its `RefusalReason` branch
- [x] `Efrpg.Gui.Core/ObjectSelection.cs` - delete the reserved-`MultiContext`-schema lock
- [x] `Efrpg.Gui.Core.Tests` - `ObjectSelectionTests` and `TemplateFilterDocumentTests` lose their multi-context
      cases; `TemplateUpgradeTests` gains the deletion and refusal cases against `Fixtures/Database.v3.14.1.tt`

### Tests, testers and test databases

- [x] `Generator.Tests.Unit` - delete `MultiContextFilterTests.cs`, `MultiContextNameNormalisationTests.cs`,
      `MultiContextSettingsPluginNorthwind.cs`, `MultiContextSettingsTests.cs`,
      `NullMultiDbContextSettingsPlugin.cs`, `MultiTables.sql` and `Scripts/MultiTables.sql`, and their
      `.csproj` entries
- [x] `Generator.Tests.Unit/WireContractTests.cs` - drop the `MultiContextSettings` assertion (~111) and the
      known-gap entry (~53). The fixture keeps its `<MultiContextSettings>` element until the tool is regenerated;
      the reflection test only checks DTOs that still exist, so it stays green either way
- [x] `Generator.Tests.Unit/DocSamples/DocSampleRunner.cs` 287-290 and `SettingsPageCoverageTests.cs` 91 - remove
- [x] `Generator.Tests.Integration` - delete `MultiContextDatabaseReverseEngineerTests.cs` and its `.csproj`
      entry; `SingleDatabaseTestBase.cs` lines 30-31 and 90 go
- [x] `Tester.Integration.EFCore8/Multi context many files/` and `Multi context single files/` - delete the
      folders, their generated output and their `.csproj` entries. `Single context many files/` stays
- [x] Every in-repo `.tt` (24 files across `EntityFramework.Reverse.POCO.Generator/` and `Tester.Integration.*`)
      carries `Settings.GenerateSingleDbContext = true;`, the `MultiContext*` lines and the `efrpgMultiContext`
      entry-point lines. Script the deletion - it is the same statement-level edit the v3 upgrade performs, so
      reuse it rather than a second regex - then run every `.tt` and confirm the generated output is unchanged
- [x] `TestDatabases/SQLServer/EfrpgTest_Settings.sql` - delete; remove the `MultiContext.*` tables from
      `EfrpgTest (manually created).sql`

### The tool - `C:\S\Source (open source)\Efrpg`, its own repository and its own commit

The tool reads the `MultiContext.*` tables in every dialect and carries the settings for it, so this is not a
flag removal; it is the same feature removed a second time. Nothing here is shared with this repository except
the wire format and the two fixtures, so it is done after section 1 and verified with its own tests.

- [x] `Program.cs` - delete the `--multi-context` and `--multi-context-connection-base64` options (lines 38-39,
      91-96), the `readMultiContext` flag (54), the `MultiContextConnection` secret (109-110), the
      `Settings.MultiContextSettingsConnectionString` assignment (153) and the `ReadMultiContextSettings` call
      (247-248); fix the `--secrets-stdin` help text (30) so it no longer shows `<MultiContextConnection>`
- [x] `Readers/SecretsXml.cs` - stop reading `MultiContextConnection`. This is the writer's twin of the file
      linked into `Efrpg.Gui.Core`; the two must still produce and parse the same `<Secrets><Connection>` shape
- [x] `Readers/EfrpgResult.cs` and `Readers/EfrpgResultXmlWriter.cs` - delete `MultiContextSettings` and the
      writing of the `<MultiContextSettings>` element
- [x] `Readers/DatabaseReader.cs` and each of `SqlServerDatabaseReader.cs`, `PostgreSqlDatabaseReader.cs`,
      `MySqlDatabaseReader.cs`, `OracleDatabaseReader.cs`, `SQLiteDatabaseReader.cs` - delete
      `ReadMultiContextSettings` and the SQL behind it
- [x] `Filtering/MultiContextSettings.cs` and `Readers/IMultiDbContextSettingsPlugin.cs` - delete, with whatever
      `Efrpg.csproj` says about them
- [x] `Settings.cs` - delete the `MultiContext*` settings the tool copied over from the generator
- [x] `Efrpg.Tests.Unit/WireContractTests.cs` - drop the `MultiContextSettings` assertions; then regenerate (done by
      removing the element from the captured fixture, since that is the only way the reduced tool's output differs)
      `Efrpg.Tests.Unit/WireContract/EfrpgResult.xml` and `EnumData.xml` with the reduced tool, per the recipe in
      each fixture's header, and copy both to `Generator.Tests.Unit/WireContract/` in this repository. Both
      `WireContractTests` must pass on the same bytes
- [x] `CLAUDE.md`, `AGENTS.md` and `.claude/dotnet-tool-work.md` there - remove the multi-context material
- [x] `--help` and `README.md` - remove the two options
- [x] `Efrpg.csproj` `<Version>` - bump and publish. Removing command-line options is a breaking change under
      SemVer, so 2.0.0 unless 1.x is regarded as pre-release; the number is independent of `BuildTT/version.txt`
      by design and must stay so

**Wire format.** `<MultiContextSettings>` and `<MultiContextConnection>` go; nothing is added. `SchemaVersion`
stays at 1: a newer template with an older tool works, because the template no longer asks for the element
and the tool only ever wrote it when asked; an older pre-release template with the newer tool fails loudly on
the unknown option, which is acceptable for a template that was never released. If either half of that stops
being true - a public v4 template exists that sends `--multi-context` - bump `SchemaVersion` after all.

## 2. File-based templates

- [x] `Generator/Templates/` - delete `TemplateFileBased.cs` and `TemplateFileBasedConstants.cs`; remove the
      `FileBased*` cases from `TemplateFactory.cs` and the four members from `TemplateType.cs`
- [x] `Generator/Settings.cs` - delete `TemplateFolder` (line 28) and shorten the `TemplateType` comment on
      line 19 to the remaining members. `SettingsMetadataTests` checks that comment names every member
- [x] `Generator/Generators/Generator.cs` ~1633-1654 - the `TemplateFolder` fallback and per-context template
      path logic go with the multi-context branch above
- [x] `BuildTT/TemplateFiles.cs` - delete, with its `Program.cs` call and `.csproj` entry; delete the
      `_File based templates/` folder; remove the `TemplateFolder` line and the `TemplateType` comment from the
      `BuildTT.cs` footer; fix the comment in `SettingsMetadata/SettingsMetadataWriter.cs` line 22
- [x] `Efrpg.Gui.Core/TemplateTarget.cs` - drop the four `FileBased*` entries and `RequiresTemplateFolder`.
      `TemplateTargetTests` compares the list to the metadata's enum members, so regenerate the metadata first
- [x] `EntityFramework Reverse POCO Generator/ConnectionDialog.cs` - delete the file-based hint and
      `TemplateChanged`'s use of it
- [x] `Efrpg.Gui.Core/TemplateUpgrade.cs` - delete `Settings.TemplateFolder` on upgrade; refuse a `FileBased*`
      template type
- [x] Tester `.tt` files that set `Settings.TemplateFolder` (10 files, all the default expression) - covered by
      the scripted deletion above
- [x] `Generator.Tests.Unit/SettingsMetadataTests.cs` and the GUI tests that mention `TemplateFolder` - update

## 3. GeneratorType.Custom

- [x] `Generator/Generators/GeneratorCustom.cs` - delete; remove the case from `GeneratorFactory.cs` and the
      member from `GeneratorType`; shorten the comment on `Settings.cs` line 20
- [x] `Generator.Tests.Unit/ViewTests.cs` - it constructs `GeneratorCustom` as its subject; switch it to
      `GeneratorEfCore`, or delete it if it only ever tested the custom generator
- [x] Tester `.tt` files mentioning `Custom` (9 files) - all in the `GeneratorType` comment; covered by the
      scripted edit
- [x] `Efrpg.Gui.Core/TemplateUpgrade.cs` - refuse `GeneratorType.Custom`

## 3b. Visual Studio 2019 and earlier

Assumed unused. The v4 extension takes a dependency on the VS 2022 toolkit, so it could not have run there in
any case; the manifest merely let it install.

- [x] `EntityFramework Reverse POCO Generator/source.extension.vsixmanifest` - delete the `[15.0,17.0)`
      installation targets for VS 2017 and 2019, keeping `[17.0,)`
- [x] `BuildTT/VersionSetter.cs` `UpdateVsixManifest` - the same, or the next release puts them back
- [x] `plan.md` - the paragraph under "Chunk B as built" that says the 2017/2019 entries are untouched

## 4. Documentation

### The wiki - `C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-First-Generator.wiki`, its own repository and commit

The wiki is the product's documentation for v3 users too, but the pages for a removed feature come out rather
than being banner-stamped: a page that describes a setting the current template does not have is a support
question waiting to happen. The old text stays reachable in the wiki's git history, and the upgrade page says
so. 107 pages; 20 are touched.

- [x] Delete the five pages that exist only for a removed feature:
      `Generating-Multiple-Database-Contexts-in-a-Single-Go.md`, `Settings.GenerateSingleDbContext.md`,
      `Settings.MultiContextAllFieldsProcessing.md`, `Custom-File‐Based-Templates.md`,
      `Settings.TemplateFolder.md`
- [x] `_Sidebar.md` (lines 43 and 50) and `Home.md` (154, 170) - remove the two links; `Home.md`'s template
      table at 55-60 is already the surviving set
- [x] `Settings-Reference.md` - drop the removed settings and the `FileBased*` and `Custom` enum members
- [x] `Upgrading-from-v3-to-v4.md` - add a "Removed in v4" section: the three features, one line each on why,
      that v3 stays downloadable and frozen, that the right-click upgrade deletes the dead settings from a v3
      file and refuses a file that actually uses a removed feature, and that the old pages are in the wiki's
      history
- [x] Multi-context mentions to edit out: `Filtering.md`, `Full-Control-Over-the-Generated-Code.md`,
      `SQL-Server.md`, `Settings-Callbacks.md`, `Settings.Runtime-Values.md`
- [x] File-based template mentions to edit out: `Common-Settings-Types-Explained.md`, `Settings.DatabaseType.md`,
      `How-to-implement-OnPropertyChanged.md`, `Owned-Entities.md`, `Settings.UseResharper.md`,
      `Upgrading-version.md`, and again `Full-Control-Over-the-Generated-Code.md` and `Settings.Runtime-Values.md`
- [x] Custom generator mentions: `Common-Settings-Types-Explained.md` and `Settings.DatabaseType.md`
- [x] This repository's tests police the wiki, so run them against the edited checkout before committing
      either side: `WikiSnippetDriftTests` regenerates every `<!-- docsample -->` block (`Filtering.md` carries
      one and mentions multi-context), `SettingsPageCoverageTests` expects a page per setting and lists the
      `MultiContextAllFields` wildcard at line 91, and `DocSamples/build_index.py` indexes the pages. Read
      `Generator.Tests.Unit/DocSamples/README.md` first; the generated code blocks are never hand-edited

### This repository

- [x] `CLAUDE.md` and `AGENTS.md` - delete the "Multi-Context Support" section, the `FileBased*` rows under
      "Template Types", every mention of `_File based templates`, and the BuildTT sentence that lists it as an
      output; add the `SchemaVersion` exception noted under Rules
- [x] `TODO.md` item 6 (`IMultiDbContextSettingsPlugin` excluded from the tool build) - delete, it is moot
- [ ] `README.md` and the release notes - one paragraph: what was removed, why, and that v3 remains

## 5. Order, and how it is verified

Do it in the numbered order above; multi-context first because it is the wire change, and because the
file-based per-context template path lives inside its branch of `Generator.cs`.

After each numbered section: build the solution, run BuildTT and confirm `git status` shows only intended
changes, run `Generator.Tests.Unit` and `Efrpg.Gui.Core.Tests`. After section 1 also run every `.tt` and the
integration tests against `EfrpgTest` and `Northwind`; the generated output for the single-context testers must
not change by a byte, which is the proof that removing the multi-context branch removed nothing else.

The VSIX is rebuilt once at the end, Release, `-t:Rebuild`, version 4.0.25, and the packaged manifest checked as
in the release steps in `CLAUDE.md`.

The tool is done straight after section 1, in its own repository and commit, and its fixture regeneration
closes the loop: the fixtures are copied here, and `WireContractTests` on both sides pass on the same bytes
before section 2 starts. Until the tool is published, the installed `efrpg` on this machine is the reduced
build, so the `.tt` runs and integration tests in this repository exercise the real pairing and not a stale
tool that still answers `--multi-context`.
