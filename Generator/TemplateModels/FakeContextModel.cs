using System.Collections.Generic;
using Efrpg.Generators;

namespace Efrpg.TemplateModels
{
    public class FakeContextModel
    {
        public string DbContextClassModifiers { get; set; }
        public string DbContextName { get; set; }
        public string DbContextBaseClass { get; set; }
        public string contextInterface { get; set; }
        public bool DbContextClassIsPartial { get; set; }
        public List<TableTemplateData> tables { get; set; }
        public List<StoredProcTemplateData> storedProcs { get; set; }
        public bool hasStoredProcs { get; set; }
        public List<TableValuedFunctionsTemplateData> tableValuedFunctions { get; set; }
        public List<ScalarValuedFunctionsTemplateData> scalarValuedFunctions { get; set; }
        public bool hasTableValuedFunctions { get; set; }
        public bool hasScalarValuedFunctions { get; set; }
    }
}