using System;
using Efrpg.FileManagement;
using Efrpg.Readers;
using Efrpg.Templates;

namespace Efrpg.Generators
{
    public static class GeneratorFactory
    {
        public static Generator Create(EfrpgResult result, FileManagementService fileManagementService, string singleDbContextSubNamespace = null)
        {
            Generator generator;

            switch (Settings.GeneratorType)
            {
                case GeneratorType.Ef6:
                    generator = new GeneratorEf6(fileManagementService);
                    break;

                case GeneratorType.EfCore:
                    generator = new GeneratorEfCore(fileManagementService);
                    break;

                case GeneratorType.Custom:
                    generator = new GeneratorCustom(fileManagementService);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            try
            {
                if (result != null && result.HasErrors)
                {
                    fileManagementService.Error(generator.GetPreHeaderInfo());
                    fileManagementService.Error(string.Empty);
                    fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                    foreach (var err in result.Errors)
                        fileManagementService.Error(string.Format("// {0}: {1}", err.Type, err.Message));
                    fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                    fileManagementService.Error(string.Empty);
                    return null;
                }

                generator.Init(result, singleDbContextSubNamespace);
                return generator;
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                Console.WriteLine(error);

                fileManagementService.Error(generator.GetPreHeaderInfo());
                fileManagementService.Error(string.Empty);
                fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                fileManagementService.Error(string.Format("// WARNING: Failed to initialise generator - {0}", error));
                fileManagementService.Error(string.Empty);
                fileManagementService.Error("/*" + x.StackTrace + "*/");
                fileManagementService.Error("// ------------------------------------------------------------------------------------------------");
                fileManagementService.Error(string.Empty);
            }

            return null;
        }
    }
}
