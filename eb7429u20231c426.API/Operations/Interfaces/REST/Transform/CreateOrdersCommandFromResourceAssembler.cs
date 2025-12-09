using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Operations.Interfaces.REST.Transform;

public static class CreateOrdersCommandFromResourceAssembler
{
    public static CreateOrdersCommand ToCommandFromResource(this CreateOrdersResource resource)
    {
        return new CreateOrdersCommand(resource.LockerId, resource.UserId);
    }

}