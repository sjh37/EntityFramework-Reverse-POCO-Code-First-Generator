# Oracle Database Support - Implementation Notes

## Overview

The EntityFramework Reverse POCO Generator now supports Oracle databases (version 11g and later) with full support for:
- Tables and Views
- Columns with all standard Oracle data types
- Primary Keys (single and composite)
- Foreign Keys with CASCADE and enforcement rules
- Indexes (unique, non-unique, primary)
- Column Comments (via ALL_COL_COMMENTS)
- Stored Procedures and Functions (including packages)
- Triggers
- Sequences
- Synonyms
- Identity Columns (Oracle 12c+)
- Virtual Columns (computed columns)

## Requirements

### Minimum Oracle Version
- **Oracle 11g+** for full compatibility
- **Oracle 12c+** for Identity Column support
- **Oracle 19c+** for enhanced features

### Required Oracle Privileges

The database user specified in the connection string must have the following privileges:

```sql
GRANT SELECT ON ALL_TABLES TO your_user;
GRANT SELECT ON ALL_TAB_COLUMNS TO your_user;
GRANT SELECT ON ALL_VIEWS TO your_user;
GRANT SELECT ON ALL_CONSTRAINTS TO your_user;
GRANT SELECT ON ALL_CONS_COLUMNS TO your_user;
GRANT SELECT ON ALL_INDEXES TO your_user;
GRANT SELECT ON ALL_IND_COLUMNS TO your_user;
GRANT SELECT ON ALL_COL_COMMENTS TO your_user;
GRANT SELECT ON ALL_PROCEDURES TO your_user;
GRANT SELECT ON ALL_ARGUMENTS TO your_user;
GRANT SELECT ON ALL_SEQUENCES TO your_user;
GRANT SELECT ON ALL_TRIGGERS TO your_user;
GRANT SELECT ON ALL_SYNONYMS TO your_user;
GRANT SELECT ON ALL_TAB_IDENTITY_COLS TO your_user;  -- Oracle 12c+
GRANT SELECT ON V$VERSION TO your_user;
GRANT SELECT ON V$INSTANCE TO your_user;
```

### Connection String Format

The connection string format for Oracle:

```csharp
// Basic connection with TNS alias
Settings.ConnectionString = "Data Source=ORCL;User Id=myuser;Password=mypassword;";

// Full connection descriptor (no TNS required)
Settings.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=myuser;Password=mypassword;";

// With connection pooling
Settings.ConnectionString = "Data Source=ORCL;User Id=myuser;Password=mypassword;Pooling=true;Min Pool Size=0;Max Pool Size=100;";

// With DBA role
Settings.ConnectionString = "Data Source=ORCL;User Id=system;Password=password;DBA Privilege=SYSDBA;";
```

### Registering Oracle Provider in app.config/web.config

For Oracle.ManagedDataAccess, the provider is usually registered automatically. If needed, you can register it explicitly:

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

### Required Database Provider

Since this generator is packaged as a T4 template, **you do not install NuGet packages in the template itself**. Instead, the database provider must be available in **your host project**:

**Option 1: Oracle.ManagedDataAccess (.NET Framework)**
- Install in your project: `Install-Package Oracle.ManagedDataAccess`
- Automatically registers the provider

**Option 2: Oracle.ManagedDataAccess.Core (.NET Core/.NET 5+)**
- Install in your project: `Install-Package Oracle.ManagedDataAccess.Core`
- Register in app.config/web.config

**Important**: The T4 template uses `DbProviderFactory` to access whatever Oracle provider is registered in your application. The provider must be:
1. Installed in your host project (not the T4 template)
2. Registered in your app.config or web.config (if required)
3. Available at runtime when the T4 template executes

## Configuration

1. **Ensure Oracle provider is installed in your host project** (not in the T4 template)
2. **Register the provider in app.config/web.config if needed** (usually automatic for Oracle.ManagedDataAccess)
3. **Set the database type in your T4 template Settings:**

