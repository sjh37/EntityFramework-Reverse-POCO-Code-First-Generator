// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace V5Fred
{
    // User309
    public class User309
    {
        public long UserId { get; set; } // UserID (Primary key)
        public string Lastname { get; set; } // Lastname (length: 100)
        public string Firstname { get; set; } // Firstname (length: 100)
        public int? PhoneCountryId { get; set; } // PhoneCountryID

        // Foreign keys

        /// <summary>
        /// Parent Country pointed by [User309].([PhoneCountryId]) (FK_User309_PhoneCountry)
        /// </summary>
        public virtual Country Country { get; set; } // FK_User309_PhoneCountry
    }

}
// </auto-generated>