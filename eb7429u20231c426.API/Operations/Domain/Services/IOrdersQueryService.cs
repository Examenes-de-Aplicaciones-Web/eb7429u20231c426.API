using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Queries;

namespace eb7429u20231c426.API.Operations.Domain.Services;

public interface IOrdersQueryService
{
    Task<Orders?> Handle(GetOrdersByIdQuery query);
}