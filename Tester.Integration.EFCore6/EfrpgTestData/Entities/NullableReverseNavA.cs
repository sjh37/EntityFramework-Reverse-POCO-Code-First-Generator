// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace V6EfrpgTest
{
    // NullableReverseNavA
    public class NullableReverseNavA
    {
        public Guid Id { get; set; } // Id (Primary key)
        public string Data { get; set; } // Data (length: 100)

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) NullableReverseNavA pointed by [NullableReverseNavB].[Id] (FK_NullableReverseNavB_A)
        /// </summary>
        public virtual NullableReverseNavB NullableReverseNavB { get; set; } // NullableReverseNavB.FK_NullableReverseNavB_A
    }

}
// </auto-generated>
