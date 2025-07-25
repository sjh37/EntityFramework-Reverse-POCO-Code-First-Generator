// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // Person
    public class Person
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child PersonPosts where [PersonPosts].[CreatedBy] point to this entity (FK_PersonPosts_CreatedBy)
        /// </summary>
        public virtual ICollection<PersonPost> PersonPosts_CreatedBy { get; set; } // PersonPosts.FK_PersonPosts_CreatedBy

        /// <summary>
        /// Child PersonPosts where [PersonPosts].[UpdatedBy] point to this entity (FK_PersonPosts_UpdatedBy)
        /// </summary>
        public virtual ICollection<PersonPost> PersonPosts_UpdatedBy { get; set; } // PersonPosts.FK_PersonPosts_UpdatedBy

        public Person()
        {
            PersonPosts_CreatedBy = new List<PersonPost>();
            PersonPosts_UpdatedBy = new List<PersonPost>();
        }
    }

}
// </auto-generated>
