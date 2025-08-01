// <auto-generated>

using Generator.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Multi_context_many_filesBananaDbContext
{
    // ComputedColumns
    public class Stafford_ComputedColumnConfiguration : IEntityTypeConfiguration<Stafford_ComputedColumn>
    {
        public void Configure(EntityTypeBuilder<Stafford_ComputedColumn> builder)
        {
            builder.ToTable("ComputedColumns", "Stafford");
            builder.HasKey(x => x.id).HasName("PK_Stafford_ComputedColumns").IsClustered();

            builder.Property(x => x.id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.MyColumn).HasColumnName(@"MyColumn").HasColumnType("varchar(10)").IsRequired().IsUnicode(false).HasMaxLength(10);
        }
    }

}
// </auto-generated>
