// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // ToAlpha
    public class Beta_ToAlphaConfiguration : IEntityTypeConfiguration<Beta_ToAlpha>
    {
        public void Configure(EntityTypeBuilder<Beta_ToAlpha> builder)
        {
            builder.ToTable("ToAlpha", "Beta");
            builder.HasKey(x => x.Id).HasName("PK_beta_ToAlpha").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.AlphaId).HasColumnName(@"AlphaId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Alpha_Workflow).WithMany(b => b.Beta_ToAlphas).HasForeignKey(c => c.AlphaId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("BetaToAlpha_AlphaWorkflow");
        }
    }

}
// </auto-generated>
