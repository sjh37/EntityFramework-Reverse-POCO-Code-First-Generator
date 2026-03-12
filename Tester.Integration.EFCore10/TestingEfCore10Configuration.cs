using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azure10;

public partial class TestingEfCore10Configuration
{
    partial void InitializePartial(EntityTypeBuilder<TestingEfCore10> builder)
    {
        builder.Property(x => x.ShippingAddress).HasColumnName("ShippingAddress").HasColumnType("json").IsRequired();
    }
}