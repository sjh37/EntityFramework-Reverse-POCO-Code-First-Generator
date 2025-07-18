// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // CMS_File
    public class CmsFile
    {
        public int FileId { get; set; } // FileId (Primary key)
        public string FileName { get; set; } // FileName (length: 100)
        public string FileDescription { get; set; } // FileDescription (length: 500)
        public string FileIdentifier { get; set; } // FileIdentifier (length: 100)
        public DateTime? ValidStartDate { get; set; } // ValidStartDate
        public DateTime? ValidEndDate { get; set; } // ValidEndDate
        public bool IsActive { get; set; } // IsActive

        // Reverse navigation

        /// <summary>
        /// Child CmsFileTags where [CMS_FileTag].[FileId] point to this entity (FK_CMS_FileTag_CMS_File)
        /// </summary>
        public virtual ICollection<CmsFileTag> CmsFileTags { get; set; } // CMS_FileTag.FK_CMS_FileTag_CMS_File

        public CmsFile()
        {
            CmsFileTags = new List<CmsFileTag>();
        }
    }

}
// </auto-generated>
