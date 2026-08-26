using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Efrpg.Filtering;

namespace Efrpg.Readers
{
    public static class EfrpgResultXmlReader
    {
        // Lowest wire-format version this template can work with. The efrpg tool is installed globally and shared by
        // every project on the machine, whereas this template is pinned inside one project and rarely upgraded, so a
        // NEWER tool meeting an older template is the normal case and must keep working - attributes this reader does
        // not know about are simply ignored. Only an OLDER tool is a problem, hence a floor check and not a match.
        // Raise this only when the reader starts depending on something a previous tool did not emit, and read the
        // "Wire format contract" section of AGENTS.md first.
        public const int RequiredSchemaVersion = 1;

        public static EfrpgResult Read(string xml)
        {
            var root = XDocument.Parse(xml).Root;
            if (root == null || root.Name != "EfrpgResult")
                throw new Exception("Unexpected output from the efrpg tool - an EfrpgResult document was expected. Check that nothing else on the PATH is named efrpg");

            // A tool built before the version handshake existed emits no schemaVersion at all, which reads back as 0
            // and so fails this check - which is what we want, rather than assuming it is compatible.
            var schemaVersion = Int(root, "schemaVersion");
            if (schemaVersion < RequiredSchemaVersion)
                throw new Exception(string.Format(
                    "The installed efrpg tool is too old for this template. Tool version {0} provides wire format version {1}, but this template needs version {2} or later. Update it with: dotnet tool update -g Efrpg",
                    ToolVersionText(root), schemaVersion, RequiredSchemaVersion));

            var result = new EfrpgResult
            {
                DefaultSchema                = Str(root, "defaultSchema"),
                DatabaseDetails              = Str(root, "databaseDetails"),
                IncludeSchema                = Bool(root, "includeSchema"),
                CanReadStoredProcedures      = Bool(root, "canReadStoredProcedures"),
                HasIdentityColumnSupport     = Bool(root, "hasIdentityColumnSupport"),
                DoNotSpecifySizeForMaxLength = Bool(root, "doNotSpecifySizeForMaxLength"),
            };
            result.Tables                = Rows(root, "Tables",               ReadTable);
            result.ForeignKeys           = Rows(root, "ForeignKeys",          ReadForeignKey);
            result.Indexes               = Rows(root, "Indexes",              ReadIndex);
            result.ExtendedProperties    = Rows(root, "ExtendedProperties",   ReadExtendedProperty);
            result.Sequences             = Rows(root, "Sequences",            ReadSequence);
            result.Triggers              = Rows(root, "Triggers",             ReadTrigger);
            result.MemoryOptimisedTables = Rows(root, "MemoryOptimisedTables",ReadMemoryOptimisedTable);
            result.StoredProcedures      = Rows(root, "StoredProcedures",     ReadStoredProcedure);
            result.Errors                = Rows(root, "Errors",               ReadError);

            var mcs = root.Element("MultiContextSettings");
            if (mcs != null)
                result.MultiContextSettings = mcs.Elements("Context").Select(ReadContext).ToList();

            return result;
        }

        private static List<T> Rows<T>(XElement parent, string section, Func<XElement, T> reader)
        {
            var el = parent.Element(section);
            return el != null ? el.Elements("Row").Select(reader).ToList() : new List<T>();
        }

        private static RawTable ReadTable(XElement e)
        {
            return new RawTable(
                Str(e, "schemaName"), Str(e, "tableName"),
                Bool(e, "isView"), Bool(e, "isSynonym"),
                Int(e, "scale"), Str(e, "typeName"),
                Bool(e, "isNullable"), Int(e, "maxLength"),
                Int(e, "dateTimePrecision"), Int(e, "precision"),
                Bool(e, "isIdentity"), Bool(e, "isComputed"),
                Bool(e, "isRowGuid"), Byt(e, "generatedAlwaysType"),
                Bool(e, "isStoreGenerated"), Int(e, "primaryKeyOrdinal"),
                Bool(e, "primaryKey"), Bool(e, "isForeignKey"),
                Str(e, "synonymTriggerName"),
                Int(e, "ordinal"), Str(e, "columnName"), Str(e, "default")
            );
        }

