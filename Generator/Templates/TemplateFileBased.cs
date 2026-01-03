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
        private readonly Dictionary<string, string> _cacheText;
        private readonly Dictionary<string, List<string>> _cacheList;

        public TemplateFileBased()
        {
            _cacheText = new Dictionary<string, string>();
            _cacheList = new Dictionary<string, List<string>>();
        }

        public override string Usings()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.Usings);
        }

        public override List<string> DatabaseContextInterfaceUsings(InterfaceModel data)
        {
            return CacheList(TemplateFileBasedConstants.Text.DatabaseContextInterfaceUsings);
        }

        public override string DatabaseContextInterface()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.DatabaseContextInterface);
        }

        public override List<string> DatabaseContextUsings(ContextModel data)
        {
            return CacheList(TemplateFileBasedConstants.Text.DatabaseContextUsings);
        }

        public override string DatabaseContext()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.DatabaseContext);
        }

        public override List<string> DatabaseContextFactoryUsings(FactoryModel data)
        {
            return CacheList(TemplateFileBasedConstants.Text.DatabaseContextFactoryUsings);
        }

        public override string DatabaseContextFactory()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.DatabaseContextFactory);
        }

        public override List<string> FakeDatabaseContextUsings(FakeContextModel data, IDbContextFilter filter)
        {
            return CacheList(TemplateFileBasedConstants.Text.FakeDatabaseContextUsings);
        }

        public override string FakeDatabaseContext()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.FakeDatabaseContext);
        }

        public override List<string> FakeDbSetUsings(FakeDbSetModel data)
        {
            return CacheList(TemplateFileBasedConstants.Text.FakeDbSetUsings);
        }

        public override string FakeDbSet()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.FakeDbSet);
        }

        public override List<string> PocoUsings(PocoModel data)
        {
            var usings = new List<string>(CacheList(TemplateFileBasedConstants.Text.PocoUsings));

            // Add dynamic namespaces based on column types
            if (data.HasHierarchyId && !usings.Contains("Microsoft.EntityFrameworkCore"))
                usings.Add("Microsoft.EntityFrameworkCore");

            if (data.HasSqlVector && !usings.Contains("Microsoft.Data.SqlTypes"))
                usings.Add("Microsoft.Data.SqlTypes");

            return usings;
        }

        public override string Poco()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.Poco);
        }

        public override List<string> PocoConfigurationUsings(PocoConfigurationModel data)
        {
            return CacheList(TemplateFileBasedConstants.Text.PocoConfigurationUsings);
        }

        public override string PocoConfiguration()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.PocoConfiguration);
        }

        public override List<string> StoredProcReturnModelUsings()
        {
            return CacheList(TemplateFileBasedConstants.Text.StoredProcReturnModelUsings);
        }

        public override string StoredProcReturnModels()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.StoredProcReturnModels);
        }

        public override List<string> EnumUsings()
        {
            return CacheList(TemplateFileBasedConstants.Text.EnumUsings);
        }

        public override string Enums()
        {
            return CacheText(TemplateFileBasedConstants.Mustache.Enums);
        }

        private string CacheText(string filename)
        {
            if (_cacheText.ContainsKey(filename))
                return _cacheText[filename];

            var file = Path.Combine(Settings.TemplateFolder, filename);
            var text = File.ReadAllText(file);
            _cacheText.Add(filename, text);
            return text;
        }

        private List<string> CacheList(string filename)
        {
            if (_cacheList.ContainsKey(filename))
                return _cacheList[filename];

            var file = Path.Combine(Settings.TemplateFolder, filename);
            var lines = File.ReadLines(file).ToList();
            _cacheList.Add(filename, lines);
            return lines;
        }
    }
}