using Microsoft.EntityFrameworkCore;

namespace V9EfrpgTest;

public partial class V9EfrpgTestDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasQueryFilter(p => p.CarMake != string.Empty);
    }
}