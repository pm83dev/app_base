
PER CREARE CONTAINER DB (POSTGRE) E MIGRAZIONI EF
# 1. Avvia PostgreSQL
docker run --name dpi-postgres-test -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=DpiDb -p 5432:5432 -d postgres:15-alpine

# 2. Crea la migrazione (se non esiste)
dotnet ef migrations add InitialCreate --project dpi.infrastructure --startup-project dpi.backend --connection "Host=localhost;Database=DpiDb;Username=postgres;Password=postgres"

# 3. Applica la migrazione al database
dotnet ef database update --project dpi.infrastructure --startup-project dpi.backend --connection "Host=localhost;Database=DpiDb;Username=postgres;Password=postgres"

# 4. Verifica le tabelle
docker exec -it dpi-postgres-test psql -U postgres -d DpiDb -c "\dt"

# 5. Avvia l'app
cd dpi.backend
dotnet run

PER VERIFICARE SE LE TABELLE SONO STATE CREATE
docker exec -it dpi-postgres-test psql -U postgres -d DpiDb -c "\dt"

PER VERIFICARE CHE DOCKER SIA IN ESECUZIONE
docker ps | findstr dpi-postgres-test

PER COMANDI IN DB IN CONTAINER
(esempio per inserire campi)
docker exec -it dpi-postgres-test psql -U postgres -d DpiDb -c "INSERT INTO \"Workers\" (\"Id\", \"FirstName\", \"LastName\", \"Email\", \"Department\") VALUES (gen_random_uuid(), 'Mario', 'Rossi', 'mario.rossi@example.com', 'Produzione');"

PER ELIMINARE CONTAINER
docker rm -f dpi-postgres-test



********************************
PER USARE EF NEL PROGETTO:

//Program.cs
// Add dbContext for Entity Framework Core (assuming you have an AppDbContext defined in your project)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//appSettings.json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=AppBaseDb;Username=postgres;Password=postgres"
  }

//AppDbContextFactory.cs:
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


//AppDbContext.cs
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

//appSettings.json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=AppBaseDb;Username=postgres;Password=postgres"
  }
  
//Da lanciare da dentro la cartella repo
dotnet ef migrations add InitialCreate

dotnet ef database update