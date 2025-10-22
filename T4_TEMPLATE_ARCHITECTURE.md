# T4 Template Architecture - Database Provider Requirements

## Understanding the T4 Template Architecture

This EntityFramework Reverse POCO Generator is packaged as a **T4 template** (Text Template Transformation Toolkit). This architecture has specific implications for how database connectivity works.

## Key Architecture Points

### 1. No Direct NuGet Dependencies in Template

❌ **The T4 template CANNOT:**
- Reference NuGet packages directly
- Include external assemblies in the template itself
- Use anything beyond .NET Framework 4.x base capabilities

✅ **The T4 template CAN:**
- Use standard .NET Framework 4.x types
- Use `DbProviderFactory` pattern for database access
- Access providers registered in the host application

### 2. How Database Connectivity Works

```
┌─────────────────────────────────────────────────────────────┐
│ Your Project (Host Application)                             │
│                                                             │
│  • Has MySQL/Oracle provider installed via NuGet            │
│  • Provider registered in app.config/web.config             │
│  • Runs the T4 template at design time                      │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ Executes T4 Template
                       ▼
┌─────────────────────────────────────────────────────────────┐
│ T4 Template (EF.Reverse.POCO.v3.ttinclude)                  │
│                                                             │
│  Settings.DatabaseType = DatabaseType.MySql;                │
│                                                             │
│  ┌─────────────────────────────────────────────┐            │
│  │ Generator Code (packaged in template)       │            │
│  │                                              │           │
│  │  providerName = "MySql.Data.MySqlClient"    │            │
│  │  factory = DbProviderFactories.GetFactory() │◄─────┐     │
│  │  reader = new MySqlDatabaseReader(factory)  │      │     │
│  └─────────────────────────────────────────────┘      │     │
└────────────────────────────────────────────────────────┼────┘
                                                         │
                                                         │
┌────────────────────────────────────────────────────────┼────┐
│ DbProviderFactories (System.Data)                      │    │
│                                                        │    │
│  Looks up registered provider by invariant name ───────┘    │
│  Returns factory for that provider                          │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ Uses registered provider
                       ▼
┌─────────────────────────────────────────────────────────────┐
│ Database Provider (in your project)                         │
│                                                             │
│  • MySql.Data.MySqlClient.MySqlClientFactory                │
│  • Oracle.ManagedDataAccess.Client.OracleClientFactory      │
│                                                             │
│  Provides DbConnection, DbCommand, DbDataReader, etc.       │
└─────────────────────────────────────────────────────────────┘
```

## MySQL Provider Setup

### Step 1: Install Provider in Your Project

```powershell
Install-Package MySql.Data
```

### Step 2: Register Provider in app.config/web.config

```xml
<configuration>
  <system.data>
    <DbProviderFactories>
      <add name="MySQL Data Provider" 
           invariant="MySql.Data.MySqlClient" 
           description=".Net Framework Data Provider for MySQL" 
           type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=8.0.0.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />
    </DbProviderFactories>
  </system.data>
</configuration>
```

### Step 3: Configure T4 Template

```csharp
<#@ include file="EF.Reverse.POCO.v3.ttinclude" #>
<#
    Settings.DatabaseType = DatabaseType.MySql;
    Settings.ConnectionString = "Server=localhost;Database=mydb;Uid=user;Pwd=pass;";
    // ... other settings
#>
```

### What Happens at Runtime

1. T4 template executes in Visual Studio/MSBuild
2. Generator code calls: `DbProviderFactories.GetFactory("MySql.Data.MySqlClient")`
3. .NET looks up this invariant name in registered providers
4. Returns the `MySqlClientFactory` from your installed MySql.Data package
5. Generator uses the factory to create connections and read database schema
6. Generated code is written to your project

## Oracle Provider Setup

### Step 1: Install Provider in Your Project

```powershell
Install-Package Oracle.ManagedDataAccess
```

### Step 2: Provider Registration (Usually Automatic)

Oracle.ManagedDataAccess typically registers itself automatically when installed. If needed, you can verify/add manually:

