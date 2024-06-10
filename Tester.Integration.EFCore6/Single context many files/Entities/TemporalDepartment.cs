// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore6.Single_context_many_files
{
    // TemporalDepartment
    public class TemporalDepartment
    {
        public int DeptId { get; set; } // DeptID (Primary key)
        public string DeptName { get; set; } // DeptName (length: 50)
        public int? ManagerId { get; set; } // ManagerID
        public int? ParentDeptId { get; set; } // ParentDeptID
        public DateTime SysStartTime { get; set; } // SysStartTime
        public DateTime SysEndTime { get; set; } // SysEndTime
    }

}
// </auto-generated>
