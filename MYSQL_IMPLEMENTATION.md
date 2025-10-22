# MySQL Database Support - Implementation Notes

## Overview

The EntityFramework Reverse POCO Generator now supports MySQL databases (version 5.7 and later) with full support for:
- Tables and Views
- Columns with all standard MySQL data types
- Primary Keys (single and composite)
- Foreign Keys with CASCADE rules
- Indexes (unique, non-unique, primary)
- Column Comments (extended properties)
- Stored Procedures and Functions
- Triggers
- AUTO_INCREMENT identity columns
- Generated columns (computed columns)

## Requirements

### Minimum MySQL Version
- **MySQL 5.7+** for full compatibility
- **MySQL 8.0+** for enhanced features and performance improvements

### Required MySQL Privileges

The database user specified in the connection string must have the following privileges:

```sql
GRANT SELECT ON information_schema.* TO 'your_user'@'%';
GRANT SELECT, EXECUTE ON your_database.* TO 'your_user'@'%';
```

Specifically, the following INFORMATION_SCHEMA tables must be accessible:
- `INFORMATION_SCHEMA.TABLES`
- `INFORMATION_SCHEMA.COLUMNS`
- `INFORMATION_SCHEMA.KEY_COLUMN_USAGE`
- `INFORMATION_SCHEMA.TABLE_CONSTRAINTS`
- `INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS`
- `INFORMATION_SCHEMA.STATISTICS`
- `INFORMATION_SCHEMA.ROUTINES`
- `INFORMATION_SCHEMA.PARAMETERS`
- `INFORMATION_SCHEMA.TRIGGERS`

### Connection String Format

The connection string format depends on which MySQL provider is registered in your project:

```csharp
// MySql.Data format (Oracle official)
Settings.ConnectionString = "Server=localhost;Port=3306;Database=mydatabase;Uid=myuser;Pwd=mypassword;";

// MySqlConnector format (community provider)
Settings.ConnectionString = "Server=localhost;Port=3306;Database=mydatabase;User Id=myuser;Password=mypassword;";

// With SSL
Settings.ConnectionString = "Server=localhost;Port=3306;Database=mydatabase;Uid=myuser;Pwd=mypassword;SslMode=Required;";
```

### Registering MySQL Provider in app.config/web.config

```xml
<configuration>
  <system.data>
    <DbProviderFactories>
      <!-- For MySql.Data -->
      <add name="MySQL Data Provider" 
           invariant="MySql.Data.MySqlClient" 
           description=".Net Framework Data Provider for MySQL" 
           type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=8.0.0.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />
      
      <!-- OR for MySqlConnector -->
      <add name="MySqlConnector" 
           invariant="MySqlConnector" 
           description="Async MySQL ADO.NET Connector" 
           type="MySqlConnector.MySqlConnectorFactory, MySqlConnector, Culture=neutral, PublicKeyToken=d33d3e53aa5f8c92" />
    </DbProviderFactories>
  </system.data>
</configuration>
```

### Required Database Provider

Since this generator is packaged as a T4 template, **you do not install NuGet packages in the template itself**. Instead, the database provider must be available in **your host project**:

