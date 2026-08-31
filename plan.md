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


| Project                                | Framework      | Description                                                                                                                |
|----------------------------------------|----------------|----------------------------------------------------------------------------------------------------------------------------|
| Efrpg.Gui.Core                         | netstandard2.0 | Roslyn settings model, .tt parse/write, tool detection. All the logic. No VS reference.                                    |
| Efrpg.Gui.Core.Tests                   | net10.0        | NUnit against the above.                                                                                                   |
| EntityFramework Reverse POCO Generator | net48          | The EXISTING VSIX project, not a new one. Gains the Toolkit package, IWizard, .vsct and WPF views. Kept deliberately thin. |

**Only the two `Efrpg.Gui.Core*` projects are new.** The third row is the VSIX project that already ships;
Phase 1 converts it from item-template-only rather than adding a second extension.

**The extension identity and the output filename must not change.** `source.extension.vsixmanifest` carries
`Id="EntityFramework_Reverse_POCO_Generator..d542a934-8bd6-4136-b490-5f0049d62033"` and the project's
`<AssemblyName>` produces `EntityFramework Reverse POCO Generator.vsix`. That identity is what makes an
existing install *upgrade*. Change either and the 576,893 installs get a stranger sitting alongside the
extension they have, two entries on the marketplace, and two copies of the item template competing on
Add - New Item. So the project is not renamed to `Efrpg.Vsix` or anything else, however tidy that would look
next to `Efrpg.Gui.Core`.

**The Target Framework Monikers (TFMs) are forced, not chosen.** An in-process Visual Studio extension runs inside the VS process, which
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
- [x] Audit every enum setting's trailing comment against the enum's actual members, and lock it with a
      test so it cannot rot again

**Verification:** re-running BuildTT on an unchanged tree rewrites the metadata byte for byte. Confirmed.

### As built

`BuildTT/SettingsMetadata/` writes `EntityFramework.Reverse.POCO.Generator/settings-metadata.v4.json` -
118 settings, 104 of them assigned in `Database.tt`. The VSIX links both metadata files rather than copying
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

**The help text is harvested at build time, from this repository, never from a user's file.**
`SettingsMetadataWriter` reads this repo's `Database.tt` - itself generated by BuildTT from the footer in
`BuildTT/BuildTT.cs` - and `Generator/Settings.cs`. It runs when you run BuildTT, before packaging, and the
result is a static file at the VSIX root. A user's `Database.tt` is a *copy* extracted from `efrpoco.zip`;
stripping every comment from it cannot change the metadata, because the two files never meet. **The GUI must
keep it that way: never read tooltip text out of the file being edited.** Phase 3 takes only *values* from the
user's `.tt` and everything else from the metadata, and that must not be "improved" later.

**What the comments could not be trusted about was accuracy, and that was worth auditing.** Three defects, all
shipping to users in `Database.tt` and the `.ttinclude` long before the GUI existed:

- `ForeignKeyNamingStrategy` read *"Please use Legacy for now, Latest (not yet ready)"* when its members were
  `Current` and `Beta` - two values that did not exist, and neither of the two that did
- `TemplateType` listed `EfCore8-10, Ef6, FileBasedCore8-10` and silently omitted `FileBasedEf6`
- `ElementsToGenerate`, `OnConfiguration`, `IncludeComments`, `IncludeExtendedPropertyComments` and
  `GenerationLanguage` listed no values at all, so half the enums documented nothing

All enum settings now name every member, and
`SettingsMetadataTests.Metadata_EveryEnumSettingNamesAllItsMembersInTheHelpText` fails if one stops. It
understands the `EfCore8-10` shorthand so the template stays readable.

**Two of those were fixed in the code, not in the comment.**