```csharp
Settings.DatabaseType = DatabaseType.Oracle;
Settings.ConnectionString = "Data Source=ORCL;User Id=myuser;Password=mypassword;";
```

### How It Works

The T4 template will:
1. Call `DbProviderFactories.GetFactory("Oracle.ManagedDataAccess.Client")` to get the provider factory
2. Pass the factory to `OracleDatabaseReader`
3. Use standard ADO.NET interfaces (`DbConnection`, `DbCommand`, etc.)

**Note**: Oracle.ManagedDataAccess typically registers itself automatically when installed, so no manual configuration is usually needed.

## Data Type Mappings

### Oracle to C# Type Mappings

The following Oracle data types are mapped to C# types:

| Oracle Type | C# Type | Notes |
|------------|---------|-------|
| NUMBER | decimal | Generic number type |
| NUMBER(p,0) | int/long | Integer types based on precision |
| NUMBER(p,s) | decimal | Decimal types with scale |
| BINARY_INTEGER | long | |
| PLS_INTEGER | long | |
| BINARY_DOUBLE | decimal | |
| BINARY_FLOAT | double | |
| FLOAT | double | |
| REAL | float | |
| VARCHAR2 | string | |
| NVARCHAR2 | string | Unicode support |
| CHAR | string | |
| NCHAR | string | Unicode support |
| CLOB | string | Character large object |
| NCLOB | string | Unicode CLOB |
| LONG | long | |
| RAW | byte[] | Binary data |
| LONG RAW | byte[] | Binary data |
| BLOB | byte[] | Binary large object |
| DATE | DateTime | Date and time |
| TIMESTAMP | DateTime | High precision timestamp |
| TIMESTAMP WITH TIME ZONE | DateTime | With timezone |
| TIMESTAMP WITH LOCAL TIME ZONE | DateTime | Local timezone |
| INTERVAL DAY TO SECOND | decimal | Time interval |
| INTERVAL YEAR TO MONTH | decimal | Time interval |
| ROWID | string | Physical row address |
| UROWID | string | Universal ROWID |
| XMLTYPE | string | XML data |
| BFILE | byte[] | External file reference |

## Features

### Supported Features

✅ **Tables and Views**: Full support for reading schema, columns, and metadata  
✅ **Primary Keys**: Single and composite primary keys  
✅ **Foreign Keys**: Including CASCADE, SET NULL, and enforcement status  
✅ **Indexes**: Unique, non-unique, and primary key indexes  
✅ **Identity Columns**: Oracle 12c+ GENERATED ALWAYS/BY DEFAULT  
✅ **Column Comments**: Full support via ALL_COL_COMMENTS  
✅ **Stored Procedures**: Full support with parameter detection  
✅ **Functions**: Scalar and complex return types  
✅ **Packages**: Detection of packaged procedures and functions  
✅ **Triggers**: Detection and documentation  
✅ **Sequences**: Full support with min/max/increment metadata  
✅ **Synonyms**: Reading tables through synonyms  
✅ **Virtual Columns**: Computed columns  
✅ **Temporary Tables**: Detection of temporary tables  

### Limitations

❌ **REF CURSOR Return Types**: Requires manual configuration or execution  
❌ **Object Types**: User-defined types need manual mapping  
❌ **Nested Tables**: Not automatically mapped  
❌ **Packages with Overloaded Procedures**: May require manual configuration  
❌ **Temporal Tables**: Oracle doesn't have built-in temporal table support  
❌ **Memory-Optimized Tables**: Not applicable to Oracle  

### Known Considerations

1. **Schema Awareness**: Oracle is highly schema-aware. The generator will read from the current schema by default (retrieved via `SYS_CONTEXT('USERENV','CURRENT_SCHEMA')`).

2. **Case Sensitivity**: Oracle stores object names in UPPERCASE by default unless quoted identifiers are used. The generator respects this convention.

