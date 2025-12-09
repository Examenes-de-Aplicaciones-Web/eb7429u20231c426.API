using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            (int)entity.StoreId
        );
    }
}