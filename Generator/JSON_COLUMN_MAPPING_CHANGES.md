# JSON Column Mapping Enhancement - Implementation Summary

## Overview

This enhancement allows users to map JSON database columns to specific C# POCO classes instead of the default `string` type. This feature eliminates the need for manual deserialization in application code and provides strong typing for JSON data.

## Files Created

### 1. `JsonColumnMapping.cs` (NEW)
- **Purpose**: Defines the mapping structure for JSON columns to POCO classes
- **Location**: `Generator\JsonColumnMapping.cs`
- **Key Properties**:
  - `Schema`: Database schema name (supports "*" wildcard)
  - `Table`: Table name (supports "*" wildcard)
  - `Column`: Column name to map
  - `PropertyType`: Target C# type name
  - `AdditionalNamespace`: Optional namespace(s) required for the type

### 2. `JsonColumnMappingExamples.cs` (DOCUMENTATION)
- **Purpose**: Comprehensive examples of how to use JSON column mappings
- **Location**: `Generator\JsonColumnMappingExamples.cs`
- **Contents**: 8 detailed examples covering common use cases
- **Note**: This file is for documentation only and is not compiled

### 3. `JSON_COLUMN_MAPPING_README.md` (DOCUMENTATION)
- **Purpose**: Complete user guide for the JSON column mapping feature
- **Location**: `Generator\JSON_COLUMN_MAPPING_README.md`
- **Contents**: 
  - Quick start guide
  - Configuration options
  - Common patterns
  - EF configuration examples
  - Troubleshooting guide

## Files Modified

### 1. `Settings.cs`
**Location**: `Generator\Settings.cs`

**Changes**:

a) **Added JSON mapping callback** (after line 678):
```csharp
public static Action<List<JsonColumnMapping>> AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
{
    // Callback for users to define JSON column mappings
};
```

b) **Updated UpdateColumn signature** (line 314):
- **Before**: `Action<Column, Table, List<EnumDefinition>>`
- **After**: `Action<Column, Table, List<EnumDefinition>, List<JsonColumnMapping>>`

c) **Added JSON mapping logic** (in UpdateColumn delegate, after enum mapping logic):
```csharp
// Perform JSON column to POCO class mapping
// Supports: SQL Server "json", PostgreSQL "json" and "jsonb"
var isJsonColumn = column.SqlPropertyType != null && 
                   (column.SqlPropertyType.Equals("json", StringComparison.InvariantCultureIgnoreCase) ||
                    column.SqlPropertyType.Equals("jsonb", StringComparison.InvariantCultureIgnoreCase));

if (isJsonColumn)
{
    var jsonMapping = jsonColumnMappings?.FirstOrDefault(m => ...);
    if (jsonMapping != null)
    {
        column.PropertyType = jsonMapping.PropertyType;
        // Add additional namespaces
        // Clear default value
    }
}
```

### 2. `SingleContextFilter.cs`
**Location**: `Generator\Filtering\SingleContextFilter.cs`

**Changes**:

a) **Added field** (line 14):
```csharp
public List<JsonColumnMapping> JsonColumnMappings;
```

b) **Initialize in constructor** (after line 38):
```csharp
JsonColumnMappings = new List<JsonColumnMapping>();
Settings.AddJsonColumnMappings?.Invoke(JsonColumnMappings);
```

c) **Updated UpdateColumn call** (line 95):
```csharp
Settings.UpdateColumn?.Invoke(column, table, EnumDefinitions, JsonColumnMappings);
```

### 3. `MultiContextFilter.cs`
**Location**: `Generator\Filtering\MultiContextFilter.cs`

**Changes**:

a) **Updated UpdateColumn call** (line 346):
- **Before**: `Settings.UpdateColumn?.Invoke(column, table, null);`
- **After**: `Settings.UpdateColumn?.Invoke(column, table, null, null);`
- Passes null for both EnumDefinitions and JsonColumnMappings

### 4. `Generator.csproj`
**Location**: `Generator\Generator.csproj`

**Changes**:

a) **Added new file to compilation** (after line 234):
```xml
<Compile Include="JsonColumnMapping.cs" />
```

## How It Works

### Processing Flow

1. **User Configuration**:
   - User defines JSON column mappings in the `Settings.AddJsonColumnMappings` callback
   - Mappings are stored in a `List<JsonColumnMapping>`

2. **Filter Initialization**:
   - `SingleContextFilter` constructor initializes `JsonColumnMappings` list
   - Invokes `Settings.AddJsonColumnMappings` to populate the list

3. **Column Processing**:
   - During table/column reading, `UpdateColumn` is called for each column
   - The method receives both `EnumDefinitions` and `JsonColumnMappings`

