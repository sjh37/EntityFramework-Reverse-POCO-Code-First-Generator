// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // DateTimeDefaultTest
    public class DateTimeDefaultTest
    {
        public int Id { get; set; } // Id (Primary key)
        public DateTimeOffset? CreatedDate { get; set; } // CreatedDate

        public DateTimeDefaultTest()
        {
            CreatedDate = DateTimeOffset.Now;
        }
    }

}
// </auto-generated>
