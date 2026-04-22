using backend.repository.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define DbSet properties for your entities here
    // Example:
    // public DbSet<YourEntity> YourEntities { get; set; }
    public DbSet<Data> Data { get; set; }
}