        private static RawForeignKey ReadForeignKey(XElement e)
        {
            return new RawForeignKey(
                Str(e, "constraintName"), Str(e, "parentName"), Str(e, "childName"),
                Str(e, "pkColumn"), Str(e, "fkColumn"),
                Str(e, "pkSchema"), Str(e, "pkTableName"),
                Str(e, "fkSchema"), Str(e, "fkTableName"),
                Int(e, "ordinal"), Bool(e, "cascadeOnDelete"),
                Bool(e, "isNotEnforced"), Bool(e, "hasUniqueConstraint")
            );
        }

        private static RawIndex ReadIndex(XElement e)
        {
            return new RawIndex(
                Str(e, "schema"), Str(e, "tableName"), Str(e, "indexName"),
                Byt(e, "keyOrdinal"), Str(e, "columnName"), Int(e, "columnCount"),
                Bool(e, "isUnique"), Bool(e, "isPrimaryKey"),
                Bool(e, "isUniqueConstraint"), Bool(e, "isClustered"),
                Str(e, "filterDefinition"), Str(e, "includedColumns")
            );
        }

        private static RawExtendedProperty ReadExtendedProperty(XElement e)
        {
            return new RawExtendedProperty(
                Str(e, "schemaName"), Str(e, "tableName"), Str(e, "columnName"),
                Str(e, "propertyName"), Str(e, "extendedProperty")
            );
        }

        private static RawSequence ReadSequence(XElement e)
        {
            var seq = new RawSequence(
                Str(e, "schema"), Str(e, "name"), Str(e, "dataType"),
                Str(e, "startValue"), Str(e, "incrementValue"),
                Str(e, "minValue"), Str(e, "maxValue"),
                string.Equals(Str(e, "isCycleEnabled"), "true", StringComparison.OrdinalIgnoreCase)
            );
            var mappings = e.Element("TableMappings");
            if (mappings != null)
                foreach (var m in mappings.Elements("Row"))
                    seq.TableMapping.Add(new RawSequenceTableMapping(Str(m, "tableSchema"), Str(m, "tableName")));
            return seq;
        }

        private static RawTrigger ReadTrigger(XElement e)
        {
            return new RawTrigger(Str(e, "schemaName"), Str(e, "tableName"), Str(e, "triggerName"));
        }

        private static RawMemoryOptimisedTable ReadMemoryOptimisedTable(XElement e)
        {
            return new RawMemoryOptimisedTable(Str(e, "schemaName"), Str(e, "tableName"));
        }

        private static RawStoredProcedure ReadStoredProcedure(XElement e)
        {
            StoredProcedureParameter param = null;
            var pe = e.Element("Parameter");
            if (pe != null)
            {
                param = new StoredProcedureParameter
                {
                    Ordinal             = Int(pe, "ordinal"),
                    Mode                = (StoredProcedureParameterMode)Enum.Parse(typeof(StoredProcedureParameterMode), Str(pe, "mode"), true),
                    Name                = Str(pe, "name"),
                    NameHumanCase       = Str(pe, "nameHumanCase"),
                    DataType            = Str(pe, "dataType"),
                    ReturnDataType      = Str(pe, "returnDataType"),
                    SqlDbType           = Str(pe, "sqlDbType"),
                    ReturnSqlDbType     = Str(pe, "returnSqlDbType"),
                    PropertyType        = Str(pe, "propertyType"),
                    ReturnPropertyType  = Str(pe, "returnPropertyType"),
                    UserDefinedTypeName = Str(pe, "userDefinedTypeName"),
                    DateTimePrecision   = Int(pe, "dateTimePrecision"),
                    MaxLength           = Int(pe, "maxLength"),
                    Precision           = Int(pe, "precision"),
                    Scale               = Int(pe, "scale"),
                    IsSpatial           = Bool(pe, "isSpatial"),
                    HasDefault          = Bool(pe, "hasDefault"),
                    // Missing attribute means null (the writer omits it for null): DefaultValue == null is what makes
                    // an AllowNullStrings string parameter 'string?', so "" must not be substituted here.
                    DefaultValue        = StrOrNull(pe, "defaultValue"),
                };
            }
            var sp = new RawStoredProcedure(
                Str(e, "schema"), Str(e, "name"),
                Bool(e, "isTableValuedFunction"), Bool(e, "isScalarValuedFunction"),
                Bool(e, "isStoredProcedure"), param
            );
            sp.ReturnModelsRead = Bool(e, "returnModelsRead");
            sp.ReturnModelError = Str(e, "returnModelError");

            var returnModels = e.Element("ReturnModels");
            if (returnModels != null)
            {
                foreach (var resultSet in returnModels.Elements("ResultSet").OrderBy(x => Int(x, "index")))
                {
                    sp.ReturnModels.Add(resultSet
                        .Elements("Column")
                        .Select(ReadReturnColumn)
                        .ToList());
                }
            }

            return sp;
        }

