using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Assets.Infrastructure.EFC.Configuration.Extentions;

public static class ModelBuilderExtensions
{
    public static void ApplyAssetsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Lockers>().HasKey(x => x.Id);
        builder.Entity<Lockers>().Property(x => x.LockerCode).IsRequired();
        builder.Entity<Lockers>().Property(x => x.isAvailable).IsRequired();
        builder.Entity<Lockers>().Property(x => x.StoreId).IsRequired();
    }
}