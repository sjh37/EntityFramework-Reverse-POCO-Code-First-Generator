// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore6.Single_context_many_files
{
    // Sorter
    public class SorterConfiguration : IEntityTypeConfiguration<Sorter>
    {
        public void Configure(EntityTypeBuilder<Sorter> builder)
        {
            builder.ToTable("Sorter", "dbo");
            builder.HasKey(x => x.SorterId).HasName("PK_Sorter2").IsClustered();

            builder.Property(x => x.SorterId).HasColumnName(@"SorterID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.SorterName).HasColumnName(@"SorterName").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20);
        }
    }

}
// </auto-generated>
