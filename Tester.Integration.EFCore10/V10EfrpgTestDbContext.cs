using Microsoft.EntityFrameworkCore;

namespace V10EfrpgTest;

public partial class V10EfrpgTestDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasQueryFilter(p => p.CarMake != string.Empty);
    }
}

