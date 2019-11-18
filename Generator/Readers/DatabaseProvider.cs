using System;

namespace Efrpg.Readers
{
    public static class DatabaseProvider
    {
        public static string GetProvider()
        {
            return GetProvider(Settings.DatabaseType);
        }

        public static string GetProvider(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    return "System.Data.SqlClient";

                case DatabaseType.SqlCe:
                    return "System.Data.SqlServerCe.4.0";

                case DatabaseType.Plugin:
                    return string.Empty; // Not used

                case DatabaseType.MySql:
                    return "MySql.Data.MySqlClient";

                case DatabaseType.PostgreSQL:
                    return "Npgsql";

                case DatabaseType.Oracle:
                    return "Oracle.DataAccess.Client";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}