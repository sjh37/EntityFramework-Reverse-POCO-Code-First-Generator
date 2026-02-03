# Extended Property Names Feature

## Summary

This feature enhancement allows you to access extended properties by their **name** in addition to their value. Previously, only the extended property value was available as a string in `column.ExtendedProperty`. Now you can access individual extended properties by name using the new `column.ExtendedProperties` dictionary.

## What Was Changed

### 1. **RawExtendedProperty Class**
   - Added `PropertyName` field to store the extended property name
   - Updated constructor signature to accept property name parameter

### 2. **Column Class**
   - Added `ExtendedProperties` dictionary: `Dictionary<string, string>` (case-insensitive)
   - Maintains backward compatibility with existing `ExtendedProperty` string field

### 3. **DatabaseReader**
   - Updated `ReadExtendedProperties()` method to read property names from query results
   - Added safe fallback when property name column doesn't exist

### 4. **Database-Specific Readers**
   - **SQL Server**: Updated query to include `ep.name AS [propertyname]`
   - **PostgreSQL**: Added placeholder `'Comment' AS "propertyname"` (PostgreSQL uses generic comments)
   - **SQL Server CE**: Added placeholder `'ExtendedProperty' AS [propertyname]`
   - **MySQL, Oracle, SQLite**: No changes (don't support extended properties)

### 5. **Generator**
   - Updated `AddExtendedPropertiesToFilters()` to populate the `ExtendedProperties` dictionary
   - Maintains backward compatibility with existing `ExtendedProperty` string

### 6. **Settings.cs**
   - Added example usage in `UpdateColumn` comments showing how to use the new feature

## How to Use

### SQL Server Setup

First, add extended properties to your database columns:

```sql
-- Add a JsonPropertyName extended property to a column
EXEC sp_addextendedproperty 
    @name = N'JsonPropertyName', 
    @value = N'id', 
    @level0type = N'SCHEMA', 
    @level0name = N'dbo', 
    @level1type = N'TABLE', 
    @level1name = N'YourTable', 
    @level2type = N'COLUMN', 
    @level2name = N'SystemId'

-- You can add multiple extended properties to the same column
EXEC sp_addextendedproperty 
    @name = N'CustomValidator', 
    @value = N'EmailValidator', 
    @level0type = N'SCHEMA', 
    @level0name = N'dbo', 
    @level1type = N'TABLE', 
    @level1name = N'YourTable', 
    @level2type = N'COLUMN', 
    @level2name = N'EmailAddress'
```

### Code Generation Setup

In your `.tt` file or `Settings.cs`, use the `UpdateColumn` delegate:

```csharp
Settings.UpdateColumn = delegate (Column column, Table table, List<EnumDefinition> enumDefinitions)
{
    // Add JsonPropertyName attribute from extended property
    if (column.ExtendedProperties.ContainsKey("JsonPropertyName"))
    {
        var jsonPropertyName = column.ExtendedProperties["JsonPropertyName"];
        column.Attributes.Add($"[JsonPropertyName(\"{jsonPropertyName}\")]");
    }

    // Add custom validator attribute
    if (column.ExtendedProperties.ContainsKey("CustomValidator"))
    {
        var validatorName = column.ExtendedProperties["CustomValidator"];
        column.Attributes.Add($"[CustomValidator(typeof({validatorName}))]");
    }

    // Check if a specific extended property exists
    if (column.ExtendedProperties.ContainsKey("HideFromApi"))
    {
        column.Attributes.Add("[JsonIgnore]");
    }

    // Backward compatibility: still works with the old ExtendedProperty string
    if (column.ExtendedProperty == "HIDE")
        column.Hidden = true;
};
```

### Generated Code Example

Given the SQL setup above, the generated code will look like:

```csharp
public class YourTable
{
    [JsonPropertyName("id")]
    public string SystemId { get; set; }

    [CustomValidator(typeof(EmailValidator))]
    public string EmailAddress { get; set; }
}
```

## Supported Databases

| Database | Extended Property Name Support | Notes |
|----------|-------------------------------|-------|
| **SQL Server** | ✅ Yes | Reads actual extended property names from `sys.extended_properties` |
| **PostgreSQL** | ⚠️ Placeholder | Uses 'Comment' as property name (PostgreSQL only has generic column comments) |
| **SQL Server CE** | ⚠️ Placeholder | Uses 'ExtendedProperty' as property name |
| **MySQL** | ❌ No | Does not support extended properties |
| **Oracle** | ❌ No | Does not support extended properties |
| **SQLite** | ❌ No | Does not support extended properties |

## Backward Compatibility

All existing code continues to work:
- `column.ExtendedProperty` still contains the concatenated extended property values
- Old settings and templates work without modification
- The new `column.ExtendedProperties` dictionary is additive functionality

## Use Cases

1. **JSON Property Mapping**: Map database column names to different JSON property names
2. **Custom Validation**: Specify validators via extended properties
3. **API Visibility**: Control which properties appear in APIs
4. **Documentation**: Store property-specific documentation by category
5. **Feature Flags**: Enable/disable features per column
6. **Custom Attributes**: Add any custom attributes based on extended properties

## Example: Multiple Extended Properties on One Column

```sql
-- Add multiple extended properties to one column
EXEC sp_addextendedproperty @name = N'JsonPropertyName', @value = N'userId', 
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Orders', 
    @level2type = N'COLUMN', @level2name = N'UserId'

EXEC sp_addextendedproperty @name = N'Validator', @value = N'Required|Guid', 
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Orders', 
    @level2type = N'COLUMN', @level2name = N'UserId'

EXEC sp_addextendedproperty @name = N'Description', @value = N'The unique identifier for the user', 
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Orders', 
    @level2type = N'COLUMN', @level2name = N'UserId'
```

```csharp
Settings.UpdateColumn = delegate (Column column, Table table, List<EnumDefinition> enumDefinitions)
{
    // Access all three extended properties
    if (column.ExtendedProperties.ContainsKey("JsonPropertyName"))
        column.Attributes.Add($"[JsonPropertyName(\"{column.ExtendedProperties["JsonPropertyName"]}\")]");
    
    if (column.ExtendedProperties.ContainsKey("Validator"))
    {
        var validators = column.ExtendedProperties["Validator"].Split('|');
        foreach (var validator in validators)
            column.Attributes.Add($"[{validator}]");
    }
    
    if (column.ExtendedProperties.ContainsKey("Description"))
        column.SummaryComments = column.ExtendedProperties["Description"];
};
```

## Notes

- Extended property names are **case-insensitive** (using `StringComparer.OrdinalIgnoreCase`)
- If a database doesn't support named extended properties, a placeholder name is used
- The `DatabaseReader.ReadExtendedProperties()` method safely handles missing property name columns
- Property names are trimmed of whitespace
