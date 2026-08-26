# EFRPG Dotnet Tool - Project Briefing

## What This Is

This is a .NET global tool (`efrpg`) that is part of v4 of the
**EntityFramework Reverse POCO Code First Generator** (EFRPG) project.

The tool's sole job is to connect to a database, reverse-engineer the schema,
and write the raw schema data as XML to stdout. A T4 template in a separate
public repo reads that XML and generates EF Code First POCO classes, DbContext,
mappings, stored procedure callers, etc.

This repo is private. The public repo (T4 template side) is at:
https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator

## Why a Separate Tool

The old architecture embedded the database readers inside the T4 template as a
`.ttinclude` file. This caused two problems:

1. **NuGet packages were unavailable.** T4 runs inside the Visual Studio host
   process (.NET Framework 4.8), so database drivers for Oracle, MySQL,
   PostgreSQL etc. had to be in the GAC. Users had to install them manually.
   As a .NET tool, this project can declare standard NuGet dependencies and
   they restore automatically.

2. **Licensing could be bypassed.** The `.ttinclude` source was visible and
   modifiable. Moving the core logic here makes licensing enforceable.

## Architecture

```
T4 template (public repo)
    |
    | runs as child process
    v
efrpg --database SqlServer --connection "..." [--timeout 600]
    |
    | writes XML to stdout
    v
T4 template parses XML (using System.Xml.Linq / XDocument)
    |
    | generates C# files
    v
User's project
```

### Why XML (not JSON)

T4 templates run in the .NET Framework 4.8 VS host. `System.Xml` and
`System.Xml.Linq` are always available there without any assembly references.
JSON libraries are not. XML encoding handles all special characters in table/
column names (commas, quotes, angle brackets, Unicode) automatically.

## CLI Interface

```
efrpg --database <type> --connection <string> [--timeout <seconds>]
```

| Flag | Short | Required | Default | Description |
|------|-------|----------|---------|-------------|
| `--database` | `-d` | yes | - | SqlServer, PostgreSQL, MySql, SQLite, Oracle |
| `--connection` | `-c` | yes | - | ADO.NET connection string |
| `--timeout` | `-t` | no | 600 | SQL command timeout in seconds |

Exit code 0 = success (XML on stdout). Non-zero = error (message on stderr).

## Database Drivers (NuGet)

Each database type uses its own ADO.NET driver. These are standard NuGet
package references - no GAC required.

| DatabaseType | NuGet Package |
|--------------|---------------|
| SqlServer | `Microsoft.Data.SqlClient` |
| PostgreSQL | `Npgsql` |
| MySql | `MySqlConnector` |
| SQLite | `Microsoft.Data.Sqlite` |
| Oracle | `Oracle.ManagedDataAccess.Core` |

## Code Structure

- `Program.cs` - entry point, parses CLI args, populates `Settings`, invokes reader
- `Settings.cs` - static class holding the three runtime values the readers need
- `DatabaseType.cs` - enum matching the database type names
- `Readers/DatabaseReader.cs` - abstract base class with all the Read* methods
- `Readers/DatabaseReaderFactory.cs` - selects the right reader for the database type
- `Readers/DatabaseProvider.cs` - maps DatabaseType to ADO.NET provider name
- `Readers/*DatabaseReader.cs` - one concrete reader per database
- `Readers/Raw*.cs` - pure data classes (no logic) that the readers populate

### Raw* Classes

These are the data transfer objects between the reader and the T4 template.
The same files exist in both this repo and the public T4 repo (duplicated, not
shared via NuGet - T4 templates cannot use NuGet packages).

| Class | Contains |
|-------|----------|
| `RawTable` | One row per column (table name, column name, types, flags) |
| `RawForeignKey` | FK constraint details |
| `RawIndex` | Index details |
| `RawSequence` | Sequence details |
| `RawSequenceTableMapping` | Sequence-to-table mapping |
| `RawStoredProcedure` | Stored proc / TVF / scalar function + parameters |
| `RawTrigger` | Trigger details |
| `RawMemoryOptimisedTable` | SQL Server memory-optimised table list |
| `RawExtendedProperty` | Extended property / column comment |

Any structural change to a Raw* class is a breaking change to the XML schema
and must be coordinated with the public repo.

## Current State

- `Program.cs` parses the three CLI args and populates `Settings`. The TODO
  at the bottom is to invoke `DatabaseReaderFactory`, run all the Read* methods,
  and serialise the results to XML on stdout.
- `DatabaseReader.cs` still compiles against several types from the old
  Generator project that have not yet been moved here: `Efrpg.LanguageMapping`,
  `IDbContextFilter`, `Column`, `Table`, `StoredProcedure`,
  `StoredProcedureParameter`, `Enumeration`, `Inflector`, etc. The project
  will not build until these are resolved.
- The generation-side logic in `DatabaseReader.CreateColumn()` (name
  humanisation, PascalCase, keyword escaping) probably does not belong in this
  tool - it is a T4/generator concern. In v4 the tool should return raw data
  only and the T4 template should do the name processing.

## What Needs Doing Next

1. Decide what to do with the generation-side code in `DatabaseReader.cs`
   (particularly `CreateColumn()`, `ReadEnums()` which call `Inflector` and
   `Settings.UsePascalCase`). Options:
   - Strip it out - the tool returns raw data and the T4 template processes it.
   - Keep it in the tool so the XML already has humanised names.

2. Add the NuGet package references for the five database drivers to
   `Efrpg.csproj`.

3. Move or remove the `Efrpg.LanguageMapping` references - either copy the
   relevant files from the public repo or refactor `DatabaseReader` to not
   need them.

4. Implement the XML serialisation of all Raw* objects in `Program.cs`.

5. Implement the XML deserialisation + T4 invocation side in the public repo.

## Owner

Simon Hughes (sjh37) - simon@reversepoco.co.uk
