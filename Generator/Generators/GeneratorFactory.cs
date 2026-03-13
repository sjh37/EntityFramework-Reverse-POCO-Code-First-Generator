using System;
using System.Data;
using System.Data.Common;
using Efrpg.FileManagement;
using Efrpg.Readers;
using Efrpg.Templates;

namespace Efrpg.Generators
{

    public static class GeneratorFactory
    {
        public static Generator Create(FileManagementService fileManagementService, Type fileManagerType, string singleDbContextSubNamespace = null)
        {
            Generator generator;

            switch (Settings.GeneratorType)
            {
                case GeneratorType.Ef6:
                    generator = new GeneratorEf6(fileManagementService, fileManagerType);
                    break;

                case GeneratorType.EfCore:
                    generator = new GeneratorEfCore(fileManagementService, fileManagerType);
                    break;

                case GeneratorType.Custom:
                    generator = new GeneratorCustom(fileManagementService, fileManagerType);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            var providerName = "unknown";
            try
            {
                providerName = DatabaseProvider.GetProvider(Settings.DatabaseType);
                DbProviderFactory factory;
                try
                {
                    factory = DbProviderFactories.GetFactory(providerName);
                }
                catch when (providerName == "Microsoft.Data.SqlClient")
                {
                    // Microsoft.Data.SqlClient not registered in this environment (e.g. test runner).
                    // Fall back to System.Data.SqlClient which is built into .NET Framework.
                    providerName = "System.Data.SqlClient";
                    factory = DbProviderFactories.GetFactory(providerName);
                }
                var databaseReader = DatabaseReaderFactory.Create(factory);
                generator.Init(databaseReader, singleDbContextSubNamespace);
                return generator;
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                Console.WriteLine(error);

                fileManagementService.Error(generator.GetPreHeaderInfo());
                fileManagementService.Error(string.Empty);
                fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                fileManagementService.Error(string.Format("// WARNING: Failed to load provider \"{0}\" - {1}", providerName, error));
                fileManagementService.Error("// Allowed providers:");
                foreach (DataRow fc in DbProviderFactories.GetFactoryClasses().Rows)
                {
                    var s = string.Format("//    \"{0}\"", fc[2]);
                    fileManagementService.Error(s);
                }
                fileManagementService.Error(string.Empty);
                fileManagementService.Error("/*" + x.StackTrace + "*/");
                fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                fileManagementService.Error(string.Empty);
            }

            return null;
        }
    }
}