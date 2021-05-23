using System.Collections.Generic;
using System.Linq;
using Efrpg.Filtering;

namespace Efrpg.ForeignKeyStrategies
{
    // v0.0.0 <= v3.4.0
    public class LegacyForeignKeyNamingStrategy : BaseForeignKeyNamingStrategy, IForeignKeyNamingStrategy
    {
        public List<string> ReverseNavigationUniquePropName;
        public List<string> ReverseNavigationUniquePropNameClashes;

        public LegacyForeignKeyNamingStrategy(IDbContextFilter filter, Table table)
            : base(filter, table)
        {
            ReverseNavigationUniquePropNameClashes = new List<string>();
        }

        public string GetUniqueForeignKeyName(bool isParent, string tableNameHumanCase, ForeignKey foreignKey, bool checkForFkNameClashes, bool makeSingular,
            Relationship relationship)
        {
            var userSpecifiedName = CheckForUserSpecifiedName(isParent, foreignKey);
            if (!string.IsNullOrEmpty(userSpecifiedName))
                return userSpecifiedName;

            var addReverseNavigationUniquePropName = checkForFkNameClashes &&
                                                     (_table.DbName == foreignKey.FkTableName ||
                                                      (_table.DbName == foreignKey.PkTableName && foreignKey.IncludeReverseNavigation));

            // Generate name
            if (ReverseNavigationUniquePropName.Count == 0)
            {
                // Reserve table name and all column names
                ReverseNavigationUniquePropName.Add(_table.NameHumanCase);
                ReverseNavigationUniquePropName.AddRange(_table.Columns.Select(c => c.NameHumanCase));
            }

            if (!makeSingular)
                tableNameHumanCase = Inflector.MakePlural(tableNameHumanCase);

            if (checkForFkNameClashes &&
                ReverseNavigationUniquePropName.Contains(tableNameHumanCase) &&
                !ReverseNavigationUniquePropNameClashes.Contains(tableNameHumanCase))
            {
                ReverseNavigationUniquePropNameClashes.Add(tableNameHumanCase); // Name clash
            }

            // Attempt 1
            var fkName = (Settings.UsePascalCase ? Inflector.ToTitleCase(foreignKey.FkColumn) : foreignKey.FkColumn).Replace(" ", string.Empty).Replace("$", string.Empty);
            var name = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 1);
            string col;
            if (!ReverseNavigationUniquePropName.Contains(name) &&
                !ReverseNavigationUniquePropNameClashes.Contains(name))
            {
                if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
                {
                    ReverseNavigationUniquePropName.Add(name);
                    foreignKey.UniqueName = name;
                }

                return name;
            }

            if (_table.DbName == foreignKey.FkTableName)
            {
                // Attempt 2
                if (fkName.Length > 2 && fkName.ToLowerInvariant().EndsWith("id"))
                {
                    col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 2);

                    if (checkForFkNameClashes &&
                        ReverseNavigationUniquePropName.Contains(col) &&
                        !ReverseNavigationUniquePropNameClashes.Contains(col))
                    {
                        ReverseNavigationUniquePropNameClashes.Add(col); // Name clash
                    }

                    if (!ReverseNavigationUniquePropName.Contains(col) &&
                        !ReverseNavigationUniquePropNameClashes.Contains(col))
                    {
                        if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
                        {
                            ReverseNavigationUniquePropName.Add(col);
                        }

                        return col;
                    }
                }

                // Attempt 3
                col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 3);
                if (checkForFkNameClashes &&
                    ReverseNavigationUniquePropName.Contains(col) &&
                    !ReverseNavigationUniquePropNameClashes.Contains(col))
                {
                    ReverseNavigationUniquePropNameClashes.Add(col); // Name clash
                }

                if (!ReverseNavigationUniquePropName.Contains(col) &&
                    !ReverseNavigationUniquePropNameClashes.Contains(col))
                {
                    if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
                    {
                        ReverseNavigationUniquePropName.Add(col);
                    }

                    return col;
                }
            }

            // Attempt 4
            col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 4);
            if (checkForFkNameClashes &&
                ReverseNavigationUniquePropName.Contains(col) &&
                !ReverseNavigationUniquePropNameClashes.Contains(col))
            {
                ReverseNavigationUniquePropNameClashes.Add(col); // Name clash
            }

            if (!ReverseNavigationUniquePropName.Contains(col) &&
                !ReverseNavigationUniquePropNameClashes.Contains(col))
            {
                if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
                {
                    ReverseNavigationUniquePropName.Add(col);
                }

                return col;
            }

            // Attempt 5
            for (var n = 1; n < 99; ++n)
            {
                col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 5) + n;

                if (ReverseNavigationUniquePropName.Contains(col))
                    continue;

                if (addReverseNavigationUniquePropName || !checkForFkNameClashes)
                {
                    ReverseNavigationUniquePropName.Add(col);
                }

                return col;
            }

            // Give up
            return Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 6);
        }

        public void ResetNavigationProperties()
        {
            ReverseNavigationUniquePropName = new List<string>();
        }
    }
}