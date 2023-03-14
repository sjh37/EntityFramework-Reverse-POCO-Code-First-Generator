using System;
using Efrpg.LanguageMapping.LanguageFactories;

namespace Efrpg.LanguageMapping
{
    public static class DatabaseToPropertyTypeFactory
    {
        public static IDatabaseToPropertyType Create()
        {
            var factory = CreateDatabaseLanguageFactory();
            return factory.Create();
        }

        private static IDatabaseLanguageFactory CreateDatabaseLanguageFactory()
        {
            switch (Settings.DatabaseType)
            {
                case DatabaseType.SqlServer:
                case DatabaseType.SqlCe:
                    return new SqlServerLanguageFactory();

                case DatabaseType.SQLite:
                    return new SQLiteLanguageFactory();

                case DatabaseType.Plugin:
                    return new PluginLanguageFactory();

                case DatabaseType.MySql:
                    return new MySqlLanguageFactory();

                case DatabaseType.PostgreSQL:
                    return new PostgresLanguageFactory();

                case DatabaseType.Oracle:
                    return new OracleLanguageFactory();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}