using System;
using System.Collections.Generic;
using System.Linq;

namespace Efrpg
{
    public class Tables : List<Table>
    {
        public Table GetTable(string tableName, string schema)
        {
            return this.SingleOrDefault(x =>
                string.Compare(x.DbName, tableName, StringComparison.OrdinalIgnoreCase) == 0 &&
                string.Compare(x.Schema.DbName, schema, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void IdentifyMappingTables(List<ForeignKey> fkList, bool checkForFkNameClashes, bool includeSchema)
        {
            foreach (var tbl in this.Where(x => x.HasForeignKey))
            {
                tbl.IdentifyMappingTable(fkList, this, checkForFkNameClashes, includeSchema);
            }
        }

        public void ResetNavigationProperties()
        {
            foreach (var tbl in this)
            {
                tbl.ResetNavigationProperties();
            }
        }

        public void TrimForAcademicLicence()
        {
            // Mapping tables do not count
            //const int n = 2 * 2 * 2 * 2;
            //TrimForLicence(n);
        }

        public void TrimForTrialLicence()
        {
            // Mapping tables do not count
            const int n = 1 + 2 + 3 + 4;
            TrimForLicence(n);
        }

        private void TrimForLicence(int n)
        {
            if (this.Count(x => !x.IsMapping) <= n)
                return;

            RemoveAll(x => !x.HasPrimaryKey);

            while (this.Count(x => !x.IsMapping) > n)
            {
                try
                {
                    var index = FindIndex(x => !x.IsMapping);
                    RemoveAt(index);
                }
                catch
                {
                    // Cannot remove anymore
                    return;
                }
            }
        }
    }
}