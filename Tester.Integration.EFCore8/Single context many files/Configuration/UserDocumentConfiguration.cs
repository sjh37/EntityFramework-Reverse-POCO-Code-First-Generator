// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // User_Document
    public class UserDocumentConfiguration : IEntityTypeConfiguration<UserDocument>
    {
        public void Configure(EntityTypeBuilder<UserDocument> builder)
        {
            builder.ToTable("User_Document", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_User_Document").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.UserId).HasColumnName(@"UserID").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedByUserId).HasColumnName(@"CreatedByUserID").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.CreatedByUser).WithMany(b => b.UserDocuments_CreatedByUserId).HasForeignKey(c => c.CreatedByUserId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_User_Document_User1");
            builder.HasOne(a => a.User_UserId).WithMany(b => b.UserDocuments_UserId).HasForeignKey(c => c.UserId).HasConstraintName("FK_User_Document_User");
        }
    }

}
// </auto-generated>
