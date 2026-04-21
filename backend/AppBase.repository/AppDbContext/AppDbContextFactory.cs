using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Try to resolve the absolute path to appsettings.Development.json
        var basePath = Directory.GetCurrentDirectory();
        var apiSettingsPath = Path.Combine(basePath, "..", "AppBase.Api", "appsettings.Development.json");
        string connectionString = null;

        if (File.Exists(apiSettingsPath))
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(basePath, "..", "AppBase.Api")))
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        // Fallback: use a hardcoded connection string if config file is missing or connection string is empty
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = "Host=localhost;Database=AppBaseDb;Username=postgres;Password=postgres";
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}
