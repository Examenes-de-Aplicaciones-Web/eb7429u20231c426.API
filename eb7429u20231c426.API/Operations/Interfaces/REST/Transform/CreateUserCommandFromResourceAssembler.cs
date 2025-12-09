using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        // Validar que el StoreId sea un valor válido de EStore
        if (!Enum.IsDefined(typeof(EStore), resource.StoreId))
        {
            throw new ArgumentException($"Invalid StoreId value: {resource.StoreId}");
        }
        
        return new CreateUserCommand(resource.FirstName, resource.LastName, resource.Email, (EStore)resource.StoreId);
    }

}