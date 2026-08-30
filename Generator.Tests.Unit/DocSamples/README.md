# DocSamples - the wiki's code examples

Every "here is what the generated code looks like" block on the wiki is produced by this folder and checked
against the wiki by a test. Nothing on those pages is typed by hand.

## Why this exists

The wiki used to be written by reading the generator source and describing it. An audit in August 2026 found,
in one pass:

- `Settings.ForeignKeyNamingStrategy` documented in full, with an enum and two code examples. It has never
  existed.
- `Settings.ApplyColumnCustomizations` cited as the helper to call from `UpdateColumn`. Also never existed.
- Three defaults documented as the `Settings.cs` field initialiser when `Database.tt` assigns something else,
  including `DisableGeographyTypes`, so the wiki said spatial types were on when they ship off.
- A `Settings.ColumnIdentity` example that emits `UseIdentityColumn()` unconditionally, which does not compile
  on MySQL and silently disables `Settings.HiLoSequences`.

None of it was careless. Prose describing code drifts from the code, and nothing was watching. So the examples
are now generated and re-checked, and the wiki cannot quietly stop being true.

## How it fits together

```
DocSampleSchema.xml          A hand-written efrpg payload: Category, Product, sales.Order
DocSampleExtrasSchema.xml    A second one: many-to-many, a view, stored procs, rowversion, extended property
        |
        v
DocSampleRunner              Resets Settings to the shipped Database.tt values, applies ONE change,
                             runs the generator, returns the output. No database involved.
        |
        v
DocSampleExtractor           Cuts a readable snippet out of a ~280 line file
        |
        v
DocSampleCatalogue           Names each snippet: "UsePrivateSetterForComputedColumns/true" -> a func
        |
        +--> DocSampleTests.Write_all_samples_to_disk    the authoring step: writes them all to a temp folder
        |
        +--> WikiSnippetDriftTests                        regenerates each one and compares it to the wiki
```

## Adding an example to a wiki page

1. **Add an entry to `DocSampleCatalogue.Samples()`.** The key is `SettingName/variant`. Two entries sharing a
   `SettingName` prefix are treated as alternatives and must produce different output - that check exists so a
   page cannot show two identical blocks and claim a difference.

   ```csharp
   { "TrimCharFields/true", () => ProductConfiguration(() => Settings.TrimCharFields = true) },
   ```

   Use the helpers rather than calling the runner directly where one fits: `Product`, `Category`,
   `ProductConfiguration`, `Extras`, `Outline`, `Line`, `Head`, `Regions`, `Section`.

2. **Run the authoring test** and read the output it prints:

   ```
   dotnet build Generator.Tests.Unit/Generator.Tests.Unit.csproj
   dotnet vstest Generator.Tests.Unit/bin/Debug/Generator.Tests.Unit.dll ^
     --TestAdapterPath:packages/NUnit3TestAdapter.6.2.0/build/net462 ^
     --TestCaseFilter:"FullyQualifiedName~Write_all_samples_to_disk"
   ```

   It also writes one file per sample to `%TEMP%\efrpg-doc-samples\output`.

3. **Paste it into the wiki page** under a marker, which is what opts the block into drift checking:

   ~~~markdown
   <!-- docsample: TrimCharFields/true -->
   ```csharp
   ...pasted output...
   ```
   ~~~

   The marker must be on the line immediately above the opening fence. A fenced block with no marker is a
   hand-written example and is not checked.

4. **Run the drift tests.** They must pass before you commit:

   ```
   dotnet vstest Generator.Tests.Unit/bin/Debug/Generator.Tests.Unit.dll ^
     --TestAdapterPath:packages/NUnit3TestAdapter.6.2.0/build/net462 ^
     --TestCaseFilter:"FullyQualifiedName~DocSample|FullyQualifiedName~WikiSnippet"
   ```

## When a drift test fails

`Every_marked_wiki_snippet_matches_freshly_generated_output` failing means one of two things, and it prints
both sides so you can tell which:

- **The generator changed and the change is intended.** Re-run the authoring test and paste the new output.
  The failure did its job.
