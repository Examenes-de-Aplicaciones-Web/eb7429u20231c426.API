using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;

namespace eb7429u20231c426.API.Operations.Domain.Services;

public interface IOrdersCommandService
{
    Task<Orders?> Handle(CreateOrdersCommand command);
    
    Task<Orders?> Handle(PickUpOrdersCommand command);
}