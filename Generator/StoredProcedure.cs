using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Efrpg.Filtering;
using Efrpg.Readers;

namespace Efrpg
{
    public class StoredProcedure : EntityName
    {
        public Schema Schema;
        public List<StoredProcedureParameter> Parameters;
        public List<List<DataColumn>> ReturnModels; // A list of return models, containing a list of return columns
        public bool IsTableValuedFunction;
        public bool IsScalarValuedFunction;
        public bool IsStoredProcedure;

        public StoredProcedure()
        {
            Parameters = new List<StoredProcedureParameter>();
            ReturnModels = new List<List<DataColumn>>();
        }

        public static bool IsNullable(DataColumn col)
        {
            return col.DataType.Namespace != null &&
                   col.AllowDBNull &&
                   !(
                       Column.NotNullable.Contains(col.DataType.Name.ToLower()) ||
                       Column.NotNullable.Contains(col.DataType.Namespace.ToLower() + "." + col.DataType.Name.ToLower())
                     );
        }

        public static string WrapTypeIfNullable(string propertyType, DataColumn col)
        {
            return !IsNullable(col) ? propertyType : string.Format(Settings.NullableShortHand ? "{0}?" : "Nullable<{0}>", propertyType);
        }

        public string WriteStoredProcFunctionName(IDbContextFilter filter)
        {
            var name = filter.StoredProcedureRename(this);
            return !string.IsNullOrEmpty(name) ? name : NameHumanCase;
        }

        public bool StoredProcHasOutParams()
        {
            return Parameters.Any(x => x.Mode != StoredProcedureParameterMode.In);
        }

        public string WriteStoredProcFunctionParams(bool includeProcResult)
        {
            var sb = new StringBuilder(255);
            var n = 1;
            var data = Parameters.Where(x => x.Mode != StoredProcedureParameterMode.Out).OrderBy(x => x.Ordinal).ToList();
            var count = data.Count;
            foreach (var p in data)
            {
                sb.AppendFormat("{0}{1}{2} {3}{4}",
                    p.Mode == StoredProcedureParameterMode.In ? string.Empty : "out ",
                    p.PropertyType,
                    Column.NotNullable.Contains(p.PropertyType.ToLower()) ? string.Empty : "?",
                    p.NameHumanCase,
                    n++ < count ? ", " : string.Empty);
            }

            if (includeProcResult && ReturnModels.Count > 0 && ReturnModels.First().Count > 0)
                sb.AppendFormat((data.Count > 0 ? ", " : string.Empty) + "out int procResult");

            return sb.ToString();
        }

        public string WriteStoredProcFunctionOverloadCall()
        {
            var sb = new StringBuilder(255);
            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendFormat("{0}{1}, ",
                    p.Mode == StoredProcedureParameterMode.In ? string.Empty : "out ",
                    p.NameHumanCase);
            }

            sb.Append("out procResult");
            return sb.ToString();
        }

        public string WriteStoredProcFunctionSqlAtParams()
        {
            var sb = new StringBuilder(255);
            var n = 1;
            var count = Parameters.Count;
            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendFormat("{0}{1}{2}",
                    p.Name,
                    p.Mode == StoredProcedureParameterMode.In ? string.Empty : " OUTPUT",
                    n++ < count ? ", " : string.Empty);
            }

