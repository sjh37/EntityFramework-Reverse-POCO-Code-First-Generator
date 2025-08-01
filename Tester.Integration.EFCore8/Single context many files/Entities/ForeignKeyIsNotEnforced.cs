// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // ForeignKeyIsNotEnforced
    public class ForeignKeyIsNotEnforced
    {
        public int Id { get; set; } // id (Primary key)
        public int? NullValue { get; set; } // null_value
        public int NotNullValue { get; set; } // not_null_value

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) ForeignKeyIsNotEnforced pointed by [ForeignKeyIsNotEnforcedItem].[not_null_value] (FK_ForeignKeyIsNotEnforcedItem_notnull_notnull)
        /// </summary>
        public virtual ForeignKeyIsNotEnforcedItem ForeignKeyIsNotEnforcedItem_NotNullValue { get; set; } // ForeignKeyIsNotEnforcedItem.FK_ForeignKeyIsNotEnforcedItem_notnull_notnull

        /// <summary>
        /// Parent (One-to-One) ForeignKeyIsNotEnforced pointed by [ForeignKeyIsNotEnforcedItem].[null_value] (FK_ForeignKeyIsNotEnforcedItem_null_notnull)
        /// </summary>
        public virtual ForeignKeyIsNotEnforcedItem ForeignKeyIsNotEnforcedItem_NullValue { get; set; } // ForeignKeyIsNotEnforcedItem.FK_ForeignKeyIsNotEnforcedItem_null_notnull
    }

}
// </auto-generated>
