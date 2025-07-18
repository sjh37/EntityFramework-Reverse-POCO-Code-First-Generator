// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // Country
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country", "dbo");
            builder.HasKey(x => x.CountryId).HasName("PK_Country").IsClustered();

            builder.Property(x => x.CountryId).HasColumnName(@"CountryID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType("varchar(12)").IsRequired(false).IsUnicode(false).HasMaxLength(12);
        }
    }

}
// </auto-generated>