***`Settings.ForeignKeyNamingStrategy` is deleted.*** Chasing the wrong comment found `Beta` mapped to a
strategy whose own header said *"Not complete"* and whose body was two `// todo`s, so the setting chose between
one real behaviour and an unfinished one. Gone: the enum, the setting, `LatestForeignKeyNamingStrategy`,
`ForeignKeyNamingStrategyFactory`, `IForeignKeyNamingStrategy` and `BaseForeignKeyNamingStrategy` - together
with the dead `IDbContextFilter` that base class held but never read. **`LegacyForeignKeyNamingStrategy` is
gone too**; its body carried over unchanged into `Generator/ForeignKeyNaming.cs`, which `Table` constructs
directly. Nothing is called Legacy or Latest any more, because there is nothing left to be legacy *to* - the
unit test became `ForeignKeyNames` and a stray `// Legacy` banner over the SQL Server test cases went with it.
The 31 comparison files in `Generator.Tests.Integration/TestComparison/` all carried the same constant
`_FkCurrent` suffix, so it distinguished nothing; they were renamed with it dropped and the format strings in
`SingleDatabaseTestBase.cs` follow. The setting was never in `Database.tt`, so only a user who copied it out of
the `.ttinclude` is affected. The v3 metadata still records it, correctly - that is frozen history.

***`[Flags]` is off `CommentsStyle`.*** Every use in the generator is `==` or `!=`; `HasFlag` appears on
`Elements` only. The metadata had therefore been reporting `isFlags: true`, so the GUI would have rendered
checkboxes, and a user selecting `InSummaryBlock | AtEndOfField` would get value 3 - matching no `==` branch,
passing every `!= None` guard, and silently generating no comments. Three mutually exclusive styles are not a
bit set. Removing the attribute is behaviourally free, because `[Flags]` never gated the `|` operator - it only
affects `ToString()` - so no existing `.tt` stops compiling. `Elements` keeps it; that one genuinely is a flags
enum.

**Left out deliberately:** `FilterSettings.Include*` is not in the metadata. Phase 2's table / view / stored
procedure checkboxes map onto those five flags, so that is the moment to add a second array to this file.

**Why two files.** `Settings.FileManagerType` is valid in v3 and poison in v4 - showing it for a v4 file
would make the GUI write code that does not compile. The metadata is what keeps the two apart.

---

## Phase 1 - VSIX skeleton and the tool gate

De-risks the plumbing before any UI depends on it.

- [x] Add `Community.VisualStudio.Toolkit.17` and `Microsoft.VSSDK.BuildTools` to the VSIX project
- [x] Convert the VSIX from item-template-only to carrying a `ToolkitPackage`
- [x] Add the `Microsoft.VisualStudio.VsPackage` asset to `source.extension.vsixmanifest`
      **via `BuildTT/VersionSetter.cs:UpdateVsixManifest`, not by hand** - that method writes the file
      wholesale, so a hand edit is deleted on the next BuildTT run
- [x] Build `EfrpgToolGate` in `Efrpg.Gui.Core`:
  - [x] Locate `efrpg` on PATH, and at `%USERPROFILE%\.dotnet\tools\efrpg.exe` as a fallback
  - [x] Run `efrpg --version`, parse the package version and the **wire format schema version**
  - [x] Compare the schema version against `EfrpgResultXmlReader.RequiredSchemaVersion`
  - [x] Check `dotnet --version` - `dotnet tool install` needs the **SDK**, not just the runtime
  - [x] Install: `dotnet tool install -g Efrpg`
  - [x] Update: `dotnet tool update -g Efrpg`
  - [x] Surface stderr verbatim on failure - do not swallow it
- [x] Unit test `EfrpgToolGate` against a fake process runner: missing, too old, current, no SDK, no network
- [ ] Gate dialog with three choices: **Install/Update**, **Copy command**, **Continue anyway**
      (partly done: the wizard shows the problem and offers Continue anyway; Install/Update and Copy
      are still buttons to add)
- [x] Always display the exact command next to the button

### Chunk A as built - the gate

`Efrpg.Gui.Core` (netstandard2.0) and `Efrpg.Gui.Core.Tests` (net10.0) exist and are in the solution. 13 tests
against a hand-rolled `FakeProcessRunner`; nothing touches the machine's real tool install.

`IProcessRunner` is the only seam, and **"failed to start" is a result rather than an exception** - a missing
tool surfaces as a `Win32Exception` from `Process.Start`, which is the very thing the gate exists to detect, so
it is information, not a fault. Everything is `async`: `dotnet tool install` can take the best part of a minute
against a slow feed, and this is called from a dialog on the VS UI thread. Output is drained through the
`OutputDataReceived` events, for the same reason `EfrpgToolRunner` drains on two threads - a child that fills
the stdout pipe while the parent blocks on stderr deadlocks, and neither side times out.

