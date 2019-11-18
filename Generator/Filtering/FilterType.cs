namespace Efrpg.Filtering
{
    public enum FilterType
    {
        Schema,         // Can only be used on Schema
        Table,          // Can only used on Tables
        Column,         // Can only used on Columns
        StoredProcedure // Can only used on Stored Procedures
    }
}