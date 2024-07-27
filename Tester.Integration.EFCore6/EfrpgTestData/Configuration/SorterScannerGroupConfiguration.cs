// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace V6EfrpgTest
{
    // SorterScannerGroup
    public class SorterScannerGroupConfiguration : IEntityTypeConfiguration<SorterScannerGroup>
    {
        public void Configure(EntityTypeBuilder<SorterScannerGroup> builder)
        {
            builder.ToTable("SorterScannerGroup", "dbo");
            builder.HasKey(x => x.SorterName);

            builder.Property(x => x.SorterName).HasColumnName(@"SorterName").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20).ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Sorter).WithOne(b => b.SorterScannerGroup).HasForeignKey<SorterScannerGroup>(c => c.SorterName).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SorterScannerGroup_Sorters");
        }
    }

}
// </auto-generated>
