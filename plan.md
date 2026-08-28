# Plan: Visual Studio GUI for the generator

Working checklist. Tick items as they are done.

## Why

New paying customers have gone **214 → 142 → 117 → 56 → 48 → 20 → 14** over six years, while the returning
base stayed loyal (50-60% renewal, 87% of revenue, rising seat counts). Capability is not the problem - the
first five minutes is. A newcomer who installs the VSIX gets a `.tt` file and a wall of settings; the same
developer installing the free EF Core Power Tools gets a wizard, ticks some tables, and has code.

**The goal is the newcomer's first five minutes, not feature parity.**

## Decisions already made

Recorded so they are not relitigated mid-build.

- **Community.VisualStudio.Toolkit**, not raw VSSDK. Same capability, far less ceremony.
- **In-process VSIX**, not the new out-of-process VisualStudio.Extensibility model - `IWizard` is not
  available out-of-process, and the auto-popup on Add is the highest-value piece.
- **Roslyn surgical edit** for round-tripping an existing `.tt`, not regeneration and not a side-car JSON
  config. Regeneration destroys customisation; a side-car splits the product's "it is all in one file"
  identity, which the paying base likes.
- **The GUI shells out to `efrpg`** for schema, exactly as the T4 template does. No second copy of the
  readers - that is the duplication `WireContractTests` exists to police - and the picker therefore cannot
  show tables the generator would skip.
- **The GUI binary ships in the VSIX**, not in the `efrpg` tool package. WPF would force `efrpg` to
  `net10.0-windows` and kill the cross-platform CLI; a second `dotnet tool install` would add friction to the
  exact moment we are trying to make frictionless.
- **The `.tt` stays authoritative.** The GUI never becomes the only way in. Every setting it cannot
  represent must round-trip untouched.

## Solution structure

```
Efrpg.Gui.Core        netstandard2.0    Roslyn settings model, .tt parse/write, tool detection.
                                        All the logic. No VS reference.
Efrpg.Gui.Core.Tests  net10.0           NUnit against the above.
Efrpg.Vsix            net48             Toolkit package, IWizard, .vsct, WPF views.
                                        Kept deliberately thin.
```

**The TFMs are forced, not chosen.** An in-process Visual Studio extension runs inside the VS process, which
is .NET Framework - the existing VSIX project is already `<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>`.
Neither `net8.0-windows` nor `net10.0-windows` will load in-process.

**Why in-process WPF rather than a separate .NET 10 exe.** An out-of-process GUI could target `net10.0-windows`
and match `efrpg`, but WPF on .NET 10 needs the **.NET 10 Windows Desktop Runtime**, which is a separate
install. The GUI's first job is to tell a user that `efrpg` is missing - and a team still on .NET 8 would have
no .NET 10 Desktop Runtime, so the dialog whose whole purpose is removing a prerequisite would itself be
blocked by one. Visual Studio already hosts WPF, so in-process has no prerequisite at all. Self-contained
deployment would also solve it, at 70-150 MB added to the VSIX - a poor trade for a wizard.

**`netstandard2.0` for Core is the load-bearing choice.** Roslyn (`Microsoft.CodeAnalysis.CSharp`) ships
netstandard2.0, so Core is consumable from the net48 VSIX *and* from a modern test project, and a future
cross-platform Avalonia shell can reuse it without a rewrite.

VSIX debugging is miserable. Anything worth testing goes in `Efrpg.Gui.Core` where NUnit can reach it.

---

## Phase 0 - settings metadata

The GUI needs to know which settings exist, their types, enum values and help text. Hand-maintaining that
list would drift from `Database.tt` the way the language mappings drifted from each other.

- [x] Extend `BuildTT` to emit `settings-metadata.json` alongside `Database.tt`
- [x] Include for each setting: name, CLR type, whether it is an enum and its members, default value
- [x] Parse the trailing `//` comment on each `Settings.*` line as the help text - `Database.tt` already
      documents every setting inline, so the GUI's tooltips maintain themselves
- [x] Ship `settings-metadata.v4.json` in the VSIX
- [x] Hand-write `settings-metadata.v3.json` **once** - v3 is frozen at 3.14.1 and will never change, so it
      needs no generator. Derive it from the v3 `Database.tt` in git history
- [x] Unit test: every `Settings.*` assignment in `Database.tt` appears in the v4 metadata

**Verification:** re-running BuildTT on an unchanged tree rewrites the metadata byte for byte. Confirmed.

### As built

