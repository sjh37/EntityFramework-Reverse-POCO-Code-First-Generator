// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore6.Single_context_many_files
{
    // Sorters
    public class Sorter1
    {
        public string SorterName { get; set; } // SorterName (Primary key) (length: 20)

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) Sorter1 pointed by [SorterScannerGroup].[SorterName] (FK_SorterScannerGroup_Sorters)
        /// </summary>
        public virtual SorterScannerGroup SorterScannerGroup { get; set; } // SorterScannerGroup.FK_SorterScannerGroup_Sorters
    }

}
// </auto-generated>
