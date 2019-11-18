using System.Linq;

namespace Efrpg.Generators
{
    public class TableTemplateData
    {
        public string DbSetName        { get; }
        public string DbSetConfigName  { get; }
        public string PluralTableName  { get; }
        public string DbSetModifier    { get; }
        public string Comment          { get; }
        public string DbSetPrimaryKeys { get; }
        public Table  Table            { get; }

        public TableTemplateData(Table table)
        {
            Table            = table;
            DbSetName        = table.NameHumanCaseWithSuffix();
            DbSetConfigName  = table.NameHumanCaseWithSuffix() + Settings.ConfigurationClassName;
            PluralTableName  = !string.IsNullOrWhiteSpace(table.PluralNameOverride) ? table.PluralNameOverride :  Inflector.MakePlural(table.NameHumanCase);
            DbSetModifier    = table.DbSetModifier;
            Comment          = Settings.IncludeComments == CommentsStyle.None ? string.Empty : " // " + table.DbName;
            DbSetPrimaryKeys = string.Join(", ", table.PrimaryKeys.Select(x => "\"" + x.NameHumanCase + "\""));
        }
    }
}