**The schema floor is duplicated, and guarded rather than shared.** `EfrpgResultXmlReader` must stay plain
source under `Generator/` so BuildTT can concatenate it, which pins it to net48; `Efrpg.Gui.Core` is
netstandard2.0 so the net48 VSIX and a modern test project can both consume it, and netstandard cannot
reference net48. So each holds a copy and `Generator.Tests.Unit/ToolGateSchemaFloorTests.cs` fails if they
diverge. Verified by bumping one and watching it fail - drift is otherwise silent, and would leave the gate
approving a tool the reader then refuses.

Real `efrpg --version` output, which the tests use verbatim: `efrpg 1.0.1` then
`wire format schema version 1`, on stdout, exit 0. A tool predating the handshake prints no schema line, which
parses as 0 and is correctly rejected - the same reading the reader gives a payload with no `schemaVersion`
attribute.

`EfrpgToolStatus.IsOnPath` is the PATH trap made explicit: when the tool is found only via the fallback it is
on disk but not on the PATH this VS process inherited, so the bare-name resolution in `EfrpgToolRunner` will
still fail. Phase 2 reads that flag to decide whether to invoke by full path and ask for a restart.

**Still to do in Phase 1:** everything touching the VSIX - the two packages, the `ToolkitPackage`, the
`VsPackage` asset via `VersionSetter`, and the gate dialog.

### Menus need `RegisterWithCodebase`, and this cost seven attempts

A menu command - first on the Tools menu, then on the item context menu - did not appear in Visual Studio 2026,
through many rebuilds. Everything checkable was correct: the package registered, the pkgdef declared
`Menus.ctmenu`, the `.cto` compiled with 0 errors, the resource sat in `VSPackage.resources` under the right
key, and **nothing was logged anywhere** - not in the build, not in ActivityLog.xml.

**The fix is one MSBuild property:**

```xml
<RegisterWithCodebase>true</RegisterWithCodebase>
```

Without it `CreatePkgDef` emits `"Assembly"="<name>, Version=..., PublicKeyToken=null"`, which tells Visual
Studio to resolve the package assembly **by name** from the GAC or the probing path. This assembly is unsigned
and is not in the GAC, so VS cannot load it, cannot read the managed `Menus.ctmenu` resource out of it, and
draws no menu. It never reaches a load *failure*, so there is nothing to log - the merge simply finds no data.
With the property set the pkgdef says `"CodeBase"="$PackageFolder$\....dll"` and the menu appears.

**How it was actually found, after six wrong answers reasoned from documentation:** diffing our deployed pkgdef
against a *working* extension already installed on the same machine, in
`%LOCALAPPDATA%\Microsoft\VisualStudio8.0_<hive>\Extensions`. One line differed. **When VSIX plumbing
misbehaves, compare against something that works on the same box before reading any more docs.**

Two things were fixed along the way and are still required:

- `VSPackage.resx` marked `MergeWithCTO`. Without it the SDK writes the command table into a placeholder called
  `_EmptyResource.resources`, and `ProvideMenuResource` only looks in `VSPackage.resources`. Also silent.
- `<Extern href="vsshlids.h" />`, **not** `vsshell.h`. `vsshlids.h` is the plain menu-ID header and compiles
  with only the VSSDK include path. `vsshell.h` is the COM interface header and pulls in Windows SDK headers
  that need the C++ workload, giving `VSCT1118: Unable to locate rpcndr.h`. An earlier attempt used the wrong
  one, hit that, and deleted both `Extern` elements rather than correcting it.

`Microsoft.VSSDK.BuildTools` is referenced, as EF Core Power Tools does. It was not what fixed the header
error, but it supplies the VSCT compiler rather than depending on the Visual Studio install.

### IWizard works, and needs one non-obvious asset

`ReversePocoWizard : IWizard` is reached from `<WizardExtension>` in `MyTemplate.vstemplate`. It needs no
package, no pkgdef and no command table, which is why it was tried when the menu stalled.

**Shipping the assembly inside the VSIX is not enough.** The template engine resolves the wizard by assembly
name, and without

```xml
<Asset Type="Microsoft.VisualStudio.Assembly" d:Source="Project" d:ProjectName="%CurrentProject%"
       Path="|%CurrentProject%|" AssemblyName="|%CurrentProject%;AssemblyName|" />
```

