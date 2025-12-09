using eb7429u20231c426.API.Shared.Domain.Repositories;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace eb7429u20231c426.API.Shared.Infrastructure.Interfaces.ASP.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSharedContextServices(this WebApplicationBuilder builder)
    {
        // Profiles Bounded Context Dependency Injection Configuration
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    public static void AddCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
  
    public static void UseRequestAuthorization(this WebApplication app)
    {
        // Si luego configuras autenticación JWT u otra, aquí va:
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
