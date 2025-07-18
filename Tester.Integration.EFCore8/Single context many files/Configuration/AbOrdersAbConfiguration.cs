// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // AB_OrdersAB_
    public class AbOrdersAbConfiguration : IEntityTypeConfiguration<AbOrdersAb>
    {
        public void Configure(EntityTypeBuilder<AbOrdersAb> builder)
        {
            builder.ToTable("AB_OrdersAB_", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_AB_OrdersAB").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Added).HasColumnName(@"added").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>
