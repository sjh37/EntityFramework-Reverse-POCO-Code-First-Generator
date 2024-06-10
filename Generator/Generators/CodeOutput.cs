using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Efrpg.Generators
{
    public class CodeOutput
    {
        public string DbName { get; private set; }
        public string Filename { get; private set; }
        public string Region { get; private set; }
        public List<string> Usings { get; private set; }
        public List<string> Code { get; private set; }

        public CodeOutput(string dbName, string filename, string region, string folder, List<string> usings)
        {
            DbName = dbName;
            Filename = filename;
            Region = region;
            Usings = new List<string>();
            Code = new List<string>();

            if (!string.IsNullOrWhiteSpace(folder))
                Filename = Path.Combine(folder, filename);

            AddUsings(usings);
        }

        public void AddCode(string code)
        {
            if (code != null)
                Code.AddRange(code.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }

        public void AddUsings(List<string> usings)
        {
            if (usings == null || !usings.Any())
                return;

            Usings.AddRange(usings);
            Usings = Usings.Where(x => x != null).Distinct().ToList();
        }

        public List<string> GetUsings()
        {
            return Usings
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }
    }
}