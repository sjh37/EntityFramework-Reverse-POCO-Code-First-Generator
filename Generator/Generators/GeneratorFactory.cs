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
            // The template type already says which generator runs: Ef6 is the only non-EF Core template.
            Generator generator = Settings.IsEf6()
                ? new GeneratorEf6(fileManagementService)
                : new GeneratorEfCore(fileManagementService);

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
