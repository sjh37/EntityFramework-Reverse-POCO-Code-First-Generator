using System.Collections.Generic;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public class SqlServerCeDatabaseReader : DatabaseReader
    {
        public SqlServerCeDatabaseReader(DbProviderFactory factory, IDatabaseToPropertyType databaseToPropertyType)
            : base(factory, databaseToPropertyType)
        {
            StoredProcedureParameterDbType = new Dictionary<string, string>
            {
                { string.Empty,       "VarChar" }, // default
                { "hierarchyid",      "VarChar" },
                { "bigint",           "BigInt" },
                { "binary",           "Binary" },
                { "bit",              "Bit" },
                { "char",             "Char" },
                { "datetime",         "DateTime" },
                { "decimal",          "Decimal" },
                { "numeric",          "Decimal" },
                { "float",            "Float" },
                { "image",            "Image" },
                { "int",              "Int" },
                { "money",            "Money" },
                { "nchar",            "NChar" },
                { "ntext",            "NText" },
                { "nvarchar",         "NVarChar" },
                { "real",             "Real" },
                { "uniqueidentifier", "UniqueIdentifier" },
                { "smalldatetime",    "SmallDateTime" },
                { "smallint",         "SmallInt" },
                { "smallmoney",       "SmallMoney" },
                { "text",             "Text" },
                { "timestamp",        "Timestamp" },
                { "tinyint",          "TinyInt" },
                { "varbinary",        "VarBinary" },
                { "varchar",          "VarChar" },
                { "variant",          "Variant" },
                { "xml",              "Xml" },
                { "udt",              "Udt" },
                { "table type",       "Structured" },
                { "structured",       "Structured" },
                { "date",             "Date" },
                { "time",             "Time" },
                { "datetime2",        "DateTime2" },
                { "datetimeoffset",   "DateTimeOffset" }
            };
        }

        protected override string TableSQL()
        {
            return @"
SELECT  '' AS SchemaName,
    c.TABLE_NAME AS TableName,
    'BASE TABLE' AS TableType,
    CONVERT( tinyint, 0 ) AS TableTemporalType,
    c.ORDINAL_POSITION AS Ordinal,
    c.COLUMN_NAME AS ColumnName,
    CAST(CASE WHEN c.IS_NULLABLE = N'YES' THEN 1 ELSE 0 END AS BIT) AS IsNullable,
    CASE WHEN c.DATA_TYPE = N'rowversion' THEN 'timestamp' ELSE c.DATA_TYPE END AS TypeName,
    CASE WHEN c.CHARACTER_MAXIMUM_LENGTH IS NOT NULL THEN c.CHARACTER_MAXIMUM_LENGTH ELSE 0 END AS MaxLength,
    CASE WHEN c.NUMERIC_PRECISION IS NOT NULL THEN c.NUMERIC_PRECISION ELSE 0 END AS Precision,
    c.COLUMN_DEFAULT AS [Default],
    CASE WHEN c.DATA_TYPE = N'datetime' THEN 0 ELSE 0 END AS DateTimePrecision,
    CASE WHEN c.DATA_TYPE = N'datetime' THEN 0 WHEN c.NUMERIC_SCALE IS NOT NULL THEN c.NUMERIC_SCALE ELSE 0 END AS Scale,

    CAST(CASE WHEN c.AUTOINC_INCREMENT > 0 THEN 1 ELSE 0 END AS BIT) AS IsIdentity,
    CONVERT( bit, 0 ) as IsComputed,
    CONVERT( bit, 0 ) as IsRowGuid,
    CONVERT( tinyint, 0 ) AS GeneratedAlwaysType,
    CAST(CASE WHEN c.DATA_TYPE = N'rowversion' THEN 1 ELSE 0 END AS BIT) AS IsStoreGenerated,
    0 AS PrimaryKeyOrdinal,
    CAST(CASE WHEN u.TABLE_NAME IS NULL THEN 0 ELSE 1 END AS BIT) AS PrimaryKey,
    CONVERT( bit, 0 ) as IsForeignKey
FROM
    INFORMATION_SCHEMA.COLUMNS c
    INNER JOIN INFORMATION_SCHEMA.TABLES t ON c.TABLE_NAME = t.TABLE_NAME
    LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS cons ON cons.TABLE_NAME = c.TABLE_NAME
    LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON
        cons.CONSTRAINT_NAME = u.CONSTRAINT_NAME AND
        u.TABLE_NAME = c.TABLE_NAME AND
        u.COLUMN_NAME = c.COLUMN_NAME
WHERE
    t.TABLE_TYPE <> N'SYSTEM TABLE' AND
    cons.CONSTRAINT_TYPE = 'PRIMARY KEY'
ORDER BY
    c.TABLE_NAME,
    c.COLUMN_NAME,
    c.ORDINAL_POSITION";
        }

        protected override string ForeignKeySQL()
        {
            return @"
SELECT DISTINCT
    FK.TABLE_NAME AS FK_Table,
    FK.COLUMN_NAME AS FK_Column,
    PK.TABLE_NAME AS PK_Table,
    PK.COLUMN_NAME AS PK_Column,
    FK.CONSTRAINT_NAME AS Constraint_Name,
    '' AS fkSchema,
    '' AS pkSchema,
    PT.COLUMN_NAME AS primarykey,
    FK.ORDINAL_POSITION,
    CASE WHEN C.DELETE_RULE = 'CASCADE' THEN 1 ELSE 0 END AS CascadeOnDelete,
    CAST(0 AS BIT) AS IsNotEnforced
FROM    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS C
    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS FK
        ON FK.CONSTRAINT_NAME = C.CONSTRAINT_NAME
    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS PK
        ON PK.CONSTRAINT_NAME = C.UNIQUE_CONSTRAINT_NAME
            AND PK.ORDINAL_POSITION = FK.ORDINAL_POSITION
    INNER JOIN (
                SELECT  i1.TABLE_NAME,
                        i2.COLUMN_NAME
                FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                            ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                WHERE   i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
                ) PT
        ON PT.TABLE_NAME = PK.TABLE_NAME
WHERE   PT.COLUMN_NAME = PK.COLUMN_NAME
ORDER BY FK.TABLE_NAME, FK.COLUMN_NAME";
        }

        protected override string ExtendedPropertySQL()
        {
            return @"
SELECT  '' AS [schema],
        [ObjectName] AS [column],
        [ParentName] AS [table],
        [Value] AS [property]
FROM    [__ExtendedProperties]";
        }

        protected override string DoesExtendedPropertyTableExistSQL()
        {
            return @"
SELECT  1
FROM    INFORMATION_SCHEMA.TABLES
WHERE   TABLE_NAME = '__ExtendedProperties'";
        }

        protected override string IndexSQL()
        {
            return string.Empty;
        }

        public override bool CanReadStoredProcedures()
        {
            return false;
        }

        protected override string StoredProcedureSQL()
        {
            return string.Empty;
        }

        protected override string ReadDatabaseEditionSQL()
        {
            return string.Empty;
        }

        protected override string MultiContextSQL()
        {
            // You can add extra fields to these tables and they will be read in and stored in a Dictionary<string,string>() for you to access and process.
            // Therefore, using an * for the fields as we want to read in all the fields.
            return @"
SELECT * FROM MultiContext.Context;
SELECT * FROM MultiContext.[Table];
SELECT * FROM MultiContext.[Column];
SELECT * FROM MultiContext.StoredProcedure;
SELECT * FROM MultiContext.[Function];
SELECT * FROM MultiContext.Enumeration;
SELECT * FROM MultiContext.ForeignKey;";
        }

        protected override string EnumSQL(string table, string nameField, string valueField)
        {
            return string.Format("SELECT {0} as NameField, {1} as ValueField FROM {2};", nameField, valueField, table);
        }

        protected override string SynonymTableSQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymTableSQL()
        {
            return string.Empty;
        }

        protected override string SynonymForeignKeySQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymForeignKeySQL()
        {
            return string.Empty;
        }

        protected override string SynonymStoredProcedureSQLSetup()
        {
            return string.Empty;
        }

        protected override string SynonymStoredProcedureSQL()
        {
            return string.Empty;
        }

        protected override string SpecialQueryFlags()
        {
            return string.Empty;
        }

        protected override bool HasTemporalTableSupport()
        {
            return false;
        }

        public override void ReadStoredProcReturnObjects(List<StoredProcedure> procs)
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            base.Init();
            Settings.PrependSchemaName = false;
            IncludeSchema = false;
            DoNotSpecifySizeForMaxLength = true;
        }
    }
}