the user gets *"this template attempted to load component assembly ..."* on Add - New Item. This is the
same root cause as the missing menu above - an unsigned assembly that cannot be resolved by name - just
reached through the template engine rather than the package loader. The `<Assembly>`
element in the vstemplate embeds `AssemblyVersion`, so both it and this asset are generated by `VersionSetter`
from `version.txt`; a hand-written copy rots at the next version bump and the failure is obscure.

**Verified working**: with the tool hidden, Add - New Item shows the gate's message and the exact install
command, and offers to continue. With the tool present and current the wizard runs and shows nothing, which is
correct and indistinguishable from not running - worth remembering when testing.

**Check the schema version, not the package version.** The schema version is what the template actually
floor-checks, so the gate becomes the same test the generator does - surfaced as a friendly dialog instead
of a comment in a broken output file.

**"Copy command" is not a nicety.** It is the escape hatch for developers behind a proxy, on an internal
NuGet feed, or without permission to install.

**Deliverable:** a command that reports tool status. **Done** - Tools -> "Reverse POCO: Check efrpg tool...".

### Chunk B as built - the VSIX skeleton

`EfrpgPackage : ToolkitPackage` and `CheckEfrpgToolCommand` now ship in the existing VSIX, which also carries
`Efrpg.Gui.Core.dll`. The package is background loaded and does nothing until the command asks it to, so an
unused extension costs nothing at startup.

Three plan assumptions turned out to be wrong, all found by building it:

- **`Microsoft.VSSDK.BuildTools` is not needed and does not help.** It was added, changed nothing, and was
  removed. `VSCommandTable.vsct` failed with `VSCT1118: Unable to locate 'rpcndr.h'` because
  `<Extern href="vsshell.h" />` pulls in *Windows SDK* C headers that are only present with the C++ workload -
  not VSSDK ones, so no VSSDK package can supply them. The fix is to declare the two shell symbols the file
  actually uses (`guidSHLMainMenu` `{d309f791-903f-11d0-9efc-00a0c911004f}` and `IDM_VS_MENU_TOOLS` `0x0085`,
  both read out of `VSSDK\VisualStudioIntegration\Common\inc`) and drop both `Extern` elements. That also
  makes the build work on a machine with only the managed workloads, which is what CI would have.
- **`VersionSetter` was dead code, and is now live.** `SetVersions()` was commented out in
  `BuildTT/Program.cs`, so adding the asset there alone would have shipped nothing. It was therefore added to
  `UpdateVsixManifest` *and* to the checked-in `source.extension.vsixmanifest`, and the two compared line by
  line. `SetVersions()` has since been re-enabled, so BuildTT now also stamps the vsixmanifest and the
  vstemplate and rebuilds `efrpoco.zip` in all four locations. That regeneration overwrites the manifest
  wholesale - and it kept the `VsPackage` asset and the open version ranges precisely because they were put
  into the generator rather than only into its output. Hand-editing the manifest alone would have been silently
  undone the moment that line was uncommented.
- **The three VSIX flags had to flip.** `GeneratePkgDefFile`, `IncludeAssemblyInVSIXContainer` and
  `CopyBuildOutputToOutputDirectory` were all `false` while this was item-template-only. An old-style project
  also needs `<RestoreProjectStyle>PackageReference</RestoreProjectStyle>` before `PackageReference` works.

**The 17.x installation targets lost their upper bound.** They were `[17.0,18.0)` and are now `[17.0,)`, in
the manifest and in `VersionSetter` alike. From Visual Studio 2026 onward compatibility is decided by *API
version*, not product version: VS 2026 supports API 17.x, reads only the lower bound of the range and
**ignores the upper bound entirely** - which is why the old range installed into 18.9 perfectly happily. The
open range is what VS 2026 emits for new extensions, VS 2022 still uses the old product-range model and is
satisfied by it too, and it means this never needs touching again for a new major release. The `[15.0,17.0)`
entries for VS 2017/2019 predate all of this and are untouched, as is `<ProductArchitecture>amd64</...>`.

Worth being clear about what this does *not* buy: the manifest range is no longer the gate for a future VS 19,
API version support is. Nothing written here can make a future release load an extension whose APIs it has
dropped.

