namespace Generator.Tests.Unit
{
    using System.Data;
    using System.Data.Common;

    public class FakeDbTransaction : DbTransaction
    {
        public override void Commit() { }
        public override void Rollback() { }
        protected override DbConnection DbConnection { get; }
        public override IsolationLevel IsolationLevel { get; }
    }
}