using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public static class OrdersResourceFromEntityAssembler
{
    public static OrdersResource ToResourceFromEntity(Orders entity)
    {
        bool hasExpired = entity.PlacedAt.HasValue && 
                          entity.PlacedAt.Value.AddHours(48) < DateTimeOffset.UtcNow && 
                          entity.PickedUpAt == null;
    
        bool isActive = entity.PickedUpAt == null && !hasExpired;
    
        TimeSpan? timeUntilExpiry = null;
        if (entity.PlacedAt.HasValue && entity.PickedUpAt == null)
        {
            timeUntilExpiry = entity.PlacedAt.Value.AddHours(48) - DateTimeOffset.UtcNow;
            if (timeUntilExpiry.Value.TotalSeconds < 0)
                timeUntilExpiry = TimeSpan.Zero;
        }
    
        return new OrdersResource(
            entity.Id,
            entity.LockerId,
            entity.PlacedAt,
            entity.PickedUpAt, 
            entity.CreatedDate,
            entity.UpdatedDate,
            UserResourceFromEntityAssembler.ToResourceFromEntity(entity.User),
            hasExpired,
            isActive,
            timeUntilExpiry
        );
    }
}