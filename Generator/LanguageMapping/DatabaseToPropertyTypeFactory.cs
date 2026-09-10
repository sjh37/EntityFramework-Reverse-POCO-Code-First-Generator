using System;

namespace Efrpg.LanguageMapping
{
    public static class DatabaseToPropertyTypeFactory
    {
        /// <summary>
        ///     The type map that turns this database's column types into C# property types.
        /// </summary>
        public static IDatabaseToPropertyType Create()
        {
            switch (Settings.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    return new SqlServerToCSharp();

                case DatabaseType.SQLite:
                    return new SqLiteToCSharp();

                case DatabaseType.MySql:
                    return new MySqlToCSharp();

                case DatabaseType.PostgreSQL:
                    return new PostgresToCSharp();

                case DatabaseType.Oracle:
                    return new OracleToCSharp();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
