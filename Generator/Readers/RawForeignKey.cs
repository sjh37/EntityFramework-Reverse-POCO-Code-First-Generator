namespace Efrpg.Readers
{
    public class RawForeignKey
    {
        public readonly string ConstraintName;
        public readonly string ParentName;
        public readonly string ChildName;
        public readonly string PkColumn;
        public readonly string FkColumn;
        public readonly string PkSchema;
        public readonly string PkTableName;
        public readonly string FkSchema;
        public readonly string FkTableName;
        public readonly int    Ordinal;
        public readonly bool   CascadeOnDelete;
        public readonly bool   IsNotEnforced;

        public bool HasUniqueConstraint; // Can also be changed later
        //public byte SortOrder;

        public RawForeignKey(
            string constraintName, string parentName, string childName,
            string pkColumn, string fkColumn, string pkSchema, string pkTableName,
            string fkSchema, string fkTableName, int ordinal, bool cascadeOnDelete,
            bool isNotEnforced, bool hasUniqueConstraint)
        {
            ConstraintName      = constraintName;
            ParentName          = parentName;
            ChildName           = childName;
            PkColumn            = pkColumn;
            FkColumn            = fkColumn;
            PkSchema            = pkSchema;
            PkTableName         = pkTableName;
            FkSchema            = fkSchema;
            FkTableName         = fkTableName;
            Ordinal             = ordinal;
            CascadeOnDelete     = cascadeOnDelete;
            IsNotEnforced       = isNotEnforced;
            HasUniqueConstraint = hasUniqueConstraint;
        }

        public string Dump()
        {
            var parentName = "null";
            if(!string.IsNullOrWhiteSpace(ParentName))
                parentName = string.Format("\"{0}\"", ParentName);
            
            var childName = "null";
            if(!string.IsNullOrWhiteSpace(ChildName))
                childName = string.Format("\"{0}\"", ChildName);

            return string.Format("new RawForeignKey(\"{0}\", {1}, {2}, \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", {9}, {10}, {11}, {12}),",
                ConstraintName, parentName, childName, PkColumn, FkColumn, PkSchema, PkTableName,
                FkSchema, FkTableName, Ordinal, CascadeOnDelete ? "true" : "false", IsNotEnforced ? "true" : "false", HasUniqueConstraint ? "true" : "false");
        }
    }
}