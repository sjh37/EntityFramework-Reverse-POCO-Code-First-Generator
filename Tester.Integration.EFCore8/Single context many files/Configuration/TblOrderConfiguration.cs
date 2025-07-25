// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // tblOrders
    public class TblOrderConfiguration : IEntityTypeConfiguration<TblOrder>
    {
        public void Configure(EntityTypeBuilder<TblOrder> builder)
        {
            builder.ToTable("tblOrders", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_tblOrders").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Added).HasColumnName(@"added").HasColumnType("datetime").IsRequired();
        }
    }

}
// </auto-generated>
