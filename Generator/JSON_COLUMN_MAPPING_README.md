# JSON Column to POCO Class Mapping

## Overview

This feature allows you to map JSON database columns directly to C# POCO classes instead of the default `string` type. This eliminates the need for manual deserialization in your application code and provides strong typing for your JSON data.

## Supported Databases

- **SQL Server**: `json` type (SQL Server 2016+ and Azure SQL)
- **PostgreSQL**: `json` and `jsonb` types

## Quick Start

### 1. Define Your POCO Classes

First, create the POCO classes that match your JSON structure:

```csharp
namespace MyApp.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
```

### 2. Configure the Mapping

In your settings (typically in your `.tt` file or `Settings.cs`), configure the JSON column mappings:

```csharp
Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
{
    jsonColumnMappings.Add(new JsonColumnMapping
    {
        Schema = "dbo",
        Table = "Orders",
        Column = "ShippingAddress",
        PropertyType = "Address",
        AdditionalNamespace = "MyApp.Models"
    });
};
```

### 3. Generate Code

Run the generator as usual. The generated `Order` class will now have:

```csharp
public Address ShippingAddress { get; set; }
```

instead of:

```csharp
public string ShippingAddress { get; set; }
```

## Configuration Options

### JsonColumnMapping Properties

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| `Schema` | `string` | Database schema name. Use `"*"` to match any schema. | Yes |
| `Table` | `string` | Table name. Use `"*"` to match any table. | Yes |
| `Column` | `string` | Column name to map. | Yes |
| `PropertyType` | `string` | C# type to use (e.g., `"Address"`, `"List<string>"`, `"Dictionary<string, object>"`). | Yes |
| `AdditionalNamespace` | `string` | Namespace(s) required for the type. Separate multiple namespaces with semicolons. | No |

### Wildcard Matching

You can use wildcards to apply mappings broadly:

```csharp
// Map all "Metadata" columns across all tables and schemas
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "*",
    Table = "*",
    Column = "Metadata",
    PropertyType = "Dictionary<string, object>",
    AdditionalNamespace = "System.Collections.Generic"
});
```

## Common Patterns

### Pattern 1: Simple POCO

```csharp
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "dbo",
    Table = "Users",
    Column = "Profile",
    PropertyType = "UserProfile",
    AdditionalNamespace = "MyApp.Models"
});
```

### Pattern 2: Generic Collections

```csharp
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "dbo",
    Table = "Products",
    Column = "Tags",
    PropertyType = "List<string>",
    AdditionalNamespace = "System.Collections.Generic"
});
```

### Pattern 3: Dictionaries

```csharp
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "dbo",
    Table = "Settings",
    Column = "Configuration",
    PropertyType = "Dictionary<string, object>",
    AdditionalNamespace = "System.Collections.Generic"
});
```

### Pattern 4: Complex Nested Types

```csharp
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "dbo",
    Table = "Reports",
    Column = "Data",
    PropertyType = "Dictionary<string, List<ReportItem>>",
    AdditionalNamespace = "System.Collections.Generic;MyApp.Models"
});
```

### Pattern 5: Fully Qualified Type Names

```csharp
jsonColumnMappings.Add(new JsonColumnMapping
{
    Schema = "dbo",
    Table = "Documents",
    Column = "Content",
    PropertyType = "MyCompany.Domain.Models.DocumentContent"
    // AdditionalNamespace is optional when using fully qualified names
});
```

## Entity Framework Configuration

After generating your code, you need to configure Entity Framework to handle JSON serialization/deserialization.

### EF Core

In your `DbContext.OnModelCreating` method:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // For simple types
    modelBuilder.Entity<Order>()
        .Property(e => e.ShippingAddress)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null));

    // For nullable types
    modelBuilder.Entity<Order>()
        .Property(e => e.Metadata)
        .HasConversion(
            v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => v == null ? null : JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions)null));
}
```

### EF Core with Owned Types (Recommended for EF Core 8+)

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Order>()
        .OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.ToJson();
        });
}
```

## Important Notes

1. **Type Compatibility**: Ensure your POCO classes can be serialized/deserialized to/from JSON and match the structure in your database.

2. **Namespace Requirements**: If your type requires additional namespaces, specify them in `AdditionalNamespace`. The generator will automatically add them to the using statements.

3. **Nullable Columns**: 
   - Reference types (classes) are nullable by default
   - Value types will be made nullable (`T?`) if the database column allows NULL

4. **Default Values**: Default values are cleared for JSON columns mapped to custom types, as they're typically not meaningful for complex types.

5. **Entity Framework Configuration**: You must configure EF to handle serialization/deserialization. The generator creates the property with the correct type but doesn't add EF configuration automatically.

6. **Performance**: Be aware that deserializing JSON on every query may have performance implications. Consider using projections or lazy loading patterns when appropriate.

## Implementation Details

### Files Modified

- `JsonColumnMapping.cs` - New class defining the mapping structure
- `Settings.cs` - Added `AddJsonColumnMappings` callback and mapping logic
- `SingleContextFilter.cs` - Initialize and pass JSON mappings
- `MultiContextFilter.cs` - Updated to support new signature

### How It Works

1. During code generation, the generator reads database schema including column types
2. For each column with SQL type `json` or `jsonb`, it checks for a matching `JsonColumnMapping`
3. If a match is found:
   - The column's `PropertyType` is changed from `string` to the specified type
   - Additional namespaces are added to the file's using statements
   - Default values are cleared
4. The generated code includes the strongly-typed property

## Examples

See `JsonColumnMappingExamples.cs` for comprehensive examples of various usage scenarios.

## Troubleshooting

**Q: My custom type isn't recognized**
- Ensure the namespace is correct in `AdditionalNamespace`
- Verify the type is accessible in your project
- Check that you're using the correct casing

**Q: EF throws serialization errors**
- Verify you've configured EF to handle JSON serialization (see "Entity Framework Configuration" above)
- Ensure your POCO structure matches the JSON in the database

**Q: The mapping isn't being applied**
- Verify the schema, table, and column names match exactly (case-insensitive comparison is used)
- Check that the column is actually a `json` or `jsonb` type in the database
- Ensure you're calling the generator after configuring the mappings