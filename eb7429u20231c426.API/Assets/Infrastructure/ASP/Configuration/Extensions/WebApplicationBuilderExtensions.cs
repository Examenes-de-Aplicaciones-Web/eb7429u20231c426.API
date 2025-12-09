using eb7429u20231c426.API.Assets.Application.CommandService;
using eb7429u20231c426.API.Assets.Application.QueryService;
using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Assets.Domain.Services;
using eb7429u20231c426.API.Assets.Infrastructure.EFC.Persistence;

namespace eb7429u20231c426.API.Assets.Infrastructure.ASP.Configuration.Extensions;


public static class WebApplicationBuilderExtensions
{
    public static void AddLockerContextService(this WebApplicationBuilder builder)
    {
        // Locker Bounded Context Dependency Injection Configuration
        builder.Services.AddScoped<ILockerRepository, LockerRepository>();
        builder.Services.AddScoped<ILockerCommandService, LockerCommandService>();
        builder.Services.AddScoped<ILockersQueryService, LockerQueryService>();
    }
}