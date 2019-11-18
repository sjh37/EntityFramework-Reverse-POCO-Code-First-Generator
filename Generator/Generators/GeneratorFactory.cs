using System;
using Efrpg.FileManagement;
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

            generator.Init(singleDbContextSubNamespace);
            return generator;
        }
    }
}