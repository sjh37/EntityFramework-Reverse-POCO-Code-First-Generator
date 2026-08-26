# TODO

Known gaps, in rough priority order. Everything here is a deliberate omission rather than an unknown, so each
entry says what is missing and what it would take.

## 1. Nothing compiles the MySQL or Oracle generated output

`Tester.Integration.EFCore8/9/10` compile the SQL Server output, which is what turns a bad mapping into a build
failure. MySQL and Oracle have goldens (`Generator.Tests.Integration/TestComparison/*_MySql_*`, `*_Oracle_*`) but
nothing compiles them, so a method the provider does not define only shows up if somebody reads the diff.

That is exactly how `Settings.ColumnIdentity` came to emit `.UseIdentityColumn()` for MySQL, which Pomelo spells
`UseMySqlIdentityColumn` - the generated code would not have compiled, and no test said so.

Fix: a tester project per dialect referencing `Pomelo.EntityFrameworkCore.MySql` and
`Oracle.EntityFrameworkCore`. This is the largest remaining hole.

## 2. No stored procedure return models for MySQL or Oracle

Neither reader implements `ReadStoredProcReturnObjects`, so procedures generate without result classes.
PostgreSQL builds them from the catalogue; SQL Server executes with `SET FMTONLY`. Oracle can be done from
`ALL_ARGUMENTS`; MySQL has no catalogue for it, so it would need a prepared-statement round trip.

## 3. Oracle reads exactly one schema per run

`OracleDatabaseReader` scopes every query to `SYS_CONTEXT('USERENV','CURRENT_SCHEMA')`. SQL Server and
PostgreSQL span schemas in a single context; Oracle cannot. The change is contained - take a schema list from a
setting and replace the `CURRENT_SCHEMA` predicate with an `IN` list - but nobody has asked for it.

## 4. MySQL cannot see across databases

MySQL treats a database as a schema, so connecting to `EfrpgTest` makes the `EfrpgTestOther` tables invisible.
`TestDatabases/MySQL/EfrpgTest.sql` creates that second database for the cross-schema foreign key case, so the
script contains the scenario but the test does not actually exercise it. Fixing it means teaching the MySQL
reader to read across databases.

## 5. Oracle synonyms are not read

`SynonymsSQL` deliberately returns an empty string. `ALL_SYNONYMS` is there whenever someone asks for it.

## 6. `IMultiDbContextSettingsPlugin` is excluded from the tool build

It depends on Generator types, so the tool cannot compile it. If the tool ever needs it, the interface has to be
redesigned to stand alone.

## 7. `MySqlToCSharp` maps `tinyint` to `SByte`

The BCL name, where every other entry in every mapping uses the C# keyword. It compiles and
`DatabaseReader.MapType` accepts both spellings, so this is cosmetic. Left alone deliberately: changing it churns
both copies of the mapping, `DatabaseReaderSqlTests` and a golden, for no behavioural gain.

## 8. Language mapping keys are no longer compared between the two repositories

`WireContractTests` replaced `ParallelSourceTests` when the tool moved to its own repo, and it deliberately does
not compare mapping keys: an unknown key legitimately falls through to each mapping's default - that is how every
SQL Server character type is typed - so a missing key cannot be asserted mechanically. Only the integration
goldens catch it, and only for a dialect they cover. Item 1 would close most of this.
