# Database Support Summary

## Overview

The EntityFramework Reverse POCO Generator now supports **four major database platforms**:

1. ✅ **SQL Server** - Full support (original implementation)
2. ✅ **PostgreSQL** - Full support (existing implementation)
3. ✅ **MySQL** - **NEW** - Full support (5.7+)
4. ✅ **Oracle** - **NEW** - Full support (11g+)
5. ✅ **SQLite** - Full support (existing implementation)
6. ✅ **SQL Server CE** - Full support (existing implementation)

## Quick Start

### Important: T4 Template Architecture

This generator is packaged as a **T4 template** which means:
- ✅ The template code uses **DbProviderFactory** (no direct NuGet dependencies)
- ✅ Database providers must be installed in **your host project** (not the template)
- ✅ Providers must be **registered in app.config/web.config**
- ✅ Limited to **.NET Framework 4.x capabilities**

### MySQL Configuration

```csharp
Settings.DatabaseType = DatabaseType.MySql;
Settings.ConnectionString = "Server=localhost;Database=mydatabase;Uid=myuser;Pwd=mypassword;";
```

**Required in Your Project**: `MySqlConnector` or `MySql.Data` (registered in app.config)

**See**: [MYSQL_IMPLEMENTATION.md](MYSQL_IMPLEMENTATION.md) for detailed documentation

### Oracle Configuration

```csharp
Settings.DatabaseType = DatabaseType.Oracle;
Settings.ConnectionString = "Data Source=ORCL;User Id=myuser;Password=mypassword;";
```

**Required in Your Project**: `Oracle.ManagedDataAccess` (usually auto-registers)

**See**: [ORACLE_IMPLEMENTATION.md](ORACLE_IMPLEMENTATION.md) for detailed documentation

## Feature Comparison Matrix

| Feature | SQL Server | PostgreSQL | MySQL | Oracle | SQLite |
|---------|-----------|------------|--------|---------|--------|
| **Tables/Views** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Primary Keys** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Foreign Keys** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Indexes** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Stored Procedures** | ✅ | ✅ | ✅ | ✅ | ❌ |
| **Functions** | ✅ | ✅ | ✅ | ✅ | ❌ |
| **Triggers** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Sequences** | ✅ | ✅ | ❌* | ✅ | ❌ |
| **Synonyms** | ✅ | ❌ | ❌ | ✅ | ❌ |
| **Identity Columns** | ✅ | ✅ | ✅ | ✅** | ✅ |
| **Computed Columns** | ✅ | ✅ | ✅ | ✅ | ❌ |
| **Column Comments** | ✅ | ✅ | ✅ | ✅ | ❌ |
| **Temporal Tables** | ✅ | ❌ | ❌ | ❌ | ❌ |
| **Packages** | ❌ | ❌ | ❌ | ✅ | ❌ |

\* MySQL does not have native SEQUENCE objects, instead MySQL uses AUTO_INCREMENT on table columns for sequence-like behaviour.

\** Oracle 12c+ only

## Database-Specific Features

### SQL Server Exclusive
- Temporal tables
- Memory-optimized tables
- Hierarchical data types
- Full-text indexes
- Spatial indexes

### PostgreSQL Specific
- Array types
- JSON/JSONB types
- Range types
- Composite types
- Table inheritance

### MySQL Specific
- ENUM and SET types
- AUTO_INCREMENT
- Multiple storage engines (InnoDB, MyISAM)
- Full-text search
- Spatial types (GIS)

### Oracle Specific
- Packages (grouped procedures/functions)
- REF CURSOR types
- Sequences (widely used)
- Synonyms
- Virtual columns
- Object types (UDTs)

## Connection String Examples

### SQL Server
```csharp
"Server=localhost;Database=MyDb;Integrated Security=true;"
"Server=localhost;Database=MyDb;User Id=sa;Password=pass;"
```

### PostgreSQL
```csharp
"Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=pass;"
```

### MySQL
```csharp
"Server=localhost;Port=3306;Database=mydb;Uid=root;Pwd=pass;"
```

### Oracle
```csharp
"Data Source=localhost:1521/ORCL;User Id=system;Password=pass;"
"Data Source=ORCL;User Id=myuser;Password=pass;"  // Using TNS
```

### SQLite
```csharp
"Data Source=C:\\path\\to\\database.db;"
```

## Version Requirements

### MySQL
- **Minimum**: 5.7
- **Recommended**: 8.0+
- **Identity Columns**: All versions (AUTO_INCREMENT)
- **Generated Columns**: 5.7.6+
- **Sequences**: 8.0+ (not yet implemented)

