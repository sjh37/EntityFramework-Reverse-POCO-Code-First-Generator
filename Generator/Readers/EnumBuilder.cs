using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Efrpg.Filtering;

namespace Efrpg.Readers
{
    // Transforms the raw enum rows returned by the efrpg tool into Enumeration/EnumerationMember
    // objects, applying the same casing, grouping and description rules the legacy
    // DatabaseReader.ReadEnums applied. Lives on the Generator (T4) side because it needs Inflector.
    public static class EnumBuilder
    {
        private static readonly Regex RemoveNonAlphaNumeric = new Regex(@"[^\w\d\s_-]", RegexOptions.Compiled);

        public static List<Enumeration> Build(List<EnumerationSettings> specs, List<RawEnum> rawEnums)
        {
            var result = new List<Enumeration>();
            if (specs == null || rawEnums == null)
                return result;

            var byIndex = new Dictionary<int, RawEnum>();
            foreach (var re in rawEnums)
                byIndex[re.Index] = re;

            for (var index = 0; index < specs.Count; index++)
            {
                RawEnum rawEnum;
                if (!byIndex.TryGetValue(index, out rawEnum))
                    continue;

                var e = specs[index];
                var enumDict = new Dictionary<string, List<EnumerationMember>>();

                foreach (var rawRow in rawEnum.Rows)
                {
                    var name = (rawRow.Name ?? string.Empty).Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    var group = string.Empty;
                    if (!string.IsNullOrEmpty(e.GroupField))
                    {
                        group = (rawRow.Group ?? string.Empty).Trim();
                        group = RemoveNonAlphaNumeric.Replace(group, string.Empty);
                        group = (Settings.UsePascalCaseForEnumMembers ? Inflector.ToTitleCase(group) : group).Replace(" ", string.Empty).Trim();
                    }

                    if (!enumDict.ContainsKey(group))
                        enumDict.Add(group, new List<EnumerationMember>());

                    name = RemoveNonAlphaNumeric.Replace(name, string.Empty);
                    name = (Settings.UsePascalCaseForEnumMembers ? Inflector.ToTitleCase(name) : name).Replace(" ", string.Empty).Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    var value = (rawRow.Value ?? string.Empty).Trim();
                    if (string.IsNullOrEmpty(value))
                        continue;

                    var allValues = new Dictionary<string, object>();
                    foreach (var kv in rawRow.AllValues)
                        allValues[kv.Key] = kv.Value;

                    var description = string.Empty;
                    if (!string.IsNullOrEmpty(e.DescriptionField) && allValues.ContainsKey(e.DescriptionField))
                    {
                        var descObj = allValues[e.DescriptionField];
                        if (descObj != null)
                            description = descObj.ToString().Trim();
                    }

                    if (string.IsNullOrEmpty(description) && e.GenerateDescriptionFromName)
                        description = Inflector.ToHumanCase(Inflector.AddUnderscores(name));

                    var member = new EnumerationMember(name, value, allValues);
                    if (!string.IsNullOrEmpty(description))
                        member.Attributes.Add("[Description(\"" + description.Replace("\"", "\\\"") + "\")]");

                    enumDict[group].Add(member);
                }

                foreach (var v in enumDict)
                {
                    if (v.Value.Any())
                        result.Add(new Enumeration(e.Name.Replace("{GroupField}", v.Key), v.Value));
                }
            }

            return result;
        }
    }
}
