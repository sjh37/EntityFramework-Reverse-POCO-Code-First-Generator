using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Generators
{
    public class CodeOutput
    {
        public string Filename { get; private set; }
        public string Region { get; private set; }
        public List<string> Usings { get; private set; }
        public List<string> Code { get; private set; }

        public CodeOutput(string filename, string region, List<string> usings)
        {
            Filename = filename;
            Region = region;
            Usings = new List<string>();
            Code = new List<string>();

            AddUsings(usings);
        }

        public void AddCode(string code)
        {
            if(code != null)
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