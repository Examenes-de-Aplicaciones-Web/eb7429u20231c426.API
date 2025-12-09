using eb7429u20231c426.API.Assets.Domain.Model.Command;
using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;
using eb7429u20231c426.API.Assets.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Assets.Interfaces.REST.Transform;

public static class CreateLockerCommandFromResourceAssembler
{
    public static CreateLockerCommand ToCommandResource(this CreateLockerResource resource)
    {
        // Validar que el StoreId sea un valor válido de EStore
        if (!Enum.IsDefined(typeof(EStore), resource.StoreId))
        {
            throw new ArgumentException($"Invalid StoreId value: {resource.StoreId}");
        }
        
        return new CreateLockerCommand(resource.LockerCode, (EStore)resource.StoreId);
    }
}