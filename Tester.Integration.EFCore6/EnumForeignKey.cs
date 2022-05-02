﻿// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnumForeignKey
{
    #region POCO classes

    // DaysOfWeek
    public class EnumTest_DaysOfWeek
    {
        public string TypeName { get; set; } // TypeName (length: 50)
        public int TypeId { get; set; } // TypeId (Primary key)

        // Reverse navigation

        /// <summary>
        /// Child EnumTest_OpenDays where [OpenDays].[EnumId] point to this entity (Fk_OpenDays_EnumId)
        /// </summary>
        public virtual ICollection<EnumTest_OpenDay> EnumTest_OpenDays { get; set; } // OpenDays.Fk_OpenDays_EnumId

        public EnumTest_DaysOfWeek()
        {
            EnumTest_OpenDays = new List<EnumTest_OpenDay>();
        }
    }

    // OpenDays
    public class EnumTest_OpenDay
    {
        public int Id { get; set; } // Id (Primary key)
        public int EnumId { get; set; } // EnumId

        // Foreign keys

        /// <summary>
        /// Parent EnumTest_DaysOfWeek pointed by [OpenDays].([EnumId]) (Fk_OpenDays_EnumId)
        /// </summary>
        public virtual EnumTest_DaysOfWeek EnumTest_DaysOfWeek { get; set; } // Fk_OpenDays_EnumId
    }


    #endregion

    #region Enumerations

    public enum DaysOfWeek
    {
        Sun = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 6,
        Sat = 7,
    }


    #endregion

}
// </auto-generated>
