using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using Tester.Integration.EFCore8.Single_context_many_files;

namespace Tester.Integration.EFCore8;

[TestFixture]
[Category(Constants.Integration)]
[Category(Constants.DbType.SqlServer)]
public class ScalarValuedFunctionTests
{
    private EfCoreDbContext _db = null!;

    [SetUp]
    public void SetUp()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .Build();

        var conn = configuration.GetConnectionString("EfCoreDatabase");
        Assert.IsNotNull(conn);

        var optionsBuilder = new DbContextOptionsBuilder<EfCoreDbContext>()
            .UseSqlServer(conn!, x => x
                .UseNetTopologySuite()
                .UseHierarchyId());

        _db = new EfCoreDbContext(optionsBuilder.Options);
    }

    [Test]
    public void DoNotCallDirectly()
    {
        Assert.Throws<Exception>(() => _db.UdfNetSale(10, 79.0m, 0.1m));
    }

    [Test]
    public void UseWithinQuery()
    {
        var result = _db.ColumnNameAndTypes
            .Where(x => x.Adecimal != null)
            .Select(x => new
            {
                Sale = x.Adecimal!.Value,
                NetSale = _db.UdfNetSale(10, x.Adecimal.Value, 0.1m)
            })
            .ToList();

        Assert.IsNotNull(result);

        foreach (var row in result)
        {
            TestContext.Out.WriteLine($"Sale = {row.Sale} Net Sale = {row.NetSale}");
        }
    }
}