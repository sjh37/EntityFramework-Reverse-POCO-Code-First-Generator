namespace Generator.Tests.Unit
{
    using Efrpg.Readers;

    public static class FakeDatabaseReader
    {
        public static EfrpgResult CreateResult()
        {
            return new EfrpgResult
            {
                HasIdentityColumnSupport   = false,
                DoNotSpecifySizeForMaxLength = false,
                CanReadStoredProcedures    = true,
                IncludeSchema              = true
            };
        }
    }
}
