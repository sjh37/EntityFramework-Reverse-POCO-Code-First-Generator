﻿using System;
using Efrpg.Readers;

namespace Efrpg
{
    public class ForeignKey
    {
        public readonly string FkTableName;
        public readonly string FkSchema;
        public readonly string PkTableName;
        public readonly string FkTableNameFiltered;
        public readonly string PkTableNameFiltered;
        public readonly string PkSchema;
        public readonly string FkColumn;
        public readonly string PkColumn;
        public readonly string ConstraintName;
        public readonly int    Ordinal;
        public readonly bool   CascadeOnDelete;
        public readonly string ParentName;
        public readonly string ChildName;
        public readonly bool   HasUniqueConstraint;
        
        // Record unique name for this foreign key
        public string UniqueName;

        // User settable via ForeignKeyFilter callback
        public string AccessModifier           { get; set; }
        public bool   IncludeReverseNavigation { get; set; }
        public bool   IsNotEnforced            { get; set; }

        public ForeignKey(string fkTableName, string fkSchema, string pkTableName, string pkSchema, string fkColumn,
            string pkColumn, string constraintName, string fkTableNameFiltered, string pkTableNameFiltered, int ordinal,
            bool cascadeOnDelete, bool isNotEnforced, string parentName, string childName, bool hasUniqueConstraint)
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
            FkTableNameFiltered = fkTableNameFiltered;
            PkTableNameFiltered = pkTableNameFiltered;
            Ordinal             = ordinal;
            CascadeOnDelete     = cascadeOnDelete;
            IsNotEnforced       = isNotEnforced;
            HasUniqueConstraint = hasUniqueConstraint;

            UniqueName               = string.Empty;
            IncludeReverseNavigation = true;
        }

        public ForeignKey(RawForeignKey rfk, string fkTableNameFiltered, string pkTableNameFiltered)
        {
            ConstraintName      = rfk.ConstraintName;
            ParentName          = rfk.ParentName;
            ChildName           = rfk.ChildName;
            PkColumn            = rfk.PkColumn;
            FkColumn            = rfk.FkColumn;
            PkSchema            = rfk.PkSchema;
            PkTableName         = rfk.PkTableName;
            FkSchema            = rfk.FkSchema;
            FkTableName         = rfk.FkTableName;
            FkTableNameFiltered = fkTableNameFiltered;
            PkTableNameFiltered = pkTableNameFiltered;
            Ordinal             = rfk.Ordinal;
            CascadeOnDelete     = rfk.CascadeOnDelete;
            IsNotEnforced       = rfk.IsNotEnforced;
            HasUniqueConstraint = rfk.HasUniqueConstraint;

            UniqueName               = string.Empty;
            IncludeReverseNavigation = true;
        }

        public string PkTableHumanCase(string suffix)
        {
            var singular = Inflector.MakeSingular(DatabaseReader.CleanUp(PkTableNameFiltered));

            var pkTableHumanCase = (Settings.UsePascalCase ? Inflector.ToTitleCase(singular) : singular)
                .Replace(" ", string.Empty)
                .Replace("$", string.Empty);

            if (string.Compare(PkSchema, Settings.DefaultSchema, StringComparison.OrdinalIgnoreCase) != 0 && Settings.PrependSchemaName)
                pkTableHumanCase = PkSchema + "_" + pkTableHumanCase;

            pkTableHumanCase += suffix;
            return pkTableHumanCase;
        }
    }
}