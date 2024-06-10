// <auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tester.Integration.EFCore6.Single_context_many_files
{
    // TemporalDepartmentHistory
    public class TemporalDepartmentHistoryConfiguration : IEntityTypeConfiguration<TemporalDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<TemporalDepartmentHistory> builder)
        {
            builder.ToTable("TemporalDepartmentHistory", "dbo");
            builder.HasKey(x => new { x.DeptId, x.DeptName, x.SysStartTime, x.SysEndTime });

            builder.Property(x => x.DeptId).HasColumnName(@"DeptID").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DeptName).HasColumnName(@"DeptName").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50).ValueGeneratedNever();
            builder.Property(x => x.ManagerId).HasColumnName(@"ManagerID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ParentDeptId).HasColumnName(@"ParentDeptID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SysStartTime).HasColumnName(@"SysStartTime").HasColumnType("datetime2").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.SysEndTime).HasColumnName(@"SysEndTime").HasColumnType("datetime2").IsRequired().ValueGeneratedNever();

            builder.HasIndex(x => new { x.SysEndTime, x.SysStartTime }).HasDatabaseName("ix_TemporalDepartmentHistory");
        }
    }

}
// </auto-generated>
