using eb7429u20231c426.API.Assets.Infrastructure.EFC.Configuration.Extentions;
using eb7429u20231c426.API.Operations.Infrastructure.EFC.Configuration.Extentions;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     The application's database context, inheriting from Entity Framework Core's DbContext.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     Configures the database context options, adding the created and updated date interceptor.
    /// </summary>
    /// <param name="builder">The option builder to configure.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     Configures the model using the snake_case naming convention.
    /// </summary>
    /// <param name="builder">The model builder to configure.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();
        builder.ApplyOperationsConfiguration();
        builder.ApplyAssetsConfiguration();
    }
}