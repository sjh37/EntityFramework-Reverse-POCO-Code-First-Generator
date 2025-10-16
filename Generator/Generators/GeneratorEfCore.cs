using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Efrpg.FileManagement;

namespace Efrpg.Generators
{
    public class GeneratorEfCore : Generator
    {
        public GeneratorEfCore(FileManagementService fileManagementService, Type fileManagerType)
            : base(fileManagementService, fileManagerType)
        {
        }

        protected override bool AllowFkToNonPrimaryKey()
        {
            return true; // It is allowed to have a FK to a non-primary key
        }

        protected override bool FkMustHaveSameNumberOfColumnsAsPrimaryKey()
        {
            return true;
        }

        protected override void SetupEntity(Column c)
        {
            if (c.PropertyType == "Hierarchy.HierarchyId")
                c.PropertyType = Settings.IsEfCore8Plus() ? "HierarchyId" : "Microsoft.SqlServer.Types.SqlHierarchyId";

            var comments = string.Empty;
            if (Settings.IncludeComments != CommentsStyle.None)
            {
                comments = c.DbName;
                if (c.IsPrimaryKey)
                {
                    if (c.IsUniqueConstraint)
                        comments += " (Primary key via unique index " + c.UniqueIndexName + ")";
                    else
                        comments += " (Primary key)";
                }

                if (c.MaxLength > 0)
                    comments += string.Format(" (length: {0})", c.MaxLength);
            }

            c.InlineComments = Settings.IncludeComments == CommentsStyle.AtEndOfField ? " // " + comments : string.Empty;

            c.SummaryComments = string.Empty;
            if (Settings.IncludeComments == CommentsStyle.InSummaryBlock && !string.IsNullOrEmpty(comments))
            {
                c.SummaryComments = comments;
            }
            if (Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock && !string.IsNullOrEmpty(c.ExtendedProperty))
            {
                if (string.IsNullOrEmpty(c.SummaryComments))
                    c.SummaryComments = c.ExtendedProperty;
                else
                    c.SummaryComments += ". " + c.ExtendedProperty;
            }

            if (Settings.IncludeExtendedPropertyComments == CommentsStyle.AtEndOfField && !string.IsNullOrEmpty(c.ExtendedProperty))
            {
                if (string.IsNullOrEmpty(c.InlineComments))
                    c.InlineComments = " // " + c.ExtendedProperty;
                else
                    c.InlineComments += ". " + c.ExtendedProperty;
            }
        }

