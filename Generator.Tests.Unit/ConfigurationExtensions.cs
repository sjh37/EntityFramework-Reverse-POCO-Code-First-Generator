using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Generator.Tests.Unit
{
    internal static class ConfigurationExtensions
    {

        internal static string GetConnectionString(string csName, string dbName = "", string dataSource = "")
        {
            var cs = ConfigurationManager.ConnectionStrings[csName]
                .Then(_ => _.EnsureIsNotNull($"ConnectionStrings[{csName}]"));
            
            var csBuilder = new SqlConnectionStringBuilder(cs.ConnectionString)
            {
                ApplicationName = "Generator"
            };

            if (!string.IsNullOrEmpty(dbName))
                csBuilder.InitialCatalog = dbName;

            if (!string.IsNullOrEmpty(dataSource))
                csBuilder.DataSource = dataSource;

            Console.WriteLine(csBuilder.ConnectionString);

            return csBuilder.ConnectionString;
        }
    }

}