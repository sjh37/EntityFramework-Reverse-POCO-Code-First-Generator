using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Efrpg.Filtering;

namespace Efrpg.Readers
{
    // Generator side of the enum exchange:
    //   WriteSpecs - serialises the resolved enumeration specs to send to the tool (--enums-base64)
    //   ReadData   - parses the raw enum rows the tool read from the database
    public static class EnumXml
    {
        public static string WriteSpecs(List<EnumerationSettings> specs)
        {
            var root = new XElement("Enums",
                specs.Select(s => new XElement("Enum",
                    new XAttribute("name",                        s.Name ?? string.Empty),
                    new XAttribute("table",                       s.Table ?? string.Empty),
                    new XAttribute("nameField",                   s.NameField ?? string.Empty),
                    new XAttribute("valueField",                  s.ValueField ?? string.Empty),
                    new XAttribute("groupField",                  s.GroupField ?? string.Empty),
                    new XAttribute("descriptionField",            s.DescriptionField ?? string.Empty),
                    new XAttribute("generateDescriptionFromName", s.GenerateDescriptionFromName))));
            return root.ToString(SaveOptions.DisableFormatting);
        }

        public static List<RawEnum> ReadData(string xml)
        {
            var result = new List<RawEnum>();
            var root = XDocument.Parse(xml).Root;
            if (root == null)
                return result;

            foreach (var e in root.Elements("Enum"))
            {
                var rawEnum = new RawEnum { Index = Int(e, "index") };
                foreach (var r in e.Elements("Row"))
                {
                    var row = new RawEnumRow
                    {
                        Name  = Str(r, "name"),
                        Value = Str(r, "value"),
                        Group = Str(r, "group"),
                    };
                    foreach (var f in r.Elements("Field"))
                    {
                        var key = Str(f, "name");
                        if (!string.IsNullOrEmpty(key) && !row.AllValues.ContainsKey(key))
                            row.AllValues[key] = Str(f, "value");
                    }
                    rawEnum.Rows.Add(row);
                }
                result.Add(rawEnum);
            }

            return result;
        }

        private static string Str(XElement e, string attr)
        {
            return (string)e.Attribute(attr) ?? string.Empty;
        }

        private static int Int(XElement e, string attr)
        {
            int n;
            return int.TryParse(Str(e, attr), out n) ? n : 0;
        }
    }
}
