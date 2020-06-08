using System;
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
            if (code != null)
            {
                if (Files.ContainsKey(key))
                {
                    var error = string.Format("{0} already exists in the code output list. {1} and {2} both resolve to the same C# name. Filter one of them out.", key, Files[key].DbName, code.DbName);
                    throw new Exception(error);
                }

                Files.Add(key, code);
            }
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