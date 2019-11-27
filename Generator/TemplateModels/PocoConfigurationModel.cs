using System.Collections.Generic;

namespace Efrpg.TemplateModels
{
    public class PocoConfigurationModel
    {
        public string Name                                                        { get; set; }
        public string ConfigurationClassName                                      { get; set; }
        public string NameHumanCaseWithSuffix                                     { get; set; }
        public string Schema                                                      { get; set; }
        public string PrimaryKeyNameHumanCase                                     { get; set; }
        public bool HasSchema                                                     { get; set; }
        public string ClassModifier                                               { get; set; }
        public string ClassComment                                                { get; set; }
        public List<string> Columns                                               { get; set; }
        public bool HasReverseNavigation                                          { get; set; }
        public List<PocoReverseNavigationPropertyModel> ReverseNavigationProperty { get; set; }
        public bool HasForeignKey                                                 { get; set; }
        public List<string> ForeignKeys                                           { get; set; }
        public List<string> MappingConfiguration                                  { get; set; }
        public List<string> Indexes                                               { get; set; }
        public bool HasIndexes                                                    { get; set; }
        public bool ConfigurationClassesArePartial                                { get; set; }
        public List<string> AlternateKeys                                         { get; set; }
        public bool HasAlternateKeys                                              { get; set; }
        public bool UseHasNoKey                                                   { get; set; }
        public string ToTableOrView                                               { get; set; }
    }
}