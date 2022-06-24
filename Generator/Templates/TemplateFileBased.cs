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
            return CacheText("Usings.mustache");
        }

        public override List<string> DatabaseContextInterfaceUsings(InterfaceModel data)
        {
            return CacheList("DatabaseContextInterfaceUsings.txt");
        }

        public override string DatabaseContextInterface()
        {
            return CacheText("DatabaseContextInterface.mustache");
        }

        public override List<string> DatabaseContextUsings(ContextModel data)
        {
            return CacheList("DatabaseContextUsings.txt");
        }

        public override string DatabaseContext()
        {
            return CacheText("DatabaseContext.mustache");
        }

        public override List<string> DatabaseContextFactoryUsings(FactoryModel data)
        {
            return CacheList("DatabaseContextFactoryUsings.txt");
        }

        public override string DatabaseContextFactory()
        {
            return CacheText("DatabaseContextFactory.mustache");
        }

        public override List<string> FakeDatabaseContextUsings(FakeContextModel data, IDbContextFilter filter)
        {
            return CacheList("FakeDatabaseContextUsings.txt");
        }

        public override string FakeDatabaseContext()
        {
            return CacheText("FakeDatabaseContext.mustache");
        }

        public override List<string> FakeDbSetUsings(FakeDbSetModel data)
        {
            return CacheList("FakeDbSetUsings.txt");
        }

        public override string FakeDbSet()
        {
            return CacheText("FakeDbSet.mustache");
        }

        public override List<string> PocoUsings(PocoModel data)
        {
            return CacheList("PocoUsings.txt");
        }

        public override string Poco()
        {
            return CacheText("Poco.mustache");
        }

        public override List<string> PocoConfigurationUsings(PocoConfigurationModel data)
        {
            return CacheList("PocoConfigurationUsings.txt");
        }

        public override string PocoConfiguration()
        {
            return CacheText("PocoConfiguration.mustache");
        }

        public override List<string> StoredProcReturnModelUsings()
        {
            return CacheList("StoredProcReturnModelUsings.txt");
        }

        public override string StoredProcReturnModels()
        {
            return CacheText("StoredProcReturnModels.mustache");
        }

        public override List<string> EnumUsings()
        {
            return CacheList("EnumUsings.txt");
        }

        public override string Enums()
        {
            return CacheText("Enums.mustache");
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