        protected override void SetupConfig(Column c)
        {
            string databaseGeneratedOption = null;

            var isNewSequentialId = !string.IsNullOrEmpty(c.Default) && c.Default.ToLower().Contains("newsequentialid");
            var isTemporalColumn = c.GeneratedAlwaysType != ColumnGeneratedAlwaysType.NotApplicable;
            var hasDefaultValueSql = !string.IsNullOrEmpty(c.HasDefaultValueSql);

            // Identity, instead of Computed, seems the best for Temporal 'GENERATED ALWAYS' columns: https://stackoverflow.com/questions/40742142/entity-framework-not-working-with-temporal-table
            if (!hasDefaultValueSql)
            {
                if (c.IsIdentity || isNewSequentialId || isTemporalColumn)
                {
                    databaseGeneratedOption = ".ValueGeneratedOnAdd()";
                    
                    if (c.IsIdentity && DatabaseReader.HasIdentityColumnSupport() && Column.CanUseSqlServerIdentityColumn.Contains(c.PropertyType))
                        databaseGeneratedOption += Settings.ColumnIdentity(c);
                }
                else if (c.IsComputed)
                {
                    databaseGeneratedOption = ".ValueGeneratedOnAddOrUpdate()";
                }
                else if (c.IsPrimaryKey)
                {
                    databaseGeneratedOption = ".ValueGeneratedNever()";
                }
            }

            var sb = new StringBuilder(255);
            sb.AppendFormat(".HasColumnName(@\"{0}\")", c.DbName);

            var doNotSpecifySize = false;
            if (!c.IsMaxLength && c.MaxLength > 0)
                doNotSpecifySize = (DatabaseReader.DoNotSpecifySizeForMaxLength && c.MaxLength > 4000); // Issue #179

            var excludedHasColumnType = string.Empty;
            if (!string.IsNullOrEmpty(c.SqlPropertyType))
            {
                var columnTypeParameters = string.Empty;
            
                if ((c.Precision > 0 || c.Scale > 0) && (c.SqlPropertyType == "decimal" || c.SqlPropertyType == "numeric"))
                    columnTypeParameters = $"({c.Precision},{c.Scale})";
                else if (!c.IsMaxLength && c.MaxLength > 0 && !doNotSpecifySize)
                    columnTypeParameters = $"({c.MaxLength})";

                if (Column.ExcludedHasColumnType.Contains(c.SqlPropertyType))
                    excludedHasColumnType = string.Format(" // .HasColumnType(\"{0}{1}\") was excluded", c.SqlPropertyType, columnTypeParameters);
                else
                    sb.AppendFormat(".HasColumnType(\"{0}{1}\")", c.SqlPropertyType, columnTypeParameters);

                if (Settings.IsEfCore8Plus())
                {
                    if ((c.Precision > 0 || c.Scale > 0) && DatabaseReader.IsPrecisionAndScaleType(c.SqlPropertyType))
                        sb.AppendFormat(".HasPrecision({0},{1})", c.Precision, c.Scale);
                    else if (c.Precision > 0 && DatabaseReader.IsPrecisionType(c.SqlPropertyType))
                        sb.AppendFormat(".HasPrecision({0})", c.Precision);
                }

                if (Settings.TrimCharFields && c.MaxLength > 1 && c.SqlPropertyType == "char")
                {
                    sb.Append(".HasConversion(new ValueConverter<string, string>(v => v.TrimEnd(), v => v.TrimEnd()))");
                }
            }

            sb.Append(c.IsNullable ? ".IsRequired(false)" : ".IsRequired()");

            if (c.IsFixedLength || c.IsRowVersion)
                sb.Append(".IsFixedLength()");

            if (!c.IsUnicode)
                sb.Append(".IsUnicode(false)");

            if (!c.IsMaxLength && c.MaxLength > 0 && !doNotSpecifySize)
                sb.AppendFormat(".HasMaxLength({0})", c.MaxLength);

            //if (c.IsMaxLength)
            //    sb.Append(".IsMaxLength()");

            if (c.IsRowVersion)
            {
                sb.Append(".IsRowVersion()");
                c.IsConcurrencyToken = true;
            }

            if (c.IsConcurrencyToken)
                sb.Append(".IsConcurrencyToken()");

            if (databaseGeneratedOption != null)
                sb.Append(databaseGeneratedOption);

            if (hasDefaultValueSql)
                sb.AppendFormat(".HasDefaultValueSql(@\"{0}\")", c.HasDefaultValueSql);

            var config = sb.ToString();
            if (!string.IsNullOrEmpty(config))
                c.Config = string.Format("builder.Property(x => x.{0}){1};{2}", c.NameHumanCase, config, excludedHasColumnType);
        }

        public override string PrimaryKeyModelBuilder(Table t)
        {
            var IsEfCore8Plus = Settings.IsEfCore8Plus();
            if (IsEfCore8Plus && t.IsView && !t.HasPrimaryKey)
                return "builder.HasNoKey();";

            if (t.PrimaryKeys.All(k => k.Hidden))
                return string.Empty;

            var defaultKey = $"builder.HasKey({t.PrimaryKeyNameHumanCase()})";
            if (t.Indexes == null || !t.Indexes.Any())
                return defaultKey + ";";

            var indexName = t.Indexes.Where(x => x.IsPrimaryKey).Select(x => x.IndexName).Distinct().FirstOrDefault();
            if(string.IsNullOrEmpty(indexName))
                return defaultKey + ";";

            var indexesForName = t.Indexes
                .Where(x => x.IndexName == indexName)
                .OrderBy(x => x.KeyOrdinal)
                .ThenBy(x => x.ColumnName)
                .ToList();

            var sb = new StringBuilder(255);
            sb.Append(defaultKey);

            sb.Append(".HasName(\"");
            sb.Append(indexName);
            sb.Append("\")");

            if (indexesForName.All(x => x.IsClustered))
                sb.Append(IsEfCore8Plus ? ".IsClustered()" : ".ForSqlServerIsClustered()");

            sb.Append(";");

            return sb.ToString();
        }

