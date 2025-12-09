using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Operations.Infrastructure.EFC.Configuration.Extentions;

public static class ModelBuilderExtensions
{
    public static void ApplyOperationsConfiguration(this ModelBuilder builder)
    {
        ConfigureOrders(builder);
        ConfigureUsers(builder);
    }
    
    private static void ConfigureOrders(this ModelBuilder builder)
    {
        builder.Entity<Orders>().HasKey(o => o.Id);
        
        builder.Entity<Orders>().HasOne(o => o.Locker)
            .WithOne()
            .HasForeignKey<Orders>(o => o.LockerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Orders>().Property(o => o.LockerId).IsRequired();
        
        builder.Entity<Orders>().HasOne(o => o.User)
            .WithOne()
            .HasForeignKey<Orders>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Orders>().Property(o => o.UserId).IsRequired();
        builder.Entity<Orders>().Property(o => o.PlacedAt).IsRequired();
        builder.Entity<Orders>().Property(o => o.PickedUpAt).IsRequired(false);
        builder.Entity<Orders>().Property(o => o.CreatedDate).IsRequired();
        builder.Entity<Orders>().Property(o => o.UpdatedDate).IsRequired(false);
    }
    
    private static void ConfigureUsers(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.Entity<User>().Property(u => u.StoreId).IsRequired();
    }
}