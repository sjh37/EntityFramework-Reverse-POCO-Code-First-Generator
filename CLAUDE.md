# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Project Does

This is the **EntityFramework Reverse POCO Code First Generator** (EFRPG) — a Visual Studio extension and T4 template system that reverse-engineers an existing database and generates EF Code First POCO classes, DbContext, interface, configuration mappings, enumerations, fake DbContext (for unit testing), and stored procedure/TVF callers.

The generator is distributed as a VSIX (Visual Studio Extension) containing a T4 item template. Users add a `Database.tt` file to their project; saving it triggers the generator.

## Solution Structure

- **`Generator/`** — Core library (`Efrpg` namespace, `netstandard2.0`). All generation logic lives here.
- **`BuildTT/`** — Console app that compiles the `Generator/` C# project into a single `.ttinclude` file (`EF.Reverse.POCO.v3.ttinclude`) that ships with the extension.
- **`EntityFramework.Reverse.POCO.Generator/`** — The `.ttinclude` file output from BuildTT, plus the `Database.tt` template that users add to their projects.
- **`EntityFramework Reverse POCO Generator/`** — VSIX packaging project.
- **`Generator.Tests.Unit/`** — Unit tests using NUnit (targets `net48`, requires SQL Server LocalDB for some tests).
- **`Generator.Tests.Unit.EFCore/`** — EF Core-specific unit tests (targets `net8.0`).
- **`Generator.Tests.Integration/`** — Integration tests that actually connect to SQL Server and write generated files to `~/Documents`.
- **`Generator.Tests.Common/`** — Shared test constants and helpers (`netstandard2.0`).
- **`Tester.Integration.EFCore8/`, `EFCore9/`, `Ef6/`** — Projects that consume the generated output and verify it compiles/runs correctly.
- **`Tester.Repository/`**, **`Tester.BusinessLogic.EfCore/`** — Support projects for integration testing.
- **`_File based templates/`** — Mustache template files for the `FileBased` template mode.

## Key Architecture

### Generation Pipeline

1. **Settings** (`Generator/Settings.cs`) — static class holding all configuration. The `.tt` file sets these before running.
2. **DatabaseReader** (`Generator/Readers/`) — reads schema from the database. `DatabaseReaderFactory` selects the reader based on `Settings.DatabaseType` (SqlServer, PostgreSQL, SQLite, SqlCe, MySql, Oracle, or Plugin).
3. **Generator** (`Generator/Generators/`) — abstract base class with `GeneratorEf6`, `GeneratorEfCore`, and `GeneratorCustom` implementations. Selected by `GeneratorFactory` based on `Settings.GeneratorType`.
4. **Template** (`Generator/Templates/`) — abstract base class with `TemplateEf6`, `TemplateEfCore8`, and `TemplateFileBased` implementations. Mustache-based string templates. Selected by `TemplateFactory` based on `Settings.TemplateType`.
5. **Filtering** (`Generator/Filtering/`) — `FilterSettings` and `SingleContextFilter`/`MultiContextFilter` control which schemas/tables/columns/stored procs are included.
6. **FileManagement** (`Generator/FileManagement/`) — handles writing output files; different implementations for EF Core projects, VS4.x projects, and null (test mode).

### The `.ttinclude` Build Process

`BuildTT` concatenates all C# files from `Generator/` into one large `EF.Reverse.POCO.v3.ttinclude` file. **Never edit the `.ttinclude` directly** — edit the source files in `Generator/` and run `BuildTT` to regenerate.

Version is controlled by `BuildTT/version.txt`.

### Template Types

- `TemplateType.EfCore9` / `EfCore8` → uses `TemplateEfCore8` class with Mustache templates inline in C#
- `TemplateType.Ef6` → uses `TemplateEf6` class
- `TemplateType.FileBasedCore8/9` / `FileBasedEf6` → uses `TemplateFileBased` which reads from `Settings.TemplateFolder` (Mustache `.mustache` files)

### Multi-Context Support

When `Settings.GenerateSingleDbContext = false`, a plugin class implementing `IMultiDbContextSettingsPlugin` drives generation of multiple `DbContext` classes from one database.

## Build Commands

```bash
# Build the solution
dotnet build EF.Reverse.POCO.GeneratorV3.sln

# Run unit tests (no DB required for most)
dotnet test Generator.Tests.Unit/Generator.Tests.Unit.csproj

# Run EF Core unit tests
dotnet test Generator.Tests.Unit.EFCore/Generator.Tests.Unit.EFCore.csproj

# Run integration tests (requires SQL Server with EfrpgTest and Northwind databases)
dotnet test Generator.Tests.Integration/Generator.Tests.Integration.csproj --filter "Category=Integration"

# Run a single test by name
dotnet test Generator.Tests.Unit/Generator.Tests.Unit.csproj --filter "FullyQualifiedName~PluralisationTests"
```

## Packaging

`pack.bat` packages the VSIX item template (requires 7-Zip at `C:\Program Files\7-Zip\7z.exe`). Run after building if you need to update the VSIX item template zip.

## Testing Patterns

- Unit tests use `FakeDatabaseReader` to avoid real DB connections.
- Integration tests connect to `(local)` SQL Server using `Integrated Security=True`. Test databases are `EfrpgTest` and `Northwind` (SQL scripts in `TestDatabases/` and `EfrpgTest.sql`).
- Integration tests write generated `.cs` files to `~/Documents` sub-folders like `.V3TestE8`.
- The `Tester.Integration.*` projects compile the generated output to verify it.
- Test categories: `Constants.DbType.SqlServer`, `Constants.DbType.PostgreSQL`, `Constants.Integration`.