`BuildTT/SettingsMetadata/` writes `EntityFramework.Reverse.POCO.Generator/settings-metadata.v4.json` -
119 settings, 104 of them assigned in `Database.tt`. The VSIX links both metadata files rather than copying
them, and they are deliberately kept out of `efrpoco.zip`, which is unpacked into the user's project.

**Two sources, each answering only what it can.** Reflection over `Efrpg.Settings` is the authority on which
settings exist and what type each one is - it cannot go stale, because it is the assembly the generator runs.
The source text of `Database.tt` and `Settings.cs` is the authority on help text and on the value a new
template starts with, neither of which survives compilation. `defaultValue` is therefore always the **source
text**, never a reflected runtime value: `Settings.Namespace` evaluates to `"Efrpg"`, and
`Settings.TemplateFolder` is `""` in code but `Path.Combine(Settings.Root, "Templates")` in the template. The
GUI writes C# into a `.tt`, so source text is the thing it actually needs.

**Emitted per setting:** `name`, `type`, `kind`, `section`, `help`, `defaultValue`, `inDatabaseTt`,
`commentedOut`, `multiLine`, `runtimeOnly`, plus `isFlags` and `enumMembers` for enums. `kind` is the render
hint the later phases classify on - `bool`, `string`, `char`, `number`, `enum`, `stringList`, `callback`,
`complex`. The 27 `callback` and 3 `complex` settings are Phase 3's *"customised in code"* category, already
identified.

**Settings absent from `Database.tt` are still emitted** - 15 of them. Four (`PrependSchemaNameForTable`,
`PrependSchemaNameForStoredProcedure`, `ReadStoredProcReturnObjectCompleted`,
`ReadStoredProcReturnObjectException`) already appear in this repo's own `Tester.Integration.*` templates, so
metadata built from `Database.tt` alone would leave the GUI blind to settings that do occur in real files. The
four the generator fills in at run time (`Root`, `TemplateFile`, `DefaultSchema`, `FilterCount`) carry
`runtimeOnly: true` rather than being dropped, so a new plumbing field surfaces as a spurious GUI entry
instead of vanishing silently.

`settings-metadata.v3.json` was produced once by running this same writer against the `Generator/` and
`Database.tt` of tag `v3.14.1`, then frozen with a `note` recording that. It holds 121 settings: the v4 set
plus `FileManagerType` and `DatabaseReaderPlugin`, which is exactly the difference the two files exist to keep
apart. No setting changed type between v3 and v4.

