﻿namespace Efrpg
{
    public enum DatabaseType
    {
        SqlServer,
        SqlCe,
        SQLite,
        Plugin,     // See Settings.DatabaseReaderPlugin
        PostgreSQL,
        MySql,      // Not yet implemented
        Oracle      // Not yet implemented
    }
}