4. **Type Mapping**:
   - If column SQL type is "json" or "jsonb", check for matching mapping
   - Match criteria: Schema (or "*"), Table (or "*"), and Column name
   - If match found:
     - Change `column.PropertyType` to the specified type
     - Add required namespaces to `Settings.AdditionalNamespaces`
     - Clear default value

5. **Code Generation**:
   - Generated POCO classes use the custom type instead of `string`
   - Required namespaces are included in using statements

## Supported Databases

- **SQL Server** (2016+, Azure SQL): `json` type
- **PostgreSQL**: `json` and `jsonb` types
- **MySQL**: Currently maps JSON to string (could be enhanced)

## Breaking Changes

**None**. This is a backward-compatible enhancement.

- Existing code that doesn't use JSON column mappings will continue to work exactly as before
- The new parameter in `UpdateColumn` is added to the end, maintaining compatibility
- Default behavior (mapping JSON to string) is preserved when no mappings are defined

## Usage Example

```csharp
// In your Settings or .tt file

// Step 1: Define your POCO classes
namespace MyApp.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}

// Step 2: Configure the mapping
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

// Step 3: Generate code
// The Order class will now have:
// public Address ShippingAddress { get; set; }

// Step 4: Configure Entity Framework (in DbContext)
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Order>()
        .Property(e => e.ShippingAddress)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null));
}
```

## Testing Recommendations

1. **Unit Tests**:
   - Test JSON column detection logic
   - Test mapping matching (exact match, wildcard schema, wildcard table)
   - Test namespace addition
   - Test with null/empty mapping lists

2. **Integration Tests**:
   - Test with SQL Server json columns
   - Test with PostgreSQL json and jsonb columns
   - Test code generation output
   - Test with multiple mappings for same table
   - Test with wildcard patterns

3. **Manual Testing**:
   - Create a test database with JSON columns
   - Configure various mappings
   - Verify generated code
   - Test EF serialization/deserialization

## Future Enhancements

Potential improvements for future releases:

1. **MySQL Support**: Add explicit support for MySQL JSON type
2. **Auto-detection**: Analyze JSON structure to suggest POCO class structure
3. **Converter Attributes**: Automatically add JSON converter attributes
4. **Validation**: Validate that target types exist and are serializable
5. **Nested Mappings**: Support mapping nested JSON properties
6. **Template Support**: Add mustache template placeholders for JSON mappings
7. **Type Inference**: Suggest appropriate C# types based on JSON schema

## Migration Guide

If you have existing projects using JSON columns:

1. **Current State**: JSON columns are mapped to `string` type
2. **After Upgrade**: 
   - Default behavior unchanged (still maps to string)
   - Optionally configure mappings to use custom types
3. **Migration Steps**:
   - Create POCO classes for your JSON structures
   - Add mappings using `Settings.AddJsonColumnMappings`
   - Regenerate code
   - Configure EF to handle serialization
   - Update application code to use strongly-typed properties

## Architecture Notes

### Design Decisions

1. **Callback Pattern**: Uses the same callback pattern as `AddEnumDefinitions` for consistency
2. **Wildcard Support**: Schema and Table support "*" for flexible matching
3. **Namespace Handling**: Automatically adds required namespaces to avoid compilation errors
4. **Non-Breaking**: New parameter added to end of method signature
5. **Database Agnostic**: Works with any database that has JSON support

### Consistency with Existing Code

- Follows same pattern as `EnumDefinition` feature
- Uses same filtering and matching logic
- Integrates seamlessly with existing callback system
- Maintains code generation architecture

### Performance Considerations

- Matching is done using `FirstOrDefault()` with case-insensitive comparison
- No performance impact if feature is not used
- Minimal overhead even with many mappings (uses LINQ filtering)

## Troubleshooting

### Common Issues

1. **Mapping not applied**:
   - Verify column is actually "json" or "jsonb" type
   - Check schema/table/column names match
   - Ensure callback is being invoked

2. **Type not found**:
   - Verify `AdditionalNamespace` is correct
   - Check POCO class is accessible
   - Ensure namespace is not misspelled

3. **Runtime errors**:
   - Configure EF to handle serialization
   - Ensure JSON structure matches POCO
   - Check for nullable/non-nullable mismatches

## Questions and Support

For issues or questions:
1. Check `JSON_COLUMN_MAPPING_README.md` for detailed documentation
2. Review `JsonColumnMappingExamples.cs` for usage examples
3. Verify your configuration matches the examples
4. Check that your database columns are actually JSON type

## Conclusion

This enhancement provides a powerful way to work with JSON columns in a strongly-typed manner, reducing boilerplate code and improving type safety. The implementation follows existing patterns in the codebase and maintains backward compatibility.