**What is verified and what is not.** The VSIX builds, and the packaged manifest carries
`<Asset Type="Microsoft.VisualStudio.VsPackage" Path="...pkgdef" />` with a pkgdef that registers the package
class and `Menus.ctmenu`. **Nothing here proves the package loads in VS 2026.** An item-template-only VSIX
installs into 18.x under the existing `[17.0,18.0)` target - that is known - but a package-carrying VSIX binds
to the VS 17 shell assemblies, and that is tested at load, not at install. Installing the built VSIX and
running the Tools command is the outstanding manual step, and it should happen before any more UI is built on
top.

---

## A constraint that shapes every phase - the `**TODO**` placeholder

A `.tt` added from the item template ships with the database name unset:

```
Settings.ConnectionString = "Data Source=(local);Initial Catalog=**TODO**;Integrated Security=True;..."
```

`efrpg` checks for that literal and returns an error without attempting a connection - *"the connection string
still contains the placeholder \*\*TODO\*\*"*. **So a brand-new `.tt` cannot be reverse engineered, by
construction.** Any code path that reaches for schema before the user has supplied a real connection string is
work that can only end in an error dialog.

What follows for each phase:

- **Phase 1** is unaffected: `efrpg --version` takes no connection string. Do not "improve" the gate's liveness
  check into a schema read - it would fail on exactly the machines the gate is meant to reassure.
- **Phase 2's ordering is mandatory, not stylistic.** Gate, then connection dialog, then test connection, and
  only then shell out for schema. The checkbox tree cannot be populated before that, so the wizard cannot open
  on the table picker.
- **Phase 3 must check before offering anything schema-backed.** The settings editor can be invoked on a file
  the user has never configured, so `**TODO**` is also the reliable test for "unconfigured".

`Generator.Init` carries its own `**TODO**` guard at `Generator/Generators/Generator.cs:107`, but the tool now
fails first and the template only constructs the generator when the tool succeeded, so that guard no longer
fires through the template path. Left alone rather than removed - it still protects the direct-construction
route the unit tests use.

---

## Phase 2 - wizard on Add → New Item

