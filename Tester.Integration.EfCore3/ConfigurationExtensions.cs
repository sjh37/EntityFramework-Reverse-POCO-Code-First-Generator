using EntityFramework_Reverse_POCO_Generator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Linq;

namespace Tester.Integration.EfCore3
{
    internal static class ConfigurationExtensions
    {
        private static IConfiguration _configuration;

        internal static string GetConnectionString(string csName, string dbName = "")
        {
            _configuration ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddUserSecrets<FakeDbSetTests>()
                .Build();

            var cs = _configuration.GetConnectionString(csName)
                .Then(_ => _.EnsureIsNotNull($"ConnectionStrings[{csName}]"));

            var csBuilder = new SqlConnectionStringBuilder(cs);

            if (!string.IsNullOrEmpty(dbName))
                csBuilder.InitialCatalog = dbName;

            return csBuilder.ConnectionString;
        }

        internal static MyDbContext CreateMyDbContext()
        {
            var builder = new DbContextOptionsBuilder(new DbContextOptions<MyDbContext>());
            builder.UseLoggerFactory(loggerFactory);
            builder.UseSqlServer(GetConnectionString(nameof(MyDbContext)));

            return new MyDbContext((DbContextOptions<MyDbContext>)builder.Options);
        }

        internal static FredDbContext CreateFredDbContext()
        {
            var builder = new DbContextOptionsBuilder(new DbContextOptions<FredDbContext>());

            builder.EnableSensitiveDataLogging();

            builder.UseLoggerFactory(loggerFactory);

            builder.UseSqlServer(GetConnectionString(nameof(MyDbContext), "fred"));

            return new FredDbContext((DbContextOptions<FredDbContext>)builder.Options);
        }
        
        internal static TestDatabaseStandard.TestDbContext CreateTestDbContext(string dbName = "")
        {
            var builder = new DbContextOptionsBuilder(new DbContextOptions<TestDatabaseStandard.TestDbContext>());
            builder.UseSqlServer(GetConnectionString("EfCoreDatabase", dbName ?? "EfrpgTest"));

            return new TestDatabaseStandard.TestDbContext((DbContextOptions<TestDatabaseStandard.TestDbContext>)builder.Options);
        }

        internal static TestSynonymsDatabase.TestDbContext CreateSynonymsDbContext()
        {
            var builder = new DbContextOptionsBuilder(new DbContextOptions<TestSynonymsDatabase.TestDbContext>());
            builder.UseSqlServer(GetConnectionString("EfCoreDatabase", "EfrpgTest_Synonyms"));

            return new TestSynonymsDatabase.TestDbContext((DbContextOptions<TestSynonymsDatabase.TestDbContext>)builder.Options);
        }
        
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider( 
                new OptionsMonitor<ConsoleLoggerOptions>(
                    new OptionsFactory<ConsoleLoggerOptions>(
                        Enumerable.Empty<IConfigureOptions<ConsoleLoggerOptions>>(), 
                        Enumerable.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>()), 
                    Enumerable.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>(), 
                    new OptionsCache<ConsoleLoggerOptions>()) ) }
        );
    }
}