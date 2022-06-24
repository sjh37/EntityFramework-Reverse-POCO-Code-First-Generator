using System.Collections.Generic;
using System.IO;
using System.Linq;
using Efrpg;
using Efrpg.Generators;
using Efrpg.TemplateModels;
using Efrpg.Templates;

namespace BuildTT
{
    public static class TemplateFiles
    {
        public static void Create(string templatesRoot)
        {
            CreateFiles(new TemplateEf6(), Path.Combine(templatesRoot, "Templates.EF6"));
            CreateFiles(new TemplateEfCore2(), Path.Combine(templatesRoot, "Templates.EFCore2"));
            CreateFiles(new TemplateEfCore3(), Path.Combine(templatesRoot, "Templates.EFCore3"));
            CreateFiles(new TemplateEfCore5(), Path.Combine(templatesRoot, "Templates.EFCore5"));
            CreateFiles(new TemplateEfCore6(), Path.Combine(templatesRoot, "Templates.EFCore6"));
        }

        private static void CreateFiles(Template template, string folder)
        {
            Directory.CreateDirectory(folder);

            // Mustache
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.Usings),                   template.Usings());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.DatabaseContextInterface), template.DatabaseContextInterface());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.DatabaseContext),          template.DatabaseContext());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.DatabaseContextFactory),   template.DatabaseContextFactory());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.FakeDatabaseContext),      template.FakeDatabaseContext());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.FakeDbSet),                template.FakeDbSet());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.Poco),                     template.Poco());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.PocoConfiguration),        template.PocoConfiguration());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.StoredProcReturnModels),   template.StoredProcReturnModels());
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Mustache.Enums),                    template.Enums());

            // Text
            Settings.IncludeCodeGeneratedAttribute = true;
            Settings.UseInheritedBaseInterfaceFunctions = false;
            Settings.OnConfiguration = OnConfiguration.Configuration;

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.DatabaseContextInterfaceUsings),
                template.DatabaseContextInterfaceUsings(new InterfaceModel
                {
                    addSaveChanges           = true,
                    hasScalarValuedFunctions = true,
                    hasStoredProcs           = true,
                    hasTableValuedFunctions  = true,
                    tables                   = new List<TableTemplateData>()
                }));

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.DatabaseContextUsings),
                template.DatabaseContextUsings(new ContextModel
                {
                    addSaveChanges           = true,
                    hasScalarValuedFunctions = true,
                    hasStoredProcs           = true,
                    hasTableValuedFunctions  = true,
                    tables                   = new List<TableTemplateData>()
                }));

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.DatabaseContextFactoryUsings),
                template.DatabaseContextFactoryUsings(new FactoryModel()));

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.FakeDatabaseContextUsings),
                template.FakeDatabaseContextUsings(new FakeContextModel
                {
                    hasScalarValuedFunctions = true,
                    hasStoredProcs           = true,
                    hasTableValuedFunctions  = true,
                    tables                   = new List<TableTemplateData>()
                }, null));

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.FakeDbSetUsings),
                template.FakeDbSetUsings(new FakeDbSetModel()));

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.PocoUsings),
                template.PocoUsings(new PocoModel()));
            
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.PocoConfigurationUsings),
                template.PocoConfigurationUsings(new PocoConfigurationModel
            {
                UsesDictionary = true
            }));
            
            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.StoredProcReturnModelUsings),
                template.StoredProcReturnModelUsings());

            CreateFile(Path.Combine(folder, TemplateFileBasedConstants.Text.EnumUsings),
                template.EnumUsings());
        }

        private static void CreateFile(string path, string data)
        {
            using (var sw = File.CreateText(path))
            {
                sw.Write(data.Trim());
                sw.Flush();
            }
        }

        private static void CreateFile(string path, List<string> data)
        {
            using (var sw = File.CreateText(path))
            {
                foreach (var item in data.Distinct().OrderBy(x => x))
                    sw.WriteLine(item);

                sw.Flush();
            }
        }
    }
}