**This is the phase that addresses the acquisition collapse.** It needs no Roslyn at all. See
[Sequencing](#sequencing) - if effort has to be cut, cut Phase 3, never this.

- [ ] `ReversePocoWizard : Microsoft.VisualStudio.TemplateWizard.IWizard`
- [ ] Wire it into `MyTemplate.vstemplate` via `<WizardExtension>` -
      **through `BuildTT/VersionSetter.cs:UpdateVstemplate`**, which also regenerates that file wholesale
- [x] `RunStarted`: tool gate → connection dialog, then re-run the T4
- [x] Shell out to `efrpg --secrets-stdin` for the schema (same binary and wire format the template uses)
- [x] **Test connection** button, reporting what was found rather than a bare OK
- [ ] Checkbox tree: tables, views, stored procedures
- [x] Fields: database type, template type, connection string, DbContext name - see below
- [x] Fields: namespace
- [x] Write the answers into the generated `.tt` - see below, this is *not* `replacementsDictionary`
- [x] ~~Flip `ReplaceParameters` to `true`~~ - deliberately not done, see below
- [x] `Database.tt` confirmed to contain **zero `$` characters**, so it stays token-safe if this is ever
      revisited
- [x] `throw new WizardBackoutException()` on cancel so VS cleans up the half-created item
- [x] Reopen the dialog afterwards from the `.tt` file's right-click menu - **Reverse POCO: Settings...**
- [ ] After a successful install, invoke the tool **by full path** for the wizard's own schema read, and
      tell the user to restart Visual Studio before saving the `.tt`

### Email capture and first-run telemetry

**The wizard is the only place in the product where a new user can be reached.** Lifetime, 2,582 website
accounts have produced 653 paying customers - **25.3%** - but in the 91 days to 31 August 2026 there were
1,523 downloads and **6 registrations**. The trial is not registration-gated (the 10-table limit is enforced
in the generated code, not by a licence file anyone has to come and fetch), so roughly 1,500 leads a quarter
are discarded: no email, no follow-up, no idea they existed.

- [ ] Optional email capture step - honest framing, e.g. *"Get a trial licence key and release notes"*
- [ ] Never block generation on it. A skipped email must not degrade the wizard
- [ ] Post to the ReversePOCO site so the address lands in the same funnel as a registration
- [ ] First-run telemetry, opt-out, no connection strings and no schema:
  - [ ] wizard started
  - [ ] connection test succeeded / failed (with the dialect, not the connection string)
  - [ ] tool gate outcome - already present, installed by us, declined
  - [ ] wizard completed and a `.tt` written
- [ ] Publish what is collected, and honour a decline permanently

**Without this the wizard improves the experience but tells you nothing about whether it worked.** Installs,
first-run completions and connection successes are the three numbers that would say whether the acquisition
problem is being fixed, and none of them exist today.

**Also fix outside this plan:** `AspNetUsers.CreatedAtUtc` is NULL for 2,576 of 2,582 rows, so registration
history cannot be measured at all before mid-2026.

### The answers are written into the .tt, not substituted as tokens

The plan called for `ReplaceParameters="true"` and `$token$` substitution. That is not what was built, and
deliberately so: `EntityFramework.Reverse.POCO.Generator/Database.tt` is itself executed in this repository -
there is a `Database.cs` beside it - so putting tokens in the master would break it, and generating a second
tokenised copy for the zip would be exactly the parallel-copy problem avoided everywhere else here.

Instead `ProjectItemFinishedGenerating` records the path of the added `.tt` and `RunFinished` rewrites it
through `TemplateSettingsFile`. That also reuses the anchored-edit approach Phase 3 needs, rather than
inventing a second mechanism that Phase 3 would then replace.

`TemplateSettingsFile` lives in `Efrpg.Gui.Core` and is unit tested, including **against the real shipped
`Database.tt`** rather than only a fixture - so if BuildTT ever changes how those settings are emitted, the
tests fail before a user meets a mangled template. It refuses anything that is not a single-line string
literal or a single-line `Type.Member` enum assignment. Everything else is refused rather than mangled: a
commented-out setting means "not this one", and a combination of flags, a method call or an expression would
be turned into something that does not compile. Backslashes are escaped, because `Data Source=.\SQLEXPRESS` is
the common case and an unescaped one produces a `.tt` that fails far from where it was written.

Skipping the dialog is always allowed. The template with the placeholder still in it is a working starting
point, and a wizard that will not let you out is worse than one that asks nothing.

### The database type is chosen before the connection string, not after

Oracle, PostgreSQL, MySQL and SQL Server share no connection-string keywords at all - Oracle wants
`Data Source=host:port/service`, PostgreSQL wants `Server=;Port=;Database=`. An Oracle user handed a SQL Server
connection string is no better off than one handed the placeholder, so the database dropdown sits above the
connection box and fills it with the right skeleton. Every default carries `**TODO**` wherever the user has to
supply something, and OK stays disabled until all of them are gone.

Switching database swaps the connection string only while the box still holds an untouched default. Picking the
database is almost always the first thing a non-SQL-Server user does, so the swap happens exactly when it is
wanted; someone who has typed a connection string and then switches keeps their text rather than losing it.

**`TemplateType` and `GeneratorType` are written together, never separately.** The generator keeps the two
settings independent - nothing derives one from the other - so an `Ef6` template left with the default `EfCore`
generator produces code that does not compile, a long way from the dialog that caused it. `TemplateTarget` holds
the pairing and `TemplateTargetTests` checks every one of them.

`DatabaseTarget` and `TemplateTarget` identify their values by **enum member name**, not by `Efrpg.DatabaseType`
and `Efrpg.Templates.TemplateType` themselves: those live in the net48 `Generator` project and `Efrpg.Gui.Core`
is netstandard2.0, so it cannot reference them. The name is what gets written into the `.tt` in any case. Drift
is caught by testing both lists against the `enumMembers` recorded in **`settings-metadata.v4.json`** - the
Phase 0 artefact, doing a second job. A database or template type added to the generator and left out of the
dropdown fails the build rather than quietly going missing from the UI.

**The T4 runs before the wizard finishes, so it has to be re-run.** Adding a `.tt` to a project fires its
custom tool immediately - well before `RunFinished` - so the first generated `.cs` is always the efrpg tool's
*"the connection string still contains \*\*TODO\*\*"* error. Writing the real connection string afterwards fixes
the `.tt` but leaves that error sitting in the generated output, which is exactly the confusing first impression
the wizard exists to remove. Re-running rather than suppressing the first pass: there is no supported way to
stop the custom tool firing on add, and a second pass is cheap next to the schema read it performs.

**Re-running has to go through the editor buffer, not the file on disk.** `TemplateFileUpdater` is the whole
answer and the reason it exists. The T4 custom tool is an `IVsSingleFileGenerator`, and Visual Studio hands it
the contents of the **editor buffer**, not the file. Visual Studio opens the `.tt` as soon as it is added, so
writing straight to disk and calling `VSProjectItem.RunCustomTool()` regenerates from the stale text the buffer
still holds - the `.tt` on disk is correct and the generated `.cs` still carries the placeholder error, which
looks exactly like the file never having been written. That was shipped in 4.0.6 and 4.0.8 and is fixed in
4.0.9.

So: when the document is open, replace the buffer and `Document.Save()`. The save runs the generator by itself,
and it is the same path the user takes by hand, which is the one Visual Studio supports best. Only when the
document is *not* open is the file written directly, and then `RunCustomTool` is asked to run - falling back to
reassigning the `CustomTool` property, because `VSProjectItem` is not available in every project system.

### Testing the connection runs the real thing

**Test connection** invokes the same `efrpg` binary, with the same flags and the same wire format the T4 uses on
save. Opening a `SqlConnection` in the dialog instead would prove something subtly different from what happens at
generation time, which is what *"it tested fine but generation fails"* is made of. It reports the object counts -
*24 tables, 3 views, 15 stored procedures* - because the question behind the question is almost always "did I point
it at the right database", which a bare "OK" does not answer.

The pieces, all in `Efrpg.Gui.Core` and all unit tested against the captured wire payload in
`Generator.Tests.Unit/WireContract/`:

- `IProcessRunner` gained a `standardInput` parameter. **The connection string goes over stdin, never on the
  command line** - command lines are captured by process listings and by command-line audit logging (Sysmon event
  1, EDR telemetry, ETW), which forwards them to a SIEM and to everyone with access to one. A test asserts the
  database name appears in stdin and *not* in the arguments.
- `SecretsXml` is **linked** into `Efrpg.Gui.Core` from `Generator/Readers/`, not copied. It has to produce
  byte-identical XML to what the tool parses, and a second copy would be free to drift; compiling the one source
  file into both assemblies is the only way to share it across the net48/netstandard2.0 line.
- `DatabaseSchema.Parse` is a **name extractor, not a second `EfrpgResultXmlReader`**. It reads four attributes and
  ignores everything else - which is also what lets a newer tool serve an older GUI. The real reader builds the
  generator's whole object model and has to stay under `Generator/` for BuildTT to concatenate.
- The executable path comes from `EfrpgToolGate`, never resolved again. That is the PATH trap below, already
  solved once.

Every row under `Tables` in the payload is a *column*, so the same table appears once per column and is collapsed;
synonyms are skipped, being aliases for something already listed. That is the data the object picker needs, so the
picker is now a UI job rather than a plumbing one.

### Namespace is not a string setting

`Settings.Namespace` ships as the bare identifier `DefaultNamespace` and becomes a quoted string only once
somebody overrides it, so neither the string setter nor the enum setter can touch it. `TrySetExpression` replaces
the whole right-hand side, and `TemplateConfiguration` validates the namespace against dotted identifiers before
anything is written - what goes there becomes C# in the `.tt`, and an invalid value is left alone rather than
written and broken. Clearing the field puts `DefaultNamespace` back.

### The dialog is reachable again after the file exists

The wizard runs once. Somebody who pressed Skip, mistyped a database name, or wants to point the same template
at a different server had no route back to it at all - the only option was to find the right line in the `.tt`
by hand, which is the thing the GUI exists to avoid. **Reverse POCO: Settings...** on the `.tt` file's
right-click menu opens the same dialog on what the file already says.

That is why `TemplateSettingsFile` reads as well as writes, and why `TemplateConfiguration` lives in
`Efrpg.Gui.Core` rather than in the dialog: reading the current values first is what stops OK replacing a
user's own connection string with the SQL Server default. `TemplateConfigurationTests` asserts that reading a
template and writing it straight back leaves it **byte for byte identical**.

Both commands are `DynamicVisibility` + `DefaultInvisible` and their `BeforeQueryStatus` shows them only for a
`.tt`. The group is parented to `IDM_VS_CTXT_ITEMNODE`, which Visual Studio draws for *every* file in the
solution, so without that they would clutter the right-click menu of every file in every project.

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

- [x] Its own `.vsct` command, offered only when a v3 file is selected
- [ ] Prompt unprompted the first time a v3 template is opened after the extension updates
- [x] Change the include directive to `EF.Reverse.POCO.v4.ttinclude`
- [x] Delete the `Settings.FileManagerType` assignment
- [x] Delete the `Settings.DatabaseReaderPlugin` assignment - same class of breakage, and it was missing from
      this list until a v3.14.1 file was actually diffed against v4
- [x] Rewrite `if (Settings.GenerateSeparateFiles && Settings.FileManagerType == FileManagerType.EfCore)`
      to `if (Settings.GenerateSeparateFiles)`
- [x] Replace the entry-point block with the `EfrpgToolRunner.ReadDatabase` version
- [x] Replace `DatabaseReader.CleanUp` with `NamingHelper.CleanUp` if present
- [x] Show a diff preview and require confirmation before writing
- [x] **If the file does not match the expected shape exactly, refuse** and link to the upgrade guide

### How it was built

`TemplateUpgrade` in `Efrpg.Gui.Core`, tested against a **real v3.14.1 `Database.tt`** recovered from the commit
before database reading moved into the tool (`Efrpg.Gui.Core.Tests/Fixtures/`), not a reconstruction of one.

Three things are worth knowing:

- **The entry point is compared by statement, not by text.** Blank lines and comments are stripped from the tail
  before it is matched, so a file carrying its own commented-out notes inside that block still upgrades - which is
  exactly what tripped the first pass when the 24 in-repo templates were migrated - while a genuinely restructured
  one is refused.
- **A leftover reference to a removed name is a refusal.** After the edits, any remaining `FileManagerType`,
  `DatabaseReaderPlugin` or `DatabaseReader.` means the file mentions them somewhere this does not know how to
  change, and the result would not compile. Those checks run only when nothing else already failed, so a refused
  entry point does not also report the `FileManagerFactory.GetFileManagerType()` still inside it.
- **`V4EntryPoint` is guarded against BuildTT.** A test asserts the constant is byte for byte the tail of the
  shipped `Database.tt`, so a change to BuildTT's footer fails the build rather than leaving the upgrade emitting
  last year's code.

Line endings are preserved: a CRLF template stays CRLF and an LF one stays LF, because a whole-file line ending
change shows up as every line differing in the user's next commit.

**The full v3 to v4 delta, from diffing a real v3.14.1 `Database.tt`.** 18 blocks differ, and they split into
two groups that must be treated differently.

*Six edits that are required, because without them the template does not compile or does not run:*

| Where | Change |
|---|---|
| line 1 | include directive -> `EF.Reverse.POCO.v4.ttinclude` |
| ~17 | delete `Settings.FileManagerType` |
| ~86 | delete `Settings.DatabaseReaderPlugin` |
| ~70 | `if (GenerateSeparateFiles && FileManagerType == FileManagerType.EfCore)` -> `if (GenerateSeparateFiles)` |
| ~656 | `DatabaseReader.CleanUp(fkName)` -> `NamingHelper.CleanUp(fkName)` |
| ~812-823 | the entry-point block -> the `EfrpgToolRunner.ReadDatabase` version |

*Twelve blocks that are cosmetic and must **not** be forced:* the version header, two `v3.ttinclude` mentions
inside comment prose, and the trailing-comment improvements made in v4 (`DatabaseType`, `TemplateType`,
`ElementsToGenerate`, `OnConfiguration`, `IncludeComments`, `IncludeExtendedPropertyComments`,
`GenerationLanguage`). A customer's file will already differ here, and rewriting comments they may have edited
themselves is exactly the over-reach the refusal rule exists to prevent.

**The entry-point block is a replacement, not a patch.** In v3 `var fileManagement = new FileManagementService(outer);`
sits *after* the commented-out machine.config lines; in v4 it moves *before* the try block. So the whole span
has to go, which is the part most likely to vary between customer files and the most likely thing to refuse on.

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