```xml
<configuration>
  <system.data>
    <DbProviderFactories>
      <add name="ODP.NET, Managed Driver" 
           invariant="Oracle.ManagedDataAccess.Client" 
           description="Oracle Data Provider for .NET, Managed Driver" 
           type="Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess, Version=4.122.0.0, Culture=neutral, PublicKeyToken=89b483f429c47342" />
    </DbProviderFactories>
  </system.data>
</configuration>
```

### Step 3: Configure T4 Template

```csharp
<#@ include file="EF.Reverse.POCO.v3.ttinclude" #>
<#
    Settings.DatabaseType = DatabaseType.Oracle;
    Settings.ConnectionString = "Data Source=ORCL;User Id=user;Password=pass;";
    // ... other settings
#>
```

## Provider Invariant Names

The generator uses these invariant names when calling `DbProviderFactories.GetFactory()`:

| Database | Invariant Name | Default Provider |
|----------|---------------|------------------|
| SQL Server | `System.Data.SqlClient` | Built into .NET Framework |
| MySQL | `MySql.Data.MySqlClient` | MySql.Data package |
| Oracle | `Oracle.ManagedDataAccess.Client` | Oracle.ManagedDataAccess package |
| PostgreSQL | `Npgsql` | Npgsql package |
| SQLite | `System.Data.SQLite` | System.Data.SQLite package |

## Alternative Providers

### Using MySqlConnector Instead of MySql.Data

If you prefer to use `MySqlConnector` (the community MySQL provider), you have two options:

**Option 1: Register with Standard Invariant Name**
```xml
<add name="MySqlConnector" 
     invariant="MySql.Data.MySqlClient" 
     description="Async MySQL ADO.NET Connector" 
     type="MySqlConnector.MySqlConnectorFactory, MySqlConnector" />
```
This overrides the default MySql.Data provider.

**Option 2: Modify Generator Code**
Edit `Generator/Readers/DatabaseProvider.cs` to return `"MySqlConnector"` for MySQL type (requires rebuilding the template).

## Troubleshooting

### "Unable to find the requested .Net Framework Data Provider"

**Problem**: T4 template can't find the database provider

**Solutions**:
1. Verify provider is installed in your project (check packages.config or project file)
2. Verify provider is registered in app.config/web.config
3. Rebuild your project to ensure provider assemblies are available
4. Check that the invariant name matches exactly

### "Could not load file or assembly 'MySql.Data'"

**Problem**: Provider assembly not found at runtime

**Solutions**:
1. Ensure package is installed: `Install-Package MySql.Data`
2. Clean and rebuild your project
3. Check that the assembly is in your bin folder
4. Verify version numbers match in config registration

### "The provider did not return a ProviderManifestToken"

**Problem**: Provider is registered but can't connect to database

**Solutions**:
1. Verify connection string is correct
2. Test connection with native tools first
3. Check database server is accessible
4. Verify user has required privileges

## .NET Framework 4.x Limitation

Because T4 templates are limited to .NET Framework 4.x capabilities:

❌ **Cannot use:**
- .NET Core/.NET 5+ specific features
- C# 8+ nullable reference types in template code
- async/await patterns in template execution
- System.Text.Json (use Newtonsoft.Json if needed)

✅ **Can use:**
- All .NET Framework 4.x base class libraries
- Standard ADO.NET interfaces
- LINQ to Objects
- Collections, Regex, XML, etc.

This is why the generator code uses synchronous ADO.NET calls (`ExecuteReader()`, `Fill()`, etc.) rather than async versions.

## Building the T4 Template Package

The `BuildTT` project gathers all Generator `*.cs` files and packages them into `EF.Reverse.POCO.v3.ttinclude`. This ensures:

1. All database reader code is embedded in the template
2. No external assembly references are needed at runtime
3. The template is self-contained and portable
4. Users only need to include the `.ttinclude` file

## Summary

✅ **Implementation is correct**: Uses `DbProviderFactory` pattern properly  
✅ **No code changes needed**: MySQL and Oracle readers are already correctly implemented  
✅ **Documentation updated**: Clarifies T4 template architecture  
✅ **User action required**: Install and register providers in host project  

The architecture ensures maximum compatibility and portability while supporting multiple database platforms.