**Option 1: MySql.Data (Oracle's official provider)**
- Already included with MySQL Connector/NET installation
- Registered globally or in your project's app.config/web.config

**Option 2: MySqlConnector (Community provider)**
- Install in your project: `Install-Package MySqlConnector`
- Register in app.config/web.config

**Important**: The T4 template uses `DbProviderFactory` to access whatever MySQL provider is registered in your application. The provider must be:
1. Installed in your host project (not the T4 template)
2. Registered in your app.config or web.config
3. Available at runtime when the T4 template executes

## Configuration

1. **Ensure MySQL provider is installed in your host project** (not in the T4 template)
2. **Register the provider in app.config/web.config** (see above)
3. **Set the database type in your T4 template Settings:**

```csharp
Settings.DatabaseType = DatabaseType.MySql;
Settings.ConnectionString = "Server=localhost;Database=mydatabase;Uid=myuser;Pwd=mypassword;";
```

### How It Works

The T4 template will:
1. Call `DbProviderFactories.GetFactory("MySql.Data.MySqlClient")` to get the provider factory
2. Pass the factory to `MySqlDatabaseReader` 
3. Use standard ADO.NET interfaces (`DbConnection`, `DbCommand`, etc.)

**Note**: By default, the generator looks for `MySql.Data.MySqlClient` provider. If you want to use `MySqlConnector` instead, you would need to register it with the invariant name `MySql.Data.MySqlClient` in your config (overriding the default provider).

## Data Type Mappings

### MySQL to C# Type Mappings

The following MySQL data types are mapped to C# types:

| MySQL Type | C# Type | Notes |
|------------|---------|-------|
| BIGINT | long | |
| BIGINT UNSIGNED | decimal | Mapped to decimal to handle full range |
| INT, INTEGER | int | |
| INT UNSIGNED | long | |
| SMALLINT | short | |
| SMALLINT UNSIGNED | int | |
| TINYINT | SByte | |
| TINYINT UNSIGNED | byte | |
| TINYINT(1) | bool | Special case for boolean |
| MEDIUMINT | int | |
| BIT | long | |
| BIT(1) | bool | Single bit treated as boolean |
| DECIMAL, NUMERIC | decimal | |
| FLOAT | double | |
| FLOAT UNSIGNED | decimal | |
| DOUBLE | double | |
| DOUBLE UNSIGNED | decimal | |
| REAL | double | |
| CHAR, VARCHAR | string | |
| TEXT | string | |
| TINYTEXT | string | |
| MEDIUMTEXT | string | |
| LONGTEXT | string | |
| BINARY, VARBINARY | byte[] | |
| BLOB | byte[] | |
| TINYBLOB | byte[] | |
| MEDIUMBLOB | byte[] | |
| LONGBLOB | byte[] | |
| ENUM | string | |
| SET | string | |
| DATE | DateTime | |
| DATETIME | DateTime | |
| TIMESTAMP | DateTime | |
| TIME | TimeSpan | |
| YEAR | short | |
| JSON | string | MySQL 5.7+ |
| GEOMETRY types | NetTopologySuite | If spatial types are enabled |

## Features

### Supported Features

✅ **Tables and Views**: Full support for reading schema, columns, and metadata  
✅ **Primary Keys**: Single and composite primary keys  
✅ **Foreign Keys**: Including CASCADE, RESTRICT, SET NULL rules  
✅ **Indexes**: Unique, non-unique, and primary key indexes  
✅ **AUTO_INCREMENT**: Mapped to Identity columns in EF  
✅ **Column Comments**: Mapped to extended properties  
✅ **Stored Procedures**: Full support with parameter detection  
✅ **Functions**: Scalar and table-valued functions  
✅ **Triggers**: Detection and documentation  
✅ **Generated Columns**: Computed columns (MySQL 5.7.6+)  
✅ **Multi-Result Sets**: Procedures returning multiple result sets  
✅ **ENUM Tables**: Reading enumeration tables  

### Limitations

❌ **Sequences**: MySQL does not have native SEQUENCE objects like Oracle or PostgreSQL (all versions including 8.0+). MySQL uses `AUTO_INCREMENT` on table columns instead, which is fully supported and automatically detected by the generator.  
❌ **Synonyms**: MySQL doesn't support database synonyms  
❌ **Temporal Tables**: MySQL doesn't have built-in temporal table support  
❌ **Memory-Optimized Tables**: Not applicable to MySQL  
❌ **Full-Text Indexes**: Not yet mapped (future enhancement)  

### Known Considerations

1. **Case Sensitivity**: Table and column names may be case-sensitive depending on the operating system and the `lower_case_table_names` setting:
   - Linux: Case-sensitive by default
   - Windows: Case-insensitive by default
   - macOS: Case-insensitive by default

2. **ENUM and SET Types**: These are mapped to `string` in C#. Consider using enum definitions in Settings for better type safety.

3. **Unsigned Types**: Unsigned integer types are mapped to the next larger signed type to prevent overflow (e.g., `INT UNSIGNED` → `long`).

4. **TINYINT(1)**: MySQL convention treats `TINYINT(1)` as boolean. The generator respects this convention.

5. **Stored Procedure Return Types**: The generator attempts to infer return types by executing procedures with NULL parameters. This may not work for:
   - Procedures requiring specific parameter values
   - Procedures with dynamic SQL
   - Procedures using temporary tables

6. **Generated Columns**: MySQL 5.7.6+ supports generated columns (similar to computed columns in SQL Server). These are detected using `GENERATION_EXPRESSION`.

## Testing Scenarios

### Basic Testing

1. **Simple Tables**
   ```sql
   CREATE TABLE customers (
       id INT AUTO_INCREMENT PRIMARY KEY,
       name VARCHAR(100) NOT NULL,
       email VARCHAR(100),
       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );
   ```

2. **Foreign Keys**
   ```sql
   CREATE TABLE orders (
       id INT AUTO_INCREMENT PRIMARY KEY,
       customer_id INT NOT NULL,
       order_date DATE,
       FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
   );
   ```

3. **Composite Keys**
   ```sql
   CREATE TABLE order_items (
       order_id INT,
       product_id INT,
       quantity INT,
       PRIMARY KEY (order_id, product_id),
       FOREIGN KEY (order_id) REFERENCES orders(id)
   );
   ```

4. **Views**
   ```sql
   CREATE VIEW customer_orders AS
   SELECT c.id, c.name, COUNT(o.id) as order_count
   FROM customers c
   LEFT JOIN orders o ON c.id = o.customer_id
   GROUP BY c.id, c.name;
   ```

5. **Stored Procedures**
   ```sql
   DELIMITER //
   CREATE PROCEDURE GetCustomerOrders(IN customer_id INT)
   BEGIN
       SELECT * FROM orders WHERE customer_id = customer_id;
   END //
   DELIMITER ;
   ```

6. **Functions**
   ```sql
   DELIMITER //
   CREATE FUNCTION GetOrderTotal(order_id INT) RETURNS DECIMAL(10,2)
   BEGIN
       DECLARE total DECIMAL(10,2);
       SELECT SUM(price * quantity) INTO total
       FROM order_items WHERE order_id = order_id;
       RETURN total;
   END //
   DELIMITER ;
   ```

### Advanced Testing

7. **ENUM Types**
   ```sql
   CREATE TABLE products (
       id INT AUTO_INCREMENT PRIMARY KEY,
       name VARCHAR(100),
       status ENUM('active', 'inactive', 'discontinued')
   );
   ```

8. **Generated Columns**
   ```sql
   CREATE TABLE rectangles (
       width DECIMAL(10,2),
       height DECIMAL(10,2),
       area DECIMAL(20,4) AS (width * height) STORED
   );
   ```

9. **Indexes**
   ```sql
   CREATE TABLE users (
       id INT AUTO_INCREMENT PRIMARY KEY,
       username VARCHAR(50) UNIQUE,
       email VARCHAR(100),
       INDEX idx_email (email)
   );
   ```

10. **Column Comments**
    ```sql
    CREATE TABLE products (
        id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Product unique identifier',
        name VARCHAR(100) COMMENT 'Product display name',
        price DECIMAL(10,2) COMMENT 'Price in USD'
    );
    ```

## Troubleshooting

### Connection Issues

**Problem**: Cannot connect to MySQL server  
**Solution**: 
- Verify server is running: `mysql -u root -p`
- Check firewall settings
- Ensure MySQL is listening on the correct port (default 3306)
- Verify connection string format

**Problem**: Access denied for user  
**Solution**: Grant required privileges (see Requirements section above)

### Schema Reading Issues

**Problem**: No tables are being generated  
**Solution**: 
- Verify database name in connection string
- Check that tables exist: `SHOW TABLES;`
- Ensure tables are not in excluded system databases
- Verify user has SELECT privileges on INFORMATION_SCHEMA

**Problem**: Foreign keys not detected  
**Solution**: 
- Verify foreign keys exist: `SHOW CREATE TABLE table_name;`
- Ensure InnoDB engine is used (MyISAM doesn't support foreign keys)
- Check INFORMATION_SCHEMA.KEY_COLUMN_USAGE for foreign key data

### Stored Procedure Issues

**Problem**: Stored procedures not generating return models  
**Solution**: 
- Verify procedures can execute with NULL parameters
- Use `Settings.ReadStoredProcReturnObjectException` callback to handle exceptions
- Consider manually specifying return types using `Settings.StoredProcedureReturnTypes`

**Problem**: Parameter types incorrect  
**Solution**: Check INFORMATION_SCHEMA.PARAMETERS for accurate type information

## Performance Considerations

1. **CommandTimeout**: For large databases, increase `Settings.CommandTimeout` (default 600 seconds)
   ```csharp
   Settings.CommandTimeout = 1200; // 20 minutes
   ```

2. **Schema Filtering**: Use filters to limit the tables/views being processed
   ```csharp
   SchemaReading = (tableName, tableType) => 
       !tableName.StartsWith("temp_") && !tableName.StartsWith("old_");
   ```

3. **Stored Procedure Inference**: The generator attempts to call stored procedures to infer return types. This can be slow for large databases. Consider:
   - Disabling stored procedure generation if not needed
   - Using manual return type specification

## Version Support

| MySQL Version | Support Status | Notes |
|--------------|----------------|-------|
| 5.7.x | ✅ Fully Supported | Primary target version |
| 8.0.x | ✅ Fully Supported | All 5.7 features + better performance |
| 5.6.x | ⚠️ Partial | May work but not officially tested |
| 5.5.x | ❌ Not Supported | Too old, missing required features |

## Future Enhancements

Potential future improvements:
- Sequence support for MySQL 8.0+
- Full-text index mapping
- Partitioned table detection
- Spatial index detection
- CHECK constraint support (MySQL 8.0.16+)

## Additional Resources

- [MySQL INFORMATION_SCHEMA Documentation](https://dev.mysql.com/doc/refman/5.7/en/information-schema.html)
- [MySQL Data Types](https://dev.mysql.com/doc/refman/5.7/en/data-types.html)
- [MySqlConnector Documentation](https://mysqlconnector.net/)
- [MySql.Data Documentation](https://dev.mysql.com/doc/connector-net/en/)

