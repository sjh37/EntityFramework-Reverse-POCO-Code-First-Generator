// <auto-generated>

using System;
using System.Collections.Generic;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    public class StpMultipleResultsReturnModel
    {
        public class ResultSetModel1
        {
            public int codeObjectNo { get; set; }
            public int? applicationNo { get; set; }
            public int type { get; set; }
            public string eName { get; set; }
            public string aName { get; set; }
            public string description { get; set; }
            public string codeName { get; set; }
            public string note { get; set; }
            public bool isObject { get; set; }
            public byte[] versionNumber { get; set; }
        }
        public List<ResultSetModel1> ResultSet1;
        public class ResultSetModel2
        {
            public int Id { get; set; }
            public int PrimaryColourId { get; set; }
            public string CarMake { get; set; }
            public int? computed_column { get; set; }
            public int? computed_column_persisted { get; set; }
        }
        public List<ResultSetModel2> ResultSet2;
        public class ResultSetModel3
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public List<ResultSetModel3> ResultSet3;
    }

}
// </auto-generated>
