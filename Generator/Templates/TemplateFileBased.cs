using System.Collections.Generic;
using System.IO;
using System.Linq;
using Efrpg.Filtering;
using Efrpg.TemplateModels;

namespace Efrpg.Templates
{
    /// <summary>
    /// {{Mustache}} template documentation available at https://github.com/jehugaleahsa/mustache-sharp
    /// </summary>
    public class TemplateFileBased : Template
    {
        public override string Usings()
        {
            var file = Path.Combine(Settings.TemplateFolder, "Usings.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> DatabaseContextInterfaceUsings(InterfaceModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContextInterfaceUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string DatabaseContextInterface()
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContextInterface.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> DatabaseContextUsings(ContextModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContextUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string DatabaseContext()
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContext.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> DatabaseContextFactoryUsings(FactoryModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContextFactoryUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string DatabaseContextFactory()
        {
            var file = Path.Combine(Settings.TemplateFolder, "DatabaseContextFactory.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> FakeDatabaseContextUsings(FakeContextModel data, IDbContextFilter filter)
        {
            var file = Path.Combine(Settings.TemplateFolder, "FakeDatabaseContextUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string FakeDatabaseContext()
        {
            var file = Path.Combine(Settings.TemplateFolder, "FakeDatabaseContext.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> FakeDbSetUsings(FakeDbSetModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "FakeDbSetUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string FakeDbSet()
        {
            var file = Path.Combine(Settings.TemplateFolder, "FakeDbSet.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> PocoUsings(PocoModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "PocoUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string Poco()
        {
            var file = Path.Combine(Settings.TemplateFolder, "Poco.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> PocoConfigurationUsings(PocoConfigurationModel data)
        {
            var file = Path.Combine(Settings.TemplateFolder, "PocoConfigurationUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string PocoConfiguration()
        {
            var file = Path.Combine(Settings.TemplateFolder, "PocoConfiguration.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> StoredProcReturnModelUsings()
        {
            var file = Path.Combine(Settings.TemplateFolder, "StoredProcReturnModelUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string StoredProcReturnModels()
        {
            var file = Path.Combine(Settings.TemplateFolder, "StoredProcReturnModels.mustache");
            return File.ReadAllText(file);
        }

        public override List<string> EnumUsings()
        {
            var file = Path.Combine(Settings.TemplateFolder, "EnumUsings.txt");
            return File.ReadLines(file).ToList();
        }

        public override string Enums()
        {
            var file = Path.Combine(Settings.TemplateFolder, "Enums.mustache");
            return File.ReadAllText(file);
        }
    }
}