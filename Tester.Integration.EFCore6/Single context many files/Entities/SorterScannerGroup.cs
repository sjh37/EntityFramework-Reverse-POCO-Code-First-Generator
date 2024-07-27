// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore6.Single_context_many_files
{
    // SorterScannerGroup
    public class SorterScannerGroup
    {
        public string SorterName { get; set; } // SorterName (Primary key) (length: 20)

        // Foreign keys

        /// <summary>
        /// Parent Sorter pointed by [SorterScannerGroup].([SorterName]) (FK_SorterScannerGroup_Sorters)
        /// </summary>
        public virtual Sorter1 Sorter { get; set; } // FK_SorterScannerGroup_Sorters
    }

}
// </auto-generated>
