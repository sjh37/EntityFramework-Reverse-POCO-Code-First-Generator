namespace Efrpg
{
    // Parallel copy of the enum the dotnet tool declares. The two must stay identical: the value crosses
    // the process boundary by name, never by ordinal.
    public enum DatabaseType
    {
        SqlServer,
        SQLite,
        PostgreSQL,
        MySql,
        Oracle
    }
}