using eb7429u20231c426.API.Operations.Application.CommandService;
using eb7429u20231c426.API.Operations.Application.QueryService;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Operations.Infrastructure.EFC.Persistence;

namespace eb7429u20231c426.API.Operations.Infrastructure.ASP.Configuration.Extentions;

public static class WebApplicationBuilderExtensions
{
    public static void AddOperationsContextService(this WebApplicationBuilder builder)
    {
        // Operations Bounded Context Dependency Injection Configuration
        // Orders
        builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
        builder.Services.AddScoped<IOrdersCommandService, OrdersCommandService>();
        builder.Services.AddScoped<IOrdersQueryService, OrdersQueryService>();
       // Users
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserCommandService, UsersCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
    }
}