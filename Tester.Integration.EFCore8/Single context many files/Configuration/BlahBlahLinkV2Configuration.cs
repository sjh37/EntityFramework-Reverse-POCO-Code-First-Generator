// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // BlahBlahLink_v2
    public class BlahBlahLinkV2Configuration : IEntityTypeConfiguration<BlahBlahLinkV2>
    {
        public void Configure(EntityTypeBuilder<BlahBlahLinkV2> builder)
        {
            builder.ToTable("BlahBlahLink_v2", "dbo");
            builder.HasKey(x => new { x.BlahId, x.BlahId2 }).HasName("PK_BlahBlahLinkv2_ro").IsClustered();

            builder.Property(x => x.BlahId).HasColumnName(@"BlahID").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.BlahId2).HasColumnName(@"BlahID2").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Dummy1).HasColumnName(@"dummy1").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Dummy2).HasColumnName(@"dummy2").HasColumnType("int").IsRequired();
            builder.Property(x => x.Hello).HasColumnName(@"hello").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Blah_BlahId).WithMany(b => b.BlahBlahLinkV2_BlahId).HasForeignKey(c => c.BlahId).HasConstraintName("FK_BlahBlahLinkv2_Blah_ro");
            builder.HasOne(a => a.Blah_BlahId2).WithMany(b => b.BlahBlahLinkV2_BlahId2).HasForeignKey(c => c.BlahId2).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BlahBlahLinkv2_Blah_ro2");
        }
    }

}
// </auto-generated>
