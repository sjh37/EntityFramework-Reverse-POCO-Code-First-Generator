// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace V6EfrpgTest
{
    // NullableReverseNavA
    public class NullableReverseNavAConfiguration : IEntityTypeConfiguration<NullableReverseNavA>
    {
        public void Configure(EntityTypeBuilder<NullableReverseNavA> builder)
        {
            builder.ToTable("NullableReverseNavA", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_NullableReverseNavA").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Data).HasColumnName(@"Data").HasColumnType("nvarchar(100)").IsRequired(false).HasMaxLength(100);
        }
    }

}
// </auto-generated>
