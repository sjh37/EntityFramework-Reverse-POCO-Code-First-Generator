using System.Collections.Generic;
using Efrpg.Generators;

namespace Efrpg.TemplateModels
{
    public class InterfaceModel
    {
        public string interfaceModifier                                      { get; set; }
        public string DbContextInterfaceName                                 { get; set; }
        public string DbContextInterfaceBaseClasses                          { get; set; }
        public string DbContextName                                          { get; set; }
        public List<TableTemplateData> tables                                { get; set; }
        public List<string> AdditionalContextInterfaceItems                  { get; set; }
        public bool addSaveChanges                                           { get; set; }
        public List<StoredProcTemplateData> storedProcs                      { get; set; }
        public bool hasStoredProcs                                           { get; set; }
        public List<TableValuedFunctionsTemplateData> tableValuedFunctions   { get; set; }
        public List<ScalarValuedFunctionsTemplateData> scalarValuedFunctions { get; set; }
        public bool hasTableValuedFunctions                                  { get; set; }
        public bool hasScalarValuedFunctions                                 { get; set; }
    }
}