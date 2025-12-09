using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public class OrdersResourceFromEntityAssembler
{
    public static OrdersResource ToResourceFromEntity(Orders entity)
    {
        return new OrdersResource(
            entity.Id,
            entity.LockerId,
            UserResourceFromEntityAssembler.ToResourceFromEntity(entity.User)
        );
    }
}