using System.Collections.Generic;
using Efrpg.Readers;

namespace Efrpg.Filtering
{
    public abstract class DbContextFilter : IDbContextFilter
    {
        public string SubNamespace               { get; set; }
        public Tables Tables                     { get; set; }
        public List<StoredProcedure> StoredProcs { get; set; }
        public List<Enumeration> Enums           { get; set; }
        public List<RawSequence> Sequences       { get; set; }
        public bool IncludeViews                 { get; set; }
        public bool IncludeSynonyms              { get; set; }
        public bool IncludeStoredProcedures      { get; set; }
        public bool IncludeTableValuedFunctions  { get; set; }
        public bool IncludeScalarValuedFunctions { get; set; }

        protected DbContextFilter()
        {
            Tables       = new Tables();
            StoredProcs  = new List<StoredProcedure>();
            Enums        = new List<Enumeration>();
            Sequences    = new List<RawSequence>();
            SubNamespace = string.Empty;
        }

        public abstract bool IsExcluded(EntityName item);
        public abstract string TableRename(string name, string schema, bool isView);
        public abstract string MappingTableRename(string mappingTable, string tableName, string entityName);
        public abstract void UpdateTable(Table table);
        public abstract void UpdateColumn(Column column, Table table);
        public abstract void UpdateEnum(Enumeration enumeration);
        public abstract void UpdateEnumMember(EnumerationMember enumerationMember);
        public abstract void ViewProcessing(Table view);
        public abstract string StoredProcedureRename(StoredProcedure sp);
        public abstract string StoredProcedureReturnModelRename(string name, StoredProcedure sp);
        public abstract ForeignKey ForeignKeyFilter(ForeignKey fk);
        public abstract string[] ForeignKeyAnnotationsProcessing(Table fkTable, Table pkTable, string propName, string fkPropName);
    }
}