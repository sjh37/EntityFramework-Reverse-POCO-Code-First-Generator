// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // tblOrders
    public class TblOrder
    {
        public int Id { get; set; } // ID (Primary key)
        public DateTime Added { get; set; } // added

        // Reverse navigation

        /// <summary>
        /// Child TblOrderLines where [tblOrderLines].[OrderID] point to this entity (tblOrdersFK)
        /// </summary>
        public virtual ICollection<TblOrderLine> TblOrderLines { get; set; } // tblOrderLines.tblOrdersFK

        public TblOrder()
        {
            Added = DateTime.Now;
            TblOrderLines = new List<TblOrderLine>();
        }
    }

}
// </auto-generated>