3. **NUMBER Type Precision**: Oracle's NUMBER type is very flexible:
   - `NUMBER` (no precision/scale) → `decimal`
   - `NUMBER(p,0)` where p ≤ 9 → `int`
   - `NUMBER(p,0)` where p ≤ 18 → `long`
   - `NUMBER(p,s)` where s > 0 → `decimal`

4. **Identity Columns**: Supported in Oracle 12c+ using `GENERATED ALWAYS AS IDENTITY` or `GENERATED BY DEFAULT AS IDENTITY`.

5. **Sequences**: Oracle extensively uses sequences for identity/auto-increment behavior. The generator detects sequences but doesn't automatically link them to columns (consider manual mapping).

6. **Packages**: Oracle packages can contain multiple procedures and functions. The generator reads them but may need manual configuration for overloaded routines.

7. **REF CURSORS**: Procedures returning REF CURSORS require special handling. Consider using `Settings.StoredProcedureReturnTypes` for manual specification.

8. **Synonyms**: The generator can read tables through synonyms, providing access to tables in other schemas.

9. **System Schemas**: The following system schemas are excluded by default:
   - SYS, SYSTEM, OUTLN, DBSNMP, APPQOSSYS, DBSFWUSER, GGSYS
   - ANONYMOUS, CTXSYS, DVSYS, DVF, GSMADMIN_INTERNAL, LBACSYS
   - MDSYS, OLAPSYS, ORACLE_OCM, ORDDATA, ORDPLUGINS, ORDSYS
   - SI_INFORMTN_SCHEMA, SPATIAL_CSW_ADMIN_USR, SPATIAL_WFS_ADMIN_USR
   - WMSYS, XDB, XS$NULL, FLOWS_FILES, APEX_*, RMAN$CATALOG

## Testing Scenarios

### Basic Testing

1. **Simple Tables**
   ```sql
   CREATE TABLE customers (
       id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
       name VARCHAR2(100) NOT NULL,
       email VARCHAR2(100),
       created_at TIMESTAMP DEFAULT SYSTIMESTAMP
   );
   ```

2. **Foreign Keys**
   ```sql
   CREATE TABLE orders (
       id NUMBER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
       customer_id NUMBER NOT NULL,
       order_date DATE,
       CONSTRAINT fk_customer FOREIGN KEY (customer_id) 
           REFERENCES customers(id) ON DELETE CASCADE
   );
   ```