        private static RawStoredProcedureReturnColumn ReadReturnColumn(XElement e)
        {
            return new RawStoredProcedureReturnColumn(
                Str(e, "columnName"),
                Str(e, "dataTypeFullName"),
                Str(e, "dataTypeName"),
                Str(e, "dataTypeNamespace"),
                Str(e, "dataTypeAssemblyQualifiedName"),
                Bool(e, "allowDBNull"),
                Bool(e, "unique"),
                Byt(e, "precision"),
                Byt(e, "scale")
            );
        }

        private static EfrpgError ReadError(XElement e)
        {
            return new EfrpgError(Str(e, "type"), Str(e, "message"));
        }

        private static string ToolVersionText(XElement root)
        {
            var version = Str(root, "toolVersion");
            return string.IsNullOrEmpty(version) ? "(unknown, predates the version handshake)" : version;
        }

        private static string Str(XElement e, string attr)
        {
            return (string)e.Attribute(attr) ?? string.Empty;
        }

        private static string StrOrNull(XElement e, string attr)
        {
            return (string)e.Attribute(attr);
        }

        private static bool Bool(XElement e, string attr)
        {
            return string.Equals(Str(e, attr), "true", StringComparison.OrdinalIgnoreCase);
        }

        private static int Int(XElement e, string attr)
        {
            int n;
            return int.TryParse(Str(e, attr), out n) ? n : 0;
        }

        private static byte Byt(XElement e, string attr)
        {
            byte b;
            return byte.TryParse(Str(e, attr), out b) ? b : (byte)0;
        }

        private static bool? BoolOpt(XElement e, string attr)
        {
            var s = Str(e, attr);
            if (string.IsNullOrEmpty(s))
                return null;
            return string.Equals(s, "true", StringComparison.OrdinalIgnoreCase);
        }

        private static List<T> ChildRows<T>(XElement parent, string section, string itemName, Func<XElement, T> reader)
        {
            var el = parent.Element(section);
            return el != null ? el.Elements(itemName).Select(reader).ToList() : new List<T>();
        }

        private static Dictionary<string, object> ReadAllFields(XElement e)
        {
            var result = new Dictionary<string, object>();
            var af = e.Element("AllFields");
            if (af != null)
            {
                foreach (var f in af.Elements("Field"))
                {
                    var key = Str(f, "name");
                    if (!string.IsNullOrEmpty(key) && !result.ContainsKey(key))
                        result[key] = Str(f, "value");
                }
            }
            return result;
        }

