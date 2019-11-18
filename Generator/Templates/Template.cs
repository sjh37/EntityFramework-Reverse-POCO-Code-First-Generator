using System.Collections.Generic;
using Efrpg.Filtering;
using Efrpg.Mustache;
using Efrpg.TemplateModels;

namespace Efrpg.Templates
{
    /// <summary>
    /// {{Mustache}} template documentation available at https://github.com/jehugaleahsa/mustache-sharp
    /// </summary>
    public abstract class Template
    {
        public abstract string Usings();

        public abstract List<string> DatabaseContextInterfaceUsings(InterfaceModel data);
        public abstract string DatabaseContextInterface();

        public abstract List<string> DatabaseContextUsings(ContextModel data);
        public abstract string DatabaseContext();

        public abstract List<string> DatabaseContextFactoryUsings(FactoryModel data);
        public abstract string DatabaseContextFactory();

        public abstract List<string> FakeDatabaseContextUsings(FakeContextModel data, IDbContextFilter filter);
        public abstract string FakeDatabaseContext();

        public abstract List<string> FakeDbSetUsings(FakeDbSetModel data);
        public abstract string FakeDbSet();

        public abstract List<string> PocoUsings(PocoModel data);
        public abstract string Poco();

        public abstract List<string> PocoConfigurationUsings(PocoConfigurationModel data);
        public abstract string PocoConfiguration();

        public abstract List<string> StoredProcReturnModelUsings();
        public abstract string StoredProcReturnModels();

        public abstract string Enums();

        public static string Transform(string template, object data)
        {
            if (data == null || template == null)
                return template;

            // Thanks to the awesome work by Travis Parks and Keith Williams for the Mustache# for .NET Core library
            // which is available at https://github.com/SunBrandingSolutions/mustache-sharp
            var parser = new FormatCompiler();
            var mustacheGenerator = parser.Compile(template);
            return mustacheGenerator.Render(data);
        }
    }
}