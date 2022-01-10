// <auto-generated>

using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore6.Multi_context_many_filesCherry
{
    // ColumnNames
    public class ColumnNameConfiguration : IEntityTypeConfiguration<ColumnName>
    {
        public void Configure(EntityTypeBuilder<ColumnName> builder)
        {
            builder.ToTable("ColumnNames", "dbo");
            builder.HasKey(x => x.Dollar).HasName("PK_ColumnNames").IsClustered();

            builder.Property(x => x.Dollar).HasColumnName(@"$").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Pound).HasColumnName(@"£").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StaticField).HasColumnName(@"static").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Day).HasColumnName(@"readonly").HasColumnType("int").IsRequired(false);
        }
    }

}
// </auto-generated>