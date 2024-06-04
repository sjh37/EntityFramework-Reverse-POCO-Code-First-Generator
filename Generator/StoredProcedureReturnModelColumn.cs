namespace Efrpg
{
    // https://learn.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql?view=sql-server-ver16
    public class StoredProcedureReturnModelColumn
    {
        public bool IsHidden; // is_hidden BIT,
        public int Ordinal; // column_ordinal INT,
        public string Name; // name NVARCHAR(128),
        public int SystemTypeId; // system_type_id INT, SELECT * FROM sys.types
        public string SqlPropertyType; // system_type_name NVARCHAR(128), decimal(18,0), datetime2(7), varchar(50), int, float, etc.
        public int MaxLength; // max_length SMALLINT,
        public int Precision; // precision TINYINT,
        public int Scale; // scale TINYINT,
        public bool IsNullable; // is_nullable BIT,

        public string PropertyType;
    }
}