3. **Composite Keys**
   ```sql
   CREATE TABLE order_items (
       order_id NUMBER,
       product_id NUMBER,
       quantity NUMBER,
       PRIMARY KEY (order_id, product_id),
       CONSTRAINT fk_order FOREIGN KEY (order_id) REFERENCES orders(id)
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

5. **Sequences**
   ```sql
   CREATE SEQUENCE customer_seq
       START WITH 1
       INCREMENT BY 1
       MINVALUE 1
       MAXVALUE 999999999
       CACHE 20
       NOCYCLE;
   ```

6. **Stored Procedures**
   ```sql
   CREATE OR REPLACE PROCEDURE GetCustomerOrders(
       p_customer_id IN NUMBER,
       p_result OUT SYS_REFCURSOR
   ) AS
   BEGIN
       OPEN p_result FOR
       SELECT * FROM orders WHERE customer_id = p_customer_id;
   END;
   /
   ```

7. **Functions**
   ```sql
   CREATE OR REPLACE FUNCTION GetOrderTotal(p_order_id NUMBER) 
   RETURN NUMBER AS
       v_total NUMBER;
   BEGIN
       SELECT SUM(price * quantity) INTO v_total
       FROM order_items WHERE order_id = p_order_id;
       RETURN v_total;
   END;
   /
   ```

### Advanced Testing

8. **Packages**
   ```sql
   CREATE OR REPLACE PACKAGE order_pkg AS
       PROCEDURE create_order(p_customer_id NUMBER);
       FUNCTION get_order_count(p_customer_id NUMBER) RETURN NUMBER;
   END order_pkg;
   /
   
   CREATE OR REPLACE PACKAGE BODY order_pkg AS
       PROCEDURE create_order(p_customer_id NUMBER) AS
       BEGIN
           INSERT INTO orders (customer_id, order_date) 
           VALUES (p_customer_id, SYSDATE);
       END;
       
       FUNCTION get_order_count(p_customer_id NUMBER) RETURN NUMBER AS
           v_count NUMBER;
       BEGIN
           SELECT COUNT(*) INTO v_count 
           FROM orders WHERE customer_id = p_customer_id;
           RETURN v_count;
       END;
   END order_pkg;
   /
   ```

9. **Virtual Columns**
   ```sql
   CREATE TABLE rectangles (
       width NUMBER(10,2),
       height NUMBER(10,2),
       area NUMBER(20,4) GENERATED ALWAYS AS (width * height) VIRTUAL
   );
   ```

10. **Indexes**
    ```sql
    CREATE TABLE users (
        id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
        username VARCHAR2(50) UNIQUE,
        email VARCHAR2(100)
    );
    CREATE INDEX idx_email ON users(email);
    ```

11. **Column Comments**
    ```sql
    COMMENT ON TABLE products IS 'Product catalog';
    COMMENT ON COLUMN products.id IS 'Product unique identifier';
    COMMENT ON COLUMN products.name IS 'Product display name';
    COMMENT ON COLUMN products.price IS 'Price in USD';
    ```

12. **Synonyms**
    ```sql
    -- Create synonym for table in another schema
    CREATE SYNONYM prod_customers FOR production.customers;
    ```

13. **Triggers**
    ```sql
    CREATE OR REPLACE TRIGGER audit_customers
        BEFORE UPDATE ON customers
        FOR EACH ROW
    BEGIN
        :NEW.updated_at := SYSTIMESTAMP;
    END;
    /
    ```

## Troubleshooting

### Connection Issues

**Problem**: Cannot connect to Oracle server  
**Solution**: 
- Verify server is running: `sqlplus user/password@ORCL`
- Check TNS configuration in `tnsnames.ora`
- Ensure listener is running: `lsnrctl status`
- Verify connection string format

**Problem**: ORA-12154: TNS:could not resolve the connect identifier  
**Solution**: 
- Verify TNS alias exists in `tnsnames.ora`
- Use full connection descriptor in connection string
- Check `TNS_ADMIN` environment variable

**Problem**: ORA-01017: invalid username/password  
**Solution**: 
- Verify credentials
- Check if account is locked: `SELECT username, account_status FROM dba_users;`
- Ensure proper case sensitivity for usernames

### Schema Reading Issues

**Problem**: No tables are being generated  
**Solution**: 
- Verify schema name: `SELECT SYS_CONTEXT('USERENV','CURRENT_SCHEMA') FROM DUAL;`
- Check that tables exist: `SELECT * FROM USER_TABLES;`
- Verify user has SELECT privileges on ALL_* views
- Check that tables are not in excluded system schemas

**Problem**: Foreign keys not detected  
**Solution**: 
- Verify constraints exist: `SELECT * FROM USER_CONSTRAINTS WHERE constraint_type = 'R';`
- Ensure user has access to ALL_CONSTRAINTS and ALL_CONS_COLUMNS
- Check constraint status (ENABLED/DISABLED)

**Problem**: Identity columns not detected  
**Solution**: 
- Verify Oracle version (12c+ required for identity columns)
- Check ALL_TAB_IDENTITY_COLS view exists and is accessible
- Older versions should use sequences with triggers

### Stored Procedure Issues

**Problem**: Procedures not generating return models  
**Solution**: 
- Verify procedures are accessible: `SELECT * FROM USER_PROCEDURES;`
- For REF CURSORS, use `Settings.StoredProcedureReturnTypes` for manual specification
- Check ALL_ARGUMENTS for parameter metadata

**Problem**: Package procedures not detected correctly  
**Solution**: 
- Verify packages are compiled: `SELECT * FROM USER_OBJECTS WHERE object_type = 'PACKAGE BODY';`
- Check for overloaded procedures (may require manual configuration)
- Ensure ALL_ARGUMENTS contains package procedure data

**Problem**: Functions not returning correct type  
**Solution**: 
- Verify return type in ALL_ARGUMENTS (position 0)
- Check for PL/SQL table or record return types (requires manual mapping)

### Performance Issues

**Problem**: Schema reading is very slow  
**Solution**: 
- Oracle dictionary queries can be slow on large schemas
- Increase `Settings.CommandTimeout` (default 600 seconds)
- Consider using schema filtering to limit tables
- Ensure statistics are up to date: `EXEC DBMS_STATS.GATHER_SCHEMA_STATS('YOUR_SCHEMA');`

## Performance Considerations

1. **CommandTimeout**: For large databases, increase `Settings.CommandTimeout`
   ```csharp
   Settings.CommandTimeout = 1800; // 30 minutes
   ```

2. **Schema Filtering**: Use filters to limit the objects being processed
   ```csharp
   SchemaReading = (tableName, tableType) => 
       !tableName.StartsWith("OLD_") && !tableName.StartsWith("TEMP_");
   ```

3. **Dictionary Views**: The ALL_* views can be slow. For better performance:
   - Use USER_* views if only reading from current schema
   - Ensure database statistics are current
   - Consider creating indexes on filtered columns if permitted

4. **Connection Pooling**: Enable connection pooling for better performance
   ```csharp
   Settings.ConnectionString = "Data Source=ORCL;User Id=myuser;Password=mypassword;Pooling=true;Min Pool Size=10;Max Pool Size=100;";
   ```

## Version Support

| Oracle Version | Support Status | Notes |
|----------------|----------------|-------|
| 11g (11.2.0.4) | ✅ Fully Supported | Base version, no identity columns |
| 12c (12.1, 12.2) | ✅ Fully Supported | Adds identity column support |
| 18c | ✅ Fully Supported | Enhanced JSON support |
| 19c | ✅ Fully Supported | Long-term support release |
| 21c | ✅ Fully Supported | Latest features |
| 10g and earlier | ❌ Not Supported | Too old, missing required features |

## Best Practices

1. **Use Identity Columns (12c+)**: Prefer `GENERATED ALWAYS AS IDENTITY` over manual sequences with triggers.

2. **Document with Comments**: Use `COMMENT ON TABLE/COLUMN` statements for better generated code documentation.

3. **Name Constraints**: Always name your foreign key and unique constraints for better readability.

4. **Sequence Configuration**: When using sequences manually, configure them in `Settings.HiLoSequences` for proper mapping.

5. **Package Organization**: Group related procedures/functions in packages for better organization.

6. **REF CURSOR Handling**: For procedures with REF CURSOR out parameters, use manual return type specification:
   ```csharp
   Settings.StoredProcedureReturnTypes.Add("GetCustomerOrders", "Customer");
   ```

7. **Schema Filtering**: Always implement schema filtering for large databases to improve performance:
   ```csharp
   Settings.PrependSchemaName = false; // For single-schema applications
   ```

## Future Enhancements

Potential future improvements:
- Enhanced REF CURSOR analysis
- Object type mapping (UDTs)
- Nested table support
- Advanced package handling (overloaded procedures)
- Pipelined function support
- External table detection

## Additional Resources

- [Oracle Data Access Components Documentation](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/)
- [Oracle Database SQL Language Reference](https://docs.oracle.com/en/database/oracle/oracle-database/19/sqlrf/)
- [Oracle PL/SQL Language Reference](https://docs.oracle.com/en/database/oracle/oracle-database/19/lnpls/)
- [ALL_TABLES Documentation](https://docs.oracle.com/en/database/oracle/oracle-database/19/refrn/ALL_TABLES.html)
- [Oracle Identity Columns](https://oracle-base.com/articles/12c/identity-columns-in-oracle-12cr1)

