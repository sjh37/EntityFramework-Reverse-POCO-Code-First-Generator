namespace Generator.Tests.Unit
{
    using System.Data.Common;

    public class FakeDbProviderFactory : DbProviderFactory
    {
        public override DbConnection CreateConnection()
        {
            return new FakeDbConnection();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new FakeDbDataAdapter();
        }

        public override DbParameter CreateParameter()
        {
            return new FakeDbParameter();
        }
    }
}