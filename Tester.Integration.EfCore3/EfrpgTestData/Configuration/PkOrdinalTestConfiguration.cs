// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EfCore3
{
    // pk_ordinal_test
    public class PkOrdinalTestConfiguration : IEntityTypeConfiguration<PkOrdinalTest>
    {
        public void Configure(EntityTypeBuilder<PkOrdinalTest> builder)
        {
            builder.ToTable("pk_ordinal_test", "dbo");
            builder.HasKey(x => new { x.C3, x.C1 }).HasName("PK_pk_ordinal_test").IsClustered();

            builder.Property(x => x.C1).HasColumnName(@"C1").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.C2).HasColumnName(@"C2").HasColumnType("int").IsRequired();
            builder.Property(x => x.C3).HasColumnName(@"C3").HasColumnType("int").IsRequired().ValueGeneratedNever();
        }
    }

}
// </auto-generated>
