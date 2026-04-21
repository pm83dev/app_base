// Program.cs - Entry point for the ASP.NET Core Web API application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container, including controllers, Swagger for API documentation, and CORS policy for the Angular frontend
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dbContext for Entity Framework Core (assuming you have an AppDbContext defined in your project)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS to allow requests from the Angular frontend running on http://localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline only in development environment to use Swagger for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Serve static files from the "wwwroot" directory and enable default file mapping (e.g., index.html)
app.UseDefaultFiles();
app.UseStaticFiles();

// Enable CORS for the defined policy
app.UseCors("frontend");

// Map controller routes and fallback to index.html for client-side routing
app.MapControllers();
app.MapFallbackToFile("index.html");

// Run the application
app.Run();