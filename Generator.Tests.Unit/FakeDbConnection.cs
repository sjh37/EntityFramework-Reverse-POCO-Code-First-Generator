namespace Generator.Tests.Unit
{
    using System.Data;
    using System.Data.Common;

    public class FakeDbConnection : DbConnection
    {
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return new FakeDbTransaction();
        }

        public override void Close() { }
        public override void ChangeDatabase(string databaseName) { }
        public override void Open() { }
        public override string ConnectionString { get; set; }
        public override string Database { get; }
        public override ConnectionState State { get; }
        public override string DataSource { get; }
        public override string ServerVersion { get; }

        protected override DbCommand CreateDbCommand()
        {
            return null;
        }
    }
}