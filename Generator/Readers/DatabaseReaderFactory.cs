using System;
using System.Data.Common;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public static class DatabaseReaderFactory
    {
        public static DatabaseReader Create(DbProviderFactory factory)
        {
            var databaseToPropertyType = DatabaseToPropertyTypeFactory.Create();

            switch (Settings.DatabaseType)
            {
                case DatabaseType.SqlServer:
                case DatabaseType.SQLite:
                    return new SqlServerDatabaseReader(factory, databaseToPropertyType);

                case DatabaseType.SqlCe:
                    return new SqlServerCeDatabaseReader(factory, databaseToPropertyType);

                case DatabaseType.Plugin:
                    if(string.IsNullOrWhiteSpace(Settings.DatabaseReaderPlugin))
                        throw new ArgumentOutOfRangeException();
                    return new PluginDatabaseReader(null);

                case DatabaseType.MySql:
                    return new MySqlDatabaseReader(factory, databaseToPropertyType);

                case DatabaseType.PostgreSQL:
                    return new PostgreSqlDatabaseReader(factory, databaseToPropertyType);

                case DatabaseType.Oracle:
                    return new OracleDatabaseReader(factory, databaseToPropertyType);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}