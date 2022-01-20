namespace Generator.Tests.Integration
{
    using Efrpg;
    using Efrpg.FileManagement;
    using Efrpg.Templates;
    using Generator.Tests.Common;
    using NUnit.Framework;

    [TestFixture, NonParallelizable]
    [Category(Constants.Integration)]
    [Category(Constants.DbType.SqlCe)]
    public class SingleDatabaseTestSqlCeServer : SingleDatabaseTestBase
    {
        public void SetupSqlCe(string database, string connectionStringName, string dbContextName, TemplateType templateType, GeneratorType generatorType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            SetupDatabase(connectionStringName, dbContextName, templateType, generatorType, foreignKeyNamingStrategy);
           
            Settings.ConnectionString = @"Data Source=C:\S\Source (open source)\EntityFramework Reverse POCO Code Generator\EntityFramework.Reverse.POCO.Generator\App_Data\" + database;
            Settings.DatabaseType     = DatabaseType.SqlCe;
        }

        [Test, NonParallelizable]
        [TestCase(ForeignKeyNamingStrategy.Legacy)]
        //[TestCase(ForeignKeyNamingStrategy.Latest)]
        public void ReverseEngineerSqlCe(ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = false;
            Settings.UseMappingTables = true;
            SetupSqlCe("NorthwindSqlCe40.sdf", "MyDbContext", "MyDbContext", TemplateType.Ef6, GeneratorType.Ef6, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            Run(filename, ".SqlCE", typeof(NullFileManager), null);

            // Assert
            CompareAgainstTestComparison(filename);
        }
        
        [Test, NonParallelizable]
        // Legacy
        [TestCase(false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase(false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        [TestCase(true, TemplateType.EfCore2, ForeignKeyNamingStrategy.Legacy)]
        [TestCase(true, TemplateType.EfCore3, ForeignKeyNamingStrategy.Legacy)]
        // Latest
        //[TestCase(false, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest)]
        //[TestCase(false, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest)]
        //[TestCase(true, TemplateType.EfCore2, ForeignKeyNamingStrategy.Latest)]
        //[TestCase(true, TemplateType.EfCore3, ForeignKeyNamingStrategy.Latest)]
        public void ReverseEngineerSqlCe_EfCore(bool separateFiles, TemplateType templateType, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            // Arrange
            Settings.GenerateSeparateFiles = separateFiles;
            Settings.UseMappingTables = false;
            SetupSqlCe("NorthwindSqlCe40.sdf", "MyDbContext", "MyDbContext", templateType, GeneratorType.EfCore, foreignKeyNamingStrategy);

            // Act
            var filename = "Northwind";
            var subFolder = templateType == TemplateType.EfCore2 ? "TestComparison\\EfCore2NorthwindSqlCe40" : "TestComparison\\EfCore3NorthwindSqlCe40";
            Run(filename, ".SqlCE", typeof(EfCoreFileManager), subFolder);

            // Assert
            if (separateFiles)
                CompareAgainstFolderTestComparison(subFolder);
            else
                CompareAgainstTestComparison(filename);
        }
    }
}