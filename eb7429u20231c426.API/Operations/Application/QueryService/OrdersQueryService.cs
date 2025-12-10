using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Queries;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;

namespace eb7429u20231c426.API.Operations.Application.QueryService;

public class OrdersQueryService(IOrdersRepository ordersRepository) : IOrdersQueryService
{
    public async Task<Orders?> Handle(GetOrdersByLockerIdAndOrderIdQuery query)
    {
        var orders = await ordersRepository.FindByLockerIdAndOrderIdAsync(query.LockerId, query.OrderId);
        return orders;
    }
}