        public override List<string> IndexModelBuilder(Table t)
        {
            var indexes = new List<string>();
            if (t.Indexes == null || !t.Indexes.Any())
                return indexes;

            var isEfCore8Plus = Settings.IsEfCore8Plus();
            var indexNames = t.Indexes.Where(x => !x.IsPrimaryKey).Select(x => x.IndexName).Distinct();
            foreach (var indexName in indexNames.OrderBy(x => x))
            {
                var indexesForName = t.Indexes
                    .Where(x => x.IndexName == indexName)
                    .OrderBy(x => x.KeyOrdinal)
                    .ThenBy(x => x.ColumnName)
                    .ToList();

                var sb = new StringBuilder(255);
                var ok = true;
                var count = 0;
                sb.Append("builder.HasIndex(x => ");
                if (indexesForName.Count > 1)
                    sb.Append("new { ");

                foreach (var index in indexesForName.OrderBy(x => x.KeyOrdinal).ThenBy(x => x.ColumnName))
                {
                    var col = t.Columns.Find(x => x.DbName == index.ColumnName);
                    if (col == null || col.Hidden || string.IsNullOrEmpty(col.Config))
                    {
                        ok = false;
                        break; // Cannot use index, as one of the columns is invalid
                    }

                    if (count > 0)
                        sb.Append(", ");

                    sb.Append("x.");
                    sb.Append(col.NameHumanCase);
                    ++count;
                }

                if (!ok)
                    continue;

                if (indexesForName.Count > 1)
                    sb.Append(" }");

                sb.Append(")"); // Close bracket for HasIndex()

                
                sb.Append(isEfCore8Plus ? ".HasDatabaseName(\"" : ".HasName(\"");
                sb.Append(indexName);
                sb.Append("\")");

                if (indexesForName.All(x => x.IsUnique) || indexesForName.All(x => x.IsUniqueConstraint))
                    sb.Append(".IsUnique()");

                if (indexesForName.All(x => x.IsClustered))
                    sb.Append(isEfCore8Plus ? ".IsClustered()" : ".ForSqlServerIsClustered()");

                sb.Append(";");
                indexes.Add(sb.ToString());
            }

            return indexes;
        }

        public override string IndexModelBuilder(Column c)
        {
            return null;
        }

        // HasOne
        // HasMany
        protected override string GetHasMethod(Relationship relationship, IList<Column> fkCols, IList<Column> pkCols, bool isNotEnforced, bool fkHasUniqueConstraint)
        {
            if (relationship == Relationship.ManyToMany)
                return null; // Not yet supported in EF.Core

            var withMany = relationship == Relationship.ManyToOne || relationship == Relationship.ManyToMany;
            var pkIsUnique = pkCols.Any(c => c.IsUnique || c.IsUniqueConstraint || c.IsPrimaryKey);

            if (withMany || pkIsUnique || fkHasUniqueConstraint)
                return "builder.HasOne";

            return "builder.HasMany";
        }

        // WithOne
        // WithMany
        protected override string GetWithMethod(Relationship relationship, IList<Column> fkCols, string fkPropName, string manyToManyMapping, string mapKey,
            bool includeReverseNavigation, string hasMethod, string pkTableNameHumanCase, string fkTableNameHumanCase, string primaryKeyColumns, bool fkHasUniqueConstraint)
        {
            var withParam = includeReverseNavigation ? string.Format("b => b.{0}", fkPropName) : string.Empty;
            var principalEntityType = fkHasUniqueConstraint ? string.Format("<{0}>", pkTableNameHumanCase) : string.Empty;
            var hasPrincipleKey = !string.IsNullOrEmpty(primaryKeyColumns) ? string.Format(".HasPrincipalKey{0}({1})", principalEntityType, primaryKeyColumns) : string.Empty;

            switch (relationship)
            {
                case Relationship.OneToOne:
                    return string.Format(".WithOne({0}){1}.HasForeignKey<{2}>({3})", withParam, hasPrincipleKey, fkTableNameHumanCase, manyToManyMapping);

                case Relationship.OneToMany:
                    return string.Format(".WithMany({0})", withParam);

                case Relationship.ManyToOne:
                case Relationship.ManyToMany:
                    return string.Format(".WithMany({0}){1}.HasForeignKey({2})", withParam, hasPrincipleKey, manyToManyMapping);

                default:
                    throw new ArgumentOutOfRangeException(nameof(relationship));
            }
        }

        protected override string GetCascadeOnDelete(bool cascadeOnDelete)
        {
            return cascadeOnDelete ? string.Empty : ".OnDelete(DeleteBehavior.ClientSetNull)";
        }

        protected override string GetForeignKeyConstraintName(string foreignKeyConstraintName)
        {
            return string.Format(".HasConstraintName(\"{0}\")", foreignKeyConstraintName);
        }
    }
}