- **The generator changed and the change is not intended.** You have found a regression in the templates. Fix
  the generator, not the wiki.

`Every_catalogue_sample_is_cited_by_a_wiki_page` failing means a sample is generated that no page shows.
Either add it to a page or delete it - an example nobody reads is an example nobody maintains.

Both tests `Assert.Ignore` when the wiki repository is not checked out beside this one, so they are inert on a
machine with only the generator repo. They expect
`../EntityFramework-Reverse-POCO-Code-First-Generator.wiki`.

## Things that will bite you

**`StaticStateSnapshot` is not optional.** `Settings`, `FilterSettings` and `Inflector` are static and shared
by the whole test run. Generating a sample rewrites most of `Settings`, which made `ForeignKeyTests` pass on
its own and fail in a full run - ten failures with nothing to do with the changed code. Any fixture that calls
`DocSampleRunner` must capture in `[OneTimeSetUp]` and restore in `[OneTimeTearDown]`. Restoring by hand is not
an option: several settings are delegates with real logic, so writing them back would be a second copy of
`Settings.cs` to keep in step. Reflecting over the static fields captures whatever they actually are.

**The runner's defaults are `Database.tt`'s, not `Settings.cs`'s.** They differ for `UseLazyLoading`,
`DisableGeographyTypes`, `UseResharper` and the folder settings. `Database.tt` is what a user gets, so it is
what the wiki documents and what the samples show. Two deliberate departures, both to keep snippets readable:
`AddUnitTestingDbContext` is off (the fake context doubles every snippet) and `UseResharper` is off (six lines
of suppression comments at the top of every file).

**`Settings.Enumerations` must stay null.** A non-empty list makes the generator invoke the `efrpg` tool again
to read enum rows, which needs a live database. Enum member examples cannot be generated here; describe them
instead.

**Single-file mode, except for the folder settings.** `Generate` relies on `GenerateSingleDbContext && !GenerateSeparateFiles`
putting everything in `GeneratedTextTransformation.FileData`, so nothing touches the disk and no audit file is
written. `GenerateFileListing` is the exception: it turns separate files on, generates into a temp folder, and
returns the file names, which is the only way to show what `PocoFolder` and friends do.

**Filters are built when the generator is constructed.** Setting `FilterSettings.IncludeViews` inside a
sample's configure action is too late for the filter objects, so `EnableEverythingOnTheFilters` copies the
values across afterwards. The integration tests do the same thing.

**Snippets are compared with `\n`.** Line endings are normalised on both sides so a snippet does not change
meaning between a Windows checkout and a Linux CI agent.

## The two fixture schemas

Both are hand-written and deliberately small. `DocSampleSchema.sql` and `DocSampleExtrasSchema.sql` are the
same schemas as DDL - that is what the wiki pages show the reader, so keep them in step with the XML.

| Fixture | Contains | For |
|---------|----------|-----|
| `DocSampleSchema.xml` | `dbo.Category`, `dbo.Product`, `sales.Order` | Identity PK, a SQL default, a computed column, nullable and non-nullable strings, a decimal with precision, a foreign key, and a table outside the default schema |
| `DocSampleExtrasSchema.xml` | `Student`, `Course`, `StudentCourse`, `Document`, `ActiveStudent`, two stored procedures | Many-to-many, a view with no primary key, rowversion, an extended property, and stored procedures in and out of the default schema |

The core fixture is the one most pages use, and it is short on purpose: a reader who has understood three
tables once can read every page without re-learning the schema. Resist adding to it. If a setting needs
something else, put it in the extras fixture or add a third.

**This is not `WireContract/EfrpgResult.xml`.** That one is captured tool output and must stay captured - its
job is to prove the tool and the template still agree about every attribute on the wire. These two are written
by hand because their job is to be readable. Do not merge them.

## Related

- `plan-wiki-settings-docs.md` in the repository root - the checklist of which settings still need a page.
  It is temporary and will be deleted when the pages are done; this file is not.
- `WireContractTests.cs` - the other half of the "documentation cannot silently rot" idea, for the wire format
  rather than the wiki.