        private static MultiContextSettings ReadContext(XElement e)
        {
            return new MultiContextSettings
            {
                Name             = Str(e, "name"),
                Description      = Str(e, "description"),
                Namespace        = Str(e, "namespace"),
                TemplatePath     = Str(e, "templatePath"),
                Filename         = Str(e, "filename"),
                BaseSchema       = Str(e, "baseSchema"),
                AllFields        = ReadAllFields(e),
                Tables           = ChildRows(e, "Tables",           "Table",           ReadContextTable),
                StoredProcedures = ChildRows(e, "StoredProcedures", "StoredProcedure", ReadContextStoredProcedure),
                Functions        = ChildRows(e, "Functions",        "Function",        ReadContextFunction),
                Enumerations     = ChildRows(e, "Enumerations",     "Enumeration",     ReadContextEnumeration),
                ForeignKeys      = ChildRows(e, "ForeignKeys",      "ForeignKey",      ReadContextForeignKey),
            };
        }

        private static MultiContextTableSettings ReadContextTable(XElement e)
        {
            return new MultiContextTableSettings
            {
                Name          = Str(e, "name"),
                Description   = Str(e, "description"),
                PluralName    = Str(e, "pluralName"),
                DbName        = Str(e, "dbName"),
                Attributes    = Str(e, "attributes"),
                DbSetModifier = Str(e, "dbSetModifier"),
                AllFields     = ReadAllFields(e),
                Columns       = ChildRows(e, "Columns", "Column", ReadContextColumn),
            };
        }

        private static MultiContextColumnSettings ReadContextColumn(XElement e)
        {
            return new MultiContextColumnSettings
            {
                Name             = Str(e, "name"),
                DbName           = Str(e, "dbName"),
                IsPrimaryKey     = BoolOpt(e, "isPrimaryKey"),
                OverrideModifier = BoolOpt(e, "overrideModifier"),
                EnumType         = Str(e, "enumType"),
                Attributes       = Str(e, "attributes"),
                PropertyType     = Str(e, "propertyType"),
                IsNullable       = BoolOpt(e, "isNullable"),
                AllFields        = ReadAllFields(e),
            };
        }

        private static MultiContextStoredProcedureSettings ReadContextStoredProcedure(XElement e)
        {
            return new MultiContextStoredProcedureSettings
            {
                Name        = Str(e, "name"),
                DbName      = Str(e, "dbName"),
                ReturnModel = Str(e, "returnModel"),
                AllFields   = ReadAllFields(e),
            };
        }

        private static MultiContextFunctionSettings ReadContextFunction(XElement e)
        {
            return new MultiContextFunctionSettings
            {
                Name      = Str(e, "name"),
                DbName    = Str(e, "dbName"),
                AllFields = ReadAllFields(e),
            };
        }

        private static EnumerationSettings ReadContextEnumeration(XElement e)
        {
            return new EnumerationSettings
            {
                Name                        = Str(e, "name"),
                Table                       = Str(e, "table"),
                NameField                   = Str(e, "nameField"),
                ValueField                  = Str(e, "valueField"),
                GroupField                  = Str(e, "groupField"),
                DescriptionField            = Str(e, "descriptionField"),
                GenerateDescriptionFromName = Bool(e, "generateDescriptionFromName"),
                AllFields                   = ReadAllFields(e),
            };
        }

        private static MultiContextForeignKeySettings ReadContextForeignKey(XElement e)
        {
            return new MultiContextForeignKeySettings
            {
                ConstraintName      = Str(e, "constraintName"),
                ParentName          = Str(e, "parentName"),
                ChildName           = Str(e, "childName"),
                PkSchema            = Str(e, "pkSchema"),
                PkTableName         = Str(e, "pkTableName"),
                PkColumn            = Str(e, "pkColumn"),
                FkSchema            = Str(e, "fkSchema"),
                FkTableName         = Str(e, "fkTableName"),
                FkColumn            = Str(e, "fkColumn"),
                Ordinal             = Int(e, "ordinal"),
                CascadeOnDelete     = Bool(e, "cascadeOnDelete"),
                IsNotEnforced       = Bool(e, "isNotEnforced"),
                HasUniqueConstraint = Bool(e, "hasUniqueConstraint"),
            };
        }
    }
}
