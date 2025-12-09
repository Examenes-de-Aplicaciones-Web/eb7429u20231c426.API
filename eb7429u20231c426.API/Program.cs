using eb7429u20231c426.API.Assets.Infrastructure.ASP.Configuration.Extensions;
using eb7429u20231c426.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using eb7429u20231c426.API.Shared.Infrastructure.Interfaces.ASP;
using eb7429u20231c426.API.Shared.Infrastructure.Interfaces.ASP.Extensions;
using eb7429u20231c426.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Add Open API Configuration
builder.AddOpenApiDocumentation();

// Add context-specific services
builder.AddSharedContextServices();
builder.AddLockerContextService();

// Mediator Configuration
builder.AddCortexConfigurationServices();

// CORS Configuration
builder.AddCorsPolicy();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();