### Oracle
- **Minimum**: 11g (11.2.0.4)
- **Recommended**: 19c (LTS)
- **Identity Columns**: 12c+
- **Virtual Columns**: 11g+
- **Enhanced JSON**: 18c+

### SQL Server
- **Minimum**: 2008 R2
- **Recommended**: 2019+
- **Temporal Tables**: 2016+
- **Memory-Optimized**: 2014+

### PostgreSQL
- **Minimum**: 9.4
- **Recommended**: 12+
- **Identity Columns**: 10+
- **Partitioning**: 10+

## Data Type Mappings Summary

### Numeric Types
| Database | Small Int | Int | Long | Decimal | Float | Double |
|----------|-----------|-----|------|---------|-------|--------|
| SQL Server | SMALLINT | INT | BIGINT | DECIMAL | REAL | FLOAT |
| PostgreSQL | SMALLINT | INT | BIGINT | NUMERIC | REAL | DOUBLE PRECISION |
| MySQL | SMALLINT | INT | BIGINT | DECIMAL | FLOAT | DOUBLE |
| Oracle | NUMBER(4) | NUMBER(9) | NUMBER(18) | NUMBER(p,s) | BINARY_FLOAT | BINARY_DOUBLE |

### String Types
| Database | Fixed Char | Variable Char | Text | Unicode |
|----------|------------|---------------|------|---------|
| SQL Server | CHAR | VARCHAR | TEXT | NCHAR, NVARCHAR |
| PostgreSQL | CHAR | VARCHAR | TEXT | All support Unicode |
| MySQL | CHAR | VARCHAR | TEXT | UTF8/UTF8MB4 charset |
| Oracle | CHAR | VARCHAR2 | CLOB | NCHAR, NVARCHAR2 |

### Date/Time Types
| Database | Date | DateTime | Timestamp | Time | Interval |
|----------|------|----------|-----------|------|----------|
| SQL Server | DATE | DATETIME | DATETIME2 | TIME | ❌ |
| PostgreSQL | DATE | TIMESTAMP | TIMESTAMPTZ | TIME | INTERVAL |
| MySQL | DATE | DATETIME | TIMESTAMP | TIME | ❌ |
| Oracle | DATE | TIMESTAMP | TIMESTAMP TZ | ❌ | INTERVAL |

### Binary Types
| Database | Binary | Variable Binary | Large Binary |
|----------|--------|-----------------|--------------|
| SQL Server | BINARY | VARBINARY | VARBINARY(MAX) |
| PostgreSQL | ❌ | BYTEA | BYTEA |
| MySQL | BINARY | VARBINARY | BLOB, LONGBLOB |
| Oracle | RAW | RAW | BLOB |

## Implementation Status

### MySQL Implementation ✅ COMPLETE
- ✅ Core schema reading (tables, columns, keys)
- ✅ Foreign key relationships
- ✅ Index detection
- ✅ Extended properties (column comments)
- ✅ Stored procedures and functions
- ✅ Trigger detection
- ✅ Parameter type mappings
- ✅ Return type inference
- ✅ Documentation complete
- ⏳ **AWAITING USER TESTING**

### Oracle Implementation ✅ COMPLETE
- ✅ Core schema reading (tables, columns, keys)
- ✅ Foreign key relationships
- ✅ Index detection
- ✅ Extended properties (column comments)
- ✅ Stored procedures and functions
- ✅ Package support
- ✅ Trigger detection
- ✅ Sequence detection
- ✅ Synonym support
- ✅ Parameter type mappings
- ✅ Documentation complete
- ⏳ **AWAITING USER TESTING**

## Testing Recommendations

### MySQL Testing Checklist
1. ☐ Connect to MySQL 5.7 database
2. ☐ Connect to MySQL 8.0 database
3. ☐ Verify table generation
4. ☐ Verify foreign key detection
5. ☐ Test AUTO_INCREMENT columns
6. ☐ Test ENUM type mapping
7. ☐ Test stored procedures
8. ☐ Test functions
9. ☐ Test generated columns (computed)
10. ☐ Verify column comments

### Oracle Testing Checklist
1. ☐ Connect to Oracle 11g database
2. ☐ Connect to Oracle 12c+ database (identity columns)
3. ☐ Verify table generation
4. ☐ Verify foreign key detection
5. ☐ Test identity columns (12c+)
6. ☐ Test virtual columns (computed)
7. ☐ Test sequences
8. ☐ Test stored procedures
9. ☐ Test functions
10. ☐ Test packages
11. ☐ Test synonyms
12. ☐ Verify column comments