**Tests** are the five in `Generator.Tests.Unit/SettingsMetadataTests.cs`. They scan `Database.tt` with their
own deliberately crude regex rather than calling BuildTT's parser - a test that reused the parser would agree
with it about a setting it had dropped. Beyond the checklist item they assert both reverse directions (nothing
described that `Settings` no longer has; nothing on `Settings` missing from the file - the "BuildTT was not
run" guard, proved by adding a field and watching it fail), that every enum lists its members, and that the v3
file has not been overwritten with a copy of the v4 one.

**Left out deliberately:** `FilterSettings.Include*` is not in the metadata. Phase 2's table / view / stored
procedure checkboxes map onto those five flags, so that is the moment to add a second array to this file.

**Why two files.** `Settings.FileManagerType` is valid in v3 and poison in v4 - showing it for a v4 file
would make the GUI write code that does not compile. The metadata is what keeps the two apart.

---

## Phase 1 - VSIX skeleton and the tool gate

De-risks the plumbing before any UI depends on it.

- [ ] Add `Community.VisualStudio.Toolkit.17` and `Microsoft.VSSDK.BuildTools` to the VSIX project
- [ ] Convert the VSIX from item-template-only to carrying a `ToolkitPackage`
- [ ] Add the `Microsoft.VisualStudio.VsPackage` asset to `source.extension.vsixmanifest`
      **via `BuildTT/VersionSetter.cs:UpdateVsixmanifest`, not by hand** - that method writes the file
      wholesale, so a hand edit is deleted on the next BuildTT run
- [ ] Build `EfrpgToolGate` in `Efrpg.Gui.Core`:
  - [ ] Locate `efrpg` on PATH, and at `%USERPROFILE%\.dotnet\tools\efrpg.exe` as a fallback
  - [ ] Run `efrpg --version`, parse the package version and the **wire format schema version**
  - [ ] Compare the schema version against `EfrpgResultXmlReader.RequiredSchemaVersion`
  - [ ] Check `dotnet --version` - `dotnet tool install` needs the **SDK**, not just the runtime
  - [ ] Install: `dotnet tool install -g Efrpg`
  - [ ] Update: `dotnet tool update -g Efrpg`
  - [ ] Surface stderr verbatim on failure - do not swallow it
- [ ] Unit test `EfrpgToolGate` against a fake process runner: missing, too old, current, no SDK, no network
- [ ] Gate dialog with three choices: **Install/Update**, **Copy command**, **Continue anyway**
- [ ] Always display the exact command next to the button

**Check the schema version, not the package version.** The schema version is what the template actually
floor-checks, so the gate becomes the same test the generator does - surfaced as a friendly dialog instead
of a comment in a broken output file.

**"Copy command" is not a nicety.** It is the escape hatch for developers behind a proxy, on an internal
NuGet feed, or without permission to install.

**Deliverable:** a command that reports tool status.

---

## Phase 2 - wizard on Add → New Item

**This is the phase that addresses the acquisition collapse.** It needs no Roslyn at all. See
[Sequencing](#sequencing) - if effort has to be cut, cut Phase 3, never this.

- [ ] `ReversePocoWizard : Microsoft.VisualStudio.TemplateWizard.IWizard`
- [ ] Wire it into `MyTemplate.vstemplate` via `<WizardExtension>` -
      **through `BuildTT/VersionSetter.cs:UpdateVstemplate`**, which also regenerates that file wholesale
- [ ] `RunStarted`: tool gate → connection dialog → test connection
- [ ] Shell out to `efrpg --secrets-stdin` for the schema (same binary and wire format the template uses)
- [ ] Checkbox tree: tables, views, stored procedures
- [ ] Fields: DbContext name, namespace, EF version, database type
- [ ] Write answers into `replacementsDictionary`
- [ ] Flip `ReplaceParameters` to `true` for `Database.tt` in the vstemplate
- [ ] Unit test asserting `Database.tt` contains **zero `$` characters** - true today, and what makes token
      substitution safe; the test stops that silently changing
- [ ] `throw new WizardBackoutException()` on cancel so VS cleans up the half-created item
- [ ] After a successful install, invoke the tool **by full path** for the wizard's own schema read, and
      tell the user to restart Visual Studio before saving the `.tt`

**The PATH trap.** VS caches its environment at launch, so a tool installed by the wizard is not on the PATH
that `EfrpgToolRunner` uses - it calls `new ProcessStartInfo("efrpg", …)` and relies on PATH resolution.
Without the full-path fallback and the restart prompt, users hit "it said it installed but generation still
fails".

**Budget an afternoon for wizard assembly resolution.** The `<Assembly>` element needs the full strong name
and fails obscurely when wrong. It is the most annoying part of this phase.

**Verification:** on a clean VM with no `efrpg` installed, Add → New Item → reverse poco produces working
generated code without the user reading any documentation.

---

## Phase 2b - "Upgrade to v4"

The v3 to v4 migration is four mechanical edits - the same ones in the upgrade guide - and they are exactly
the anchored span edits this GUI already does. **Getting the v3 base onto v4 is what makes the `efrpg` tool
ubiquitous**, which matters given the licence check lives there.

- [ ] Its own `.vsct` command, offered only when a v3 file is selected
- [ ] Prompt unprompted the first time a v3 template is opened after the extension updates
- [ ] Change the include directive to `EF.Reverse.POCO.v4.ttinclude`
- [ ] Delete the `Settings.FileManagerType` assignment
- [ ] Rewrite `if (Settings.GenerateSeparateFiles && Settings.FileManagerType == FileManagerType.EfCore)`
      to `if (Settings.GenerateSeparateFiles)`
- [ ] Replace the entry-point block with the `EfrpgToolRunner.ReadDatabase` version
- [ ] Replace `DatabaseReader.CleanUp` with `NamingHelper.CleanUp` if present
- [ ] Show a diff preview and require confirmation before writing
- [ ] **If the file does not match the expected shape exactly, refuse** and link to the upgrade guide

**Refusing is the important item.** During the v4 work, 24 tester templates were migrated by script and it
took two passes - some carried an extra commented-out line inside the block the first pattern expected.
Customer files will vary more than in-repo ones. A half-applied migration leaves a template that neither
compiles nor matches the guide, which is worse than not offering the button.

**Independent of Phase 3.** Needs the Phase 1 plumbing and version detection, and nothing else - no Roslyn,
no metadata files, no settings form, no round-trip property tests. It is anchored find-and-replace plus a
diff dialog.

---

## Phase 3 - right-click "ReversePOCO Settings…"

The Roslyn round-trip. Serves people who already bought.

- [ ] `.vsct` command on `.tt` files, visible when the file includes **either** `EF.Reverse.POCO.v4.ttinclude`
      **or** `EF.Reverse.POCO.v3.ttinclude` - the settings blocks are near-identical, and almost the whole
      installed base is still on v3
- [ ] Detect the version from the `<#@ include file="..." #>` directive on line 1 and load the matching
      metadata file. **Version must be first-class, not inferred later**
- [ ] **Do not hijack double-click or Open** - the paying base lives in the text editor and expects it
- [ ] Parse: extract the text between the opening `<#` and its matching `#>`, wrap as `void M() { … }`,
      `CSharpSyntaxTree.ParseText`
- [ ] Walk for `ExpressionStatementSyntax` → `AssignmentExpressionSyntax` where the left is
      `MemberAccessExpressionSyntax` on `Settings`
- [ ] Classify the right-hand side:
  - [ ] `LiteralExpressionSyntax` → editable (textbox / checkbox / number)
  - [ ] `MemberAccessExpressionSyntax` on a known enum → dropdown, values from the Phase 0 metadata
  - [ ] anything else (lambda, `new`, method call) → **read-only**, labelled
        *"customised in code - edit in the editor"*
- [ ] Show `FilterSettings.*.Add(...)` calls read-only
- [ ] Write back by **replacing only `assignment.Right.Span`** in the original file text, offset by the
      block's start position
- [ ] Never re-render the syntax tree - that is what would eat comments, formatting and the T4 markers

### Round-trip tests (do these properly)

This phase can silently destroy a paying customer's customisation. It deserves the paranoia applied to
`WireContractTests`.

- [ ] Fixtures: the real `Database.tt`, `Northwind.tt`, several `Tester.Integration.*` templates, **and a
      v3 `Database.tt` taken from git history**
- [ ] **Property: load, change one setting, save → the diff is exactly one line**
- [ ] Load and save with no change → file is byte-for-byte identical
- [ ] A template with a custom `Settings.ForeignKeyName` lambda survives untouched
- [ ] A template with regex `FilterSettings` survives untouched
- [ ] CRLF line endings preserved

---

## Phase 4 - column exclusion and rename

Where EF Core Power Tools is genuinely ahead: checkbox per column, F2 to rename.

- [ ] Column-level exclusion
- [ ] Table and column renaming
- [ ] Persistence - per-column choices do not fit `Settings.*` assignments, so this is the point where a
      side-car file becomes unavoidable. By then we will know whether users want one.

---

## Sequencing

**Order: Phase 1 → 2 → 2b → 3 → 4.**

- **Phase 2 (wizard)** serves people who do not yet have a `.tt` - exactly the population we are failing to
  convert - and carries none of the parsing risk.
- **Phase 2b (upgrade)** serves the 576,893 v3 installs and drives adoption of the tool the licence check
  will live in. Cheap, and independent of everything in Phase 3. **Pull it ahead of Phase 2 if v4 adoption
  stalls after launch** - it ships with nothing more than Phase 1.
- **Phase 3 (settings editor)** serves people who already bought and are already productive. It is the most
  expensive phase and the one that can damage a customer's file.

If effort has to be cut, cut Phase 3. Never cut 2 or 2b.

---

## Out of scope

- **Rider / VS Code.** A standalone Avalonia shell on `net10.0-windows` would serve them, reusing
  `Efrpg.Gui.Core` and still shelling out to `efrpg`. A second audience we do not have yet - all 576,893
  installs are Visual Studio. Note it would carry the .NET 10 Desktop Runtime prerequisite the in-process
  VSIX avoids.
- **Model-first / DDL generation.** Devart territory. Different product.
- **Replacing the `.tt` file.** See decisions above.

---

## Related work not in this plan

From `TODO.md`, the database gaps behind the same competitive comparison:

- Azure Synapse - likely already works via the SQL Server reader; test before building anything
- `.dacpac` - does not fit `DatabaseReader` (no connection, no SQL), but fits as a **sibling** producing an
  `EfrpgResult` directly. The only item that removes a hard blocker rather than adding a dialect
- Firebird - textbook fit, roughly the MySQL reader again
- Snowflake - fits but degraded (foreign keys unenforced, no stored procedure result sets)
- Azure Data Explorer - **will not work**; not relational, not SQL, no `DbProviderFactory`. Document the
  refusal rather than attempting it

**Adding databases is what we have been doing while new customers went 214 → 14.** EF Core Power Tools beats
us on databases *and* has a wizard. The wizard is the likelier reason it wins evaluations.
