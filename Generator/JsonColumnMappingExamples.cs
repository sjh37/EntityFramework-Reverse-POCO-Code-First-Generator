// This file contains examples of how to use JSON column mappings
// These examples are for documentation purposes only and should not be compiled.

// =============================================================================================
// JSON COLUMN TO POCO CLASS MAPPING EXAMPLES
// =============================================================================================
//
// The generator now supports mapping JSON database columns to specific C# POCO classes
// instead of just mapping them to string type. This eliminates the need for manual
// deserialization in your application code.
//
// To use this feature, configure the Settings.AddJsonColumnMappings callback in your
// settings file (typically in your .tt file or Settings.cs).
//
// =============================================================================================

namespace Efrpg.Examples
{
    // Example 1: Simple JSON column mapping
    // =====================================
    // Database: Orders table with a JSON column "ShippingAddress"
    // Goal: Map it to an Address POCO class
    /*
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

    // Result: The generated Order class will have:
    // public Address ShippingAddress { get; set; }
    // instead of:
    // public string ShippingAddress { get; set; }
    */

    // Example 2: Mapping with generic types
    // ======================================
    // Database: Products table with a JSON column "Tags"
    // Goal: Map it to List<string>
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Products",
            Column = "Tags",
            PropertyType = "List<string>",
            AdditionalNamespace = "System.Collections.Generic"
        });
    };

    // Result: The generated Product class will have:
    // public List<string> Tags { get; set; }
    */

    // Example 3: Wildcard table matching
    // ==================================
    // Goal: Map all "Metadata" columns across all tables to Dictionary<string, object>
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "*",           // Match any schema
            Table = "*",            // Match any table
            Column = "Metadata",
            PropertyType = "Dictionary<string, object>",
            AdditionalNamespace = "System.Collections.Generic"
        });
    };

    // Result: Any table with a "Metadata" JSON column will have:
    // public Dictionary<string, object> Metadata { get; set; }
    */

    // Example 4: Fully qualified type names
    // =====================================
    // Goal: Use a fully qualified type name (including namespace)
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Users",
            Column = "Preferences",
            PropertyType = "MyCompany.Domain.Models.UserPreferences"
            // AdditionalNamespace is optional when using fully qualified names
        });
    };

    // Result: The generated User class will have:
    // public MyCompany.Domain.Models.UserPreferences Preferences { get; set; }
    */

    // Example 5: Multiple namespaces
    // ==============================
    // Goal: Add multiple namespaces required for the mapped type
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Documents",
            Column = "Content",
            PropertyType = "DocumentContent",
            AdditionalNamespace = "MyApp.Models;MyApp.Helpers" // Semicolon-separated
        });
    };

    // Result: Both namespaces will be added to the generated file's using statements
    */

    // Example 6: Complex nested types
    // ================================
    // Goal: Map to a complex type with nested generics
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Reports",
            Column = "Data",
            PropertyType = "Dictionary<string, List<ReportItem>>",
            AdditionalNamespace = "System.Collections.Generic;MyApp.Models"
        });
    };

    // Result: The generated Report class will have:
    // public Dictionary<string, List<ReportItem>> Data { get; set; }
    */

    // Example 7: Schema-specific mapping
    // ==================================
    // Goal: Map only for a specific schema
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "sales",  // Only match tables in the 'sales' schema
            Table = "*",       // But match any table name
            Column = "CustomerInfo",
            PropertyType = "CustomerInformation",
            AdditionalNamespace = "MyApp.Sales.Models"
        });
    };
    */

    // Example 8: Multiple mappings for the same table
    // ===============================================
    // Goal: Map multiple JSON columns in the same table
    /*
    Settings.AddJsonColumnMappings = delegate (List<JsonColumnMapping> jsonColumnMappings)
    {
        // Map BillingAddress
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Orders",
            Column = "BillingAddress",
            PropertyType = "Address",
            AdditionalNamespace = "MyApp.Models"
        });

        // Map ShippingAddress
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Orders",
            Column = "ShippingAddress",
            PropertyType = "Address",
            AdditionalNamespace = "MyApp.Models"
        });

        // Map OrderItems
        jsonColumnMappings.Add(new JsonColumnMapping
        {
            Schema = "dbo",
            Table = "Orders",
            Column = "Items",
            PropertyType = "List<OrderItem>",
            AdditionalNamespace = "System.Collections.Generic;MyApp.Models"
        });
    };

    // Result: The generated Order class will have:
    // public Address BillingAddress { get; set; }
    // public Address ShippingAddress { get; set; }
    // public List<OrderItem> Items { get; set; }
    */

    // =============================================================================================
    // IMPORTANT NOTES
    // =============================================================================================
    //
    // 1. This feature applies to JSON columns in:
    //    - SQL Server: "json" type (SQL Server 2016+ or Azure SQL)
    //    - PostgreSQL: "json" or "jsonb" types
    //
    // 2. The PropertyType must be a valid C# type that:
    //    - Is accessible in your project
    //    - Can be serialized/deserialized to/from JSON
    //    - Matches the JSON structure stored in the database
    //
    // 3. You are responsible for:
    //    - Ensuring the POCO classes exist in your project
    //    - Configuring Entity Framework to serialize/deserialize these types
    //    - Adding appropriate JSON converters if needed
    //
    // 4. For EF Core, you may need to configure JSON serialization in your DbContext:
    //    modelBuilder.Entity<Order>()
    //        .Property(e => e.ShippingAddress)
    //        .HasConversion(
    //            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
    //            v => JsonSerializer.Deserialize<Address>(v, (JsonSerializerOptions)null));
    //
    // 5. Nullable handling:
    //    - Reference types (classes) will be nullable by default
    //    - Value types will be made nullable if the database column allows NULL
    //
    // 6. The generator automatically adds required namespaces from AdditionalNamespace
    //    to the Settings.AdditionalNamespaces list
    //
    // =============================================================================================
}
