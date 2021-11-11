namespace Generator.Tests.Common
{
    public static class Constants
    {
        public const string CI          = nameof(CI);
        public const string Integration = nameof(Integration);
        public const string Manual      = nameof(Manual);

        public static class DbType
        {
            public const string PostgreSql = nameof(PostgreSql);
            public const string SqlServer  = nameof(SqlServer);
            public const string SqlCe      = nameof(SqlCe);
            public const string SqlLocalDb = nameof(SqlLocalDb);
        }
    }
}