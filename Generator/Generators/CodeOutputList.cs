using System.Collections.Generic;
using System.Linq;

namespace Efrpg.Generators
{
    public class CodeOutputList
    {
        public readonly Dictionary<string, CodeOutput> Files; // List of code files

        public CodeOutputList()
        {
            Files = new Dictionary<string, CodeOutput>();
        }

        public void Add(string key, CodeOutput code)
        {
            if(code != null)
                Files.Add(key, code);
        }

        public List<string> GetUsings()
        {
            var usings = new List<string>();
            foreach (var codeOutput in Files)
            {
                usings.AddRange(codeOutput.Value.Usings);
            }

            return usings
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }
    }
}