## Known Limitations & Workarounds

### MySQL
**Limitation**: Stored procedures with dynamic SQL cannot infer return types  
**Workaround**: Use `Settings.StoredProcedureReturnTypes` for manual specification

**Limitation**: ENUM types map to string  
**Workaround**: Use `Settings.AddEnumDefinitions` to map columns to C# enums

### Oracle
**Limitation**: REF CURSOR return types require execution  
**Workaround**: Use `Settings.StoredProcedureReturnTypes` for manual specification

**Limitation**: Overloaded package procedures  
**Workaround**: Manual configuration may be required

**Limitation**: Object types (UDTs) not automatically mapped  
**Workaround**: Manual type mapping required

## Performance Tips

### All Databases
```csharp
// Use schema filtering
SchemaReading = (tableName, tableType) => 
    !tableName.StartsWith("TEMP_");
```

### MySQL Specific
```csharp
// Enable connection pooling in connection string
"Server=localhost;Database=mydb;Uid=user;Pwd=pass;Pooling=true;MinPoolSize=5;MaxPoolSize=100;"

// Ensure MySQL provider is registered in app.config
```

### Oracle Specific
```csharp
// Enable statement caching
"Data Source=ORCL;User Id=user;Password=pass;Statement Cache Size=50;Min Pool Size=10;"

// Ensure Oracle provider is available in your project
// Gather database statistics for better query performance
// Run in Oracle: EXEC DBMS_STATS.GATHER_SCHEMA_STATS('YOUR_SCHEMA');
```

## Migration Notes

### From SQL Server to MySQL
- Replace `IDENTITY` with `AUTO_INCREMENT`
- Replace `NVARCHAR` with `VARCHAR` (UTF8 charset)
- Replace `BIT` with `TINYINT(1)` for booleans
- Replace `DATETIME2` with `DATETIME`
- Functions and procedures have different syntax

### From SQL Server to Oracle
- Replace `IDENTITY` with `GENERATED ALWAYS AS IDENTITY` (12c+) or sequences
- Replace `VARCHAR` with `VARCHAR2`
- Replace `TEXT` with `CLOB`
- Replace `INT` with `NUMBER(10)`
- Use packages to group procedures/functions

### From Oracle to MySQL
- Replace sequences with `AUTO_INCREMENT`
- Replace `VARCHAR2` with `VARCHAR`
- Replace `CLOB` with `LONGTEXT`
- Replace `NUMBER` with appropriate numeric type
- Packages need to be converted to individual procedures

## Troubleshooting Guide

### Connection Issues
1. Verify database server is running
2. Check firewall settings
3. Verify connection string format
4. Test with native database tools first
5. Check user privileges

### Schema Reading Issues
1. Verify user has SELECT privileges on information schema
2. Check database/schema name in connection string
3. Look for linter errors after generation
4. Enable detailed error logging
5. Check excluded schema lists

### Generated Code Issues
1. Build the generated code to find errors
2. Check data type mappings
3. Verify nullable vs non-nullable columns
4. Review foreign key relationships
5. Check for reserved keyword conflicts

## Support and Resources

### Documentation
- [MySQL Implementation Guide](MYSQL_IMPLEMENTATION.md)
- [Oracle Implementation Guide](ORACLE_IMPLEMENTATION.md)
- [Main Project README](README.md)

### External Resources
- [MySQL Connector/NET](https://dev.mysql.com/doc/connector-net/en/)
- [MySqlConnector](https://mysqlconnector.net/)
- [Oracle Managed Data Access](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/)
- [Npgsql (PostgreSQL)](https://www.npgsql.org/)

## Contributors

MySQL and Oracle support implemented following the existing architecture patterns established by SQL Server and PostgreSQL implementations.

## Changelog

### Version Next
- ✅ **NEW**: Full MySQL database support (5.7+)
- ✅ **NEW**: Full Oracle database support (11g+)
- ✅ **NEW**: MySQL stored procedures and functions
- ✅ **NEW**: Oracle packages, sequences, and synonyms
- ✅ **NEW**: Comprehensive documentation for both databases

---

**Status**: ✅ Implementation Complete | ⏳ Awaiting User Testing

**Next Steps**: 
1. User testing of MySQL implementation
2. User testing of Oracle implementation  
3. Gather feedback and address any issues
4. Consider future enhancements based on user feedback

