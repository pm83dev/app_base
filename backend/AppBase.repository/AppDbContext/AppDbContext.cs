
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define DbSet properties for your entities here
    // public DbSet<YourEntity> YourEntities { get; set; }
}