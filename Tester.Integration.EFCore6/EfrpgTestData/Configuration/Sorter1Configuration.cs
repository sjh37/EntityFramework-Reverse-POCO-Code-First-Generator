// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace V6EfrpgTest
{
    // Sorters
    public class Sorter1Configuration : IEntityTypeConfiguration<Sorter1>
    {
        public void Configure(EntityTypeBuilder<Sorter1> builder)
        {
            builder.ToTable("Sorters", "dbo");
            builder.HasKey(x => x.SorterName).HasName("PK_Sorter").IsClustered();

            builder.Property(x => x.SorterName).HasColumnName(@"SorterName").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20).ValueGeneratedNever();
        }
    }

}
// </auto-generated>
