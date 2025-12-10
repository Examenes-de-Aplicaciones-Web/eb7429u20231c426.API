using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public static class OrdersResourceFromEntityAssembler
{
    public static OrdersResource ToResourceFromEntity(Orders entity)
    {
        
        return new OrdersResource(
            entity.Id,
            entity.LockerId,
            entity.PlacedAt,
            entity.PickedUpAt, 
            entity.CreatedDate,
            entity.UpdatedDate,
            UserResourceFromEntityAssembler.ToResourceFromEntity(entity.User)
        );
    }
}