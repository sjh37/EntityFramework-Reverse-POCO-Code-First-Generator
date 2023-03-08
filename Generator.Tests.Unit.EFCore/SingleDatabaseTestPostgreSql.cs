using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit.EFCore
{
    [TestFixture]
    [NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.PostgreSql)]
    public class SingleDatabaseTestPostgreSql
    {
        /*[Test]
        public void Read_EfrpgTest_AllColumnTypes()
        {
            using var db = new MyEf6DbContext("Server=127.0.0.1;Port=5432;Database=EfrpgTest;User Id=testuser;Password=testtesttest;");
            var rows = db.Allcolumntypes.ToList();
            Assert.IsNotNull(rows);
            Assert.IsNotEmpty(rows);
            Assert.AreEqual(1234, rows.First().Bigint);
        }*/
    }
}