using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Application.CommandService;

public class OrdersCommandService 
(
    IOrdersRepository ordersRepository,
    IUnitOfWork unitOfWork
    ) : IOrdersCommandService
{
    public async Task<Orders?> Handle(CreateOrdersCommand command)
    {
        var existId = ordersRepository.ExistsByLockerIdAsync(command.LockerId);
        var existUser = ordersRepository.FindByUserIdAsync(command.UserId);
        if (existId || existUser != null)
        {
            throw new Exception("LockerId already exists or UserId already has an order.");
        }
        var orders = new Orders(command);
        try
        {
            await ordersRepository.AddAsync(orders);
            await unitOfWork.CompleteAsync();
            return orders;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}