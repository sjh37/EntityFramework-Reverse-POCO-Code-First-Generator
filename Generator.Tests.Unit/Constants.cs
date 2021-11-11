namespace Generator.Tests.Unit
{
    internal static class Constants
    {
        internal const string CI          = nameof(CI);
        internal const string Integration = nameof(Integration);
        internal const string Manual      = nameof(Manual);

        internal static class DbType
        {
            public const string PostgreSql = nameof(PostgreSql);
            public const string SqlServer  = nameof(SqlServer);
            public const string SqlCe      = nameof(SqlCe);
            public const string SqlLocalDb = nameof(SqlLocalDb);
        }
    }
}