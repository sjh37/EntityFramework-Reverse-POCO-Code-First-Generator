// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // Blah
    public class Blah
    {
        public int BlahId { get; set; } // BlahID (Primary key)

        // Reverse navigation

        /// <summary>
        /// Child BlahBlahLinks where [BlahBlahLink].[BlahID] point to this entity (FK_BlahBlahLink_Blah)
        /// </summary>
        public virtual ICollection<BlahBlahLink> BlahBlahLinks_BlahId { get; set; } // BlahBlahLink.FK_BlahBlahLink_Blah

        /// <summary>
        /// Child BlahBlahLinks where [BlahBlahLink].[BlahID2] point to this entity (FK_BlahBlahLink_Blah2)
        /// </summary>
        public virtual ICollection<BlahBlahLink> BlahBlahLinks_BlahId2 { get; set; } // BlahBlahLink.FK_BlahBlahLink_Blah2

        /// <summary>
        /// Child BlahBlahLinkReadonlies where [BlahBlahLink_readonly].[BlahID] point to this entity (FK_BlahBlahLink_Blah_ro)
        /// </summary>
        public virtual ICollection<BlahBlahLinkReadonly> BlahBlahLinkReadonlies_BlahId { get; set; } // BlahBlahLink_readonly.FK_BlahBlahLink_Blah_ro

        /// <summary>
        /// Child BlahBlahLinkReadonlies where [BlahBlahLink_readonly].[BlahID2] point to this entity (FK_BlahBlahLink_Blah_ro2)
        /// </summary>
        public virtual ICollection<BlahBlahLinkReadonly> BlahBlahLinkReadonlies_BlahId2 { get; set; } // BlahBlahLink_readonly.FK_BlahBlahLink_Blah_ro2

        /// <summary>
        /// Child BlahBlahLinkV2 where [BlahBlahLink_v2].[BlahID] point to this entity (FK_BlahBlahLinkv2_Blah_ro)
        /// </summary>
        public virtual ICollection<BlahBlahLinkV2> BlahBlahLinkV2_BlahId { get; set; } // BlahBlahLink_v2.FK_BlahBlahLinkv2_Blah_ro

        /// <summary>
        /// Child BlahBlahLinkV2 where [BlahBlahLink_v2].[BlahID2] point to this entity (FK_BlahBlahLinkv2_Blah_ro2)
        /// </summary>
        public virtual ICollection<BlahBlahLinkV2> BlahBlahLinkV2_BlahId2 { get; set; } // BlahBlahLink_v2.FK_BlahBlahLinkv2_Blah_ro2

        /// <summary>
        /// Child BlahBlargLinks where [BlahBlargLink].[BlahID] point to this entity (FK_BlahBlargLink_Blah)
        /// </summary>
        public virtual ICollection<BlahBlargLink> BlahBlargLinks { get; set; } // BlahBlargLink.FK_BlahBlargLink_Blah

        public Blah()
        {
            BlahBlahLinks_BlahId = new List<BlahBlahLink>();
            BlahBlahLinks_BlahId2 = new List<BlahBlahLink>();
            BlahBlahLinkReadonlies_BlahId = new List<BlahBlahLinkReadonly>();
            BlahBlahLinkReadonlies_BlahId2 = new List<BlahBlahLinkReadonly>();
            BlahBlahLinkV2_BlahId = new List<BlahBlahLinkV2>();
            BlahBlahLinkV2_BlahId2 = new List<BlahBlahLinkV2>();
            BlahBlargLinks = new List<BlahBlargLink>();
        }
    }

}
// </auto-generated>