            return sb.ToString();
        }

        public string WriteNetCoreTableValuedFunctionsSqlAtParams()
        {
            var sb = new StringBuilder(255);
            var count = Parameters.Count;
            for(var n = 0; n < count; n++)
            {
                sb.AppendFormat("{{{0}}}{1}",
                    n,
                    (n + 1) < count ? ", " : string.Empty);
            }

            return sb.ToString();
        }

        public string WriteStoredProcSqlParameterName(StoredProcedureParameter p)
        {
            return p.NameHumanCase + "Param";
        }

        public string WriteStoredProcFunctionDeclareSqlParameter(bool includeProcResult)
        {
            var sb = new StringBuilder(1024);
            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                var isNullable = !Column.NotNullable.Contains(p.PropertyType.ToLower());
                var getValueOrDefault = isNullable ? ".GetValueOrDefault()" : string.Empty;
                var isGeography = p.PropertyType == "DbGeography";

                sb.AppendLine(
                    string.Format("        var {0} = new SqlParameter", WriteStoredProcSqlParameterName(p))
                    + string.Format(" {{ ParameterName = \"{0}\", ", p.Name)
                    + (isGeography ? "UdtTypeName = \"geography\"" : string.Format("SqlDbType = SqlDbType.{0}", p.SqlDbType))
                    + ", Direction = ParameterDirection."
                    + (p.Mode == StoredProcedureParameterMode.In ? "Input" : "Output")
                    + (p.Mode == StoredProcedureParameterMode.In
                        ? ", Value = " + (isGeography
                            ? string.Format("SqlGeography.Parse({0}.AsText())", p.NameHumanCase)
                              : p.NameHumanCase + getValueOrDefault)
                        : string.Empty)
                    + (p.MaxLength != 0 ? ", Size = " + p.MaxLength : string.Empty)
                    + ((p.Precision > 0 || p.Scale > 0) ? ", Precision = " + p.Precision + ", Scale = " + p.Scale : string.Empty)
                    + (p.PropertyType.ToLower().Contains("datatable") ? ", TypeName = \"" + p.UserDefinedTypeName + "\"" : string.Empty)
                    + " };");

                if (p.Mode == StoredProcedureParameterMode.In)
                {
                    sb.AppendFormat(
                        isNullable
                            ? "        if (!{0}.HasValue){1}            {0}Param.Value = DBNull.Value;{1}{1}"
                            : "        if ({0}Param.Value == null){1}            {0}Param.Value = DBNull.Value;{1}{1}",
                        p.NameHumanCase, Environment.NewLine);
                }
            }

            if (includeProcResult && ReturnModels.Count < 2)
            {
                sb.AppendLine(
                    "        var procResultParam = new SqlParameter { ParameterName = \"@procResult\", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };");
            }

            return sb.ToString();
        }

        public string WriteTableValuedFunctionDeclareSqlParameter()
        {
            var sb = new StringBuilder(1024);
            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendLine(string.Format(
                    "        var {0}Param = new ObjectParameter(\"{1}\", typeof({2})) {{ Value = (object){3} }};",
                    p.NameHumanCase,
                    p.Name.Substring(1),
                    p.PropertyType,
                    p.NameHumanCase +
                    (p.Mode == StoredProcedureParameterMode.In &&
                     Column.NotNullable.Contains(p.PropertyType.ToLowerInvariant())
                        ? string.Empty
                        : " ?? DBNull.Value")));
            }

            return sb.ToString();
        }

        public string WriteStoredProcFunctionSqlParameterAnonymousArray(bool includeProcResultParam, bool appendParam)
        {
            var sb = new StringBuilder(255);
            var parameters = Parameters.OrderBy(x => x.Ordinal).ToList();
            var hasParam = parameters.Any();
            if (hasParam || includeProcResultParam)
                sb.Append(", ");

            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                sb.Append(string.Format("{0}{1}, ", p.NameHumanCase, appendParam ? "Param" : string.Empty));
                hasParam = true;
            }

            if (includeProcResultParam)
                sb.Append("procResultParam");
            else if (hasParam)
                sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public string WriteTableValuedFunctionSqlParameterAnonymousArray()
        {
            if (Parameters.Count == 0)
                return "new ObjectParameter[] { }";

            var sb = new StringBuilder(255);
            foreach (var p in Parameters.OrderBy(x => x.Ordinal))
            {
                sb.Append(string.Format("{0}Param, ", p.NameHumanCase));
            }

            return sb.ToString().Substring(0, sb.Length - 2);
        }

        public string WriteStoredProcFunctionSetSqlParameters(bool isFake)
        {
            var sb = new StringBuilder(255);
            foreach (var p in Parameters
                .Where(x => x.Mode != StoredProcedureParameterMode.In)
                .OrderBy(x => x.Ordinal))
            {
                var Default = string.Format("default({0})", p.PropertyType);
                var notNullable = Column.NotNullable.Contains(p.PropertyType.ToLower());

                if (isFake)
                {
                    sb.AppendLine(string.Format("        {0} = {1};", p.NameHumanCase, Default));
                }
                else
                {
                    sb.AppendLine(string.Format("        if (IsSqlParameterNull({0}Param))", p.NameHumanCase));
                    sb.AppendLine(string.Format("            {0} = {1};", p.NameHumanCase, notNullable ? Default : "null"));
                    sb.AppendLine("        else");
                    sb.AppendLine(string.Format("            {0} = ({1}) {2}Param.Value;", p.NameHumanCase, p.PropertyType, p.NameHumanCase));
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public string WriteStoredProcReturnModelName(IDbContextFilter filter)
        {
            if (Settings.StoredProcedureReturnTypes.ContainsKey(NameHumanCase))
                return Settings.StoredProcedureReturnTypes[NameHumanCase];
            if (Settings.StoredProcedureReturnTypes.ContainsKey(DbName))
                return Settings.StoredProcedureReturnTypes[DbName];

            var name = string.Format("{0}ReturnModel", NameHumanCase);
            var customName = filter.StoredProcedureReturnModelRename(name, this);
            if (!string.IsNullOrEmpty(customName))
                name = customName;

            return name;
        }

        public string WriteStoredProcReturnColumn(DataColumn col)
        {
            var columnName = DatabaseReader.ReservedKeywords.Contains(col.ColumnName) ? "@" + col.ColumnName : col.ColumnName;

            return string.Format("public {0} {1} {{ get; set; }}",
                WrapTypeIfNullable(ConvertDataColumnType(col.DataType), col),
                columnName);
        }

        private string ConvertDataColumnType(Type type)
        {
            if (type.Name.Equals("SqlHierarchyId"))
                return "Microsoft.SqlServer.Types.SqlHierarchyId";

            var typeNamespace = type.Namespace + ".";
            if (type.Namespace?.ToLower() == "system")
                typeNamespace = string.Empty;

            var typeName = type.Name;
            var isArray = typeName.EndsWith("[]");
            if (isArray)
                typeName = typeName.Replace("[]", string.Empty);
            switch (typeName.ToLower())
            {
                case "int16":
                    typeName = "short";
                    break;
                case "int32":
                    typeName = "int";
                    break;
                case "int64":
                    typeName = "long";
                    break;
                case "uint16":
                    typeName = "ushort";
                    break;
                case "uint32":
                    typeName = "uint";
                    break;
                case "uint64":
                    typeName = "ulong";
                    break;
                case "string":
                    typeName = "string";
                    break;
                case "decimal":
                    typeName = "decimal";
                    break;
                case "double":
                    typeName = "double";
                    break;
                case "float":
                    typeName = "float";
                    break;
                case "byte":
                    typeName = "byte";
                    break;
                case "boolean":
                    typeName = "bool";
                    break;
                default:
                    break;
            }

            if (isArray)
                typeName += "[]";

            return typeNamespace + typeName;
        }

        public string WriteStoredProcReturnType(IDbContextFilter filter)
        {
            var returnModelCount = ReturnModels.Count;
            if (returnModelCount == 0)
                return "int";

            var spReturnClassName = WriteStoredProcReturnModelName(filter);
            return returnModelCount == 1
                ? string.Format("List<{0}>", spReturnClassName)
                : spReturnClassName;
        }
    }
}