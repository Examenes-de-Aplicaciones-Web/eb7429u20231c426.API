using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Application.CommandService;

public class OrdersCommandService 
(
    IOrdersRepository ordersRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : IOrdersCommandService
{
    public async Task<Orders?> Handle(CreateOrdersCommand command)
    {
        // Check if this specific locker is already occupied by an order
        var lockerOccupied = ordersRepository.ExistsByLockerIdAsync(command.LockerId);
        
        // check if user exists
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception($"User with ID {command.UserId} does not exist.");
        }
        
        // Check if user has existing orders (await the async method)
        var userOrders = await ordersRepository.FindByUserIdAsync(command.UserId);
        
        // If locker is already occupied, throw error
        if (lockerOccupied)
        {
            throw new Exception($"Locker with ID {command.LockerId} is already occupied by another order.");
        }
        
        // Optional: If you want to limit users to only one active order at a time
        // Check if user has any orders that haven't been picked up yet
        var activeUserOrders = userOrders.Any(o => o.PickedUpAt == null);
        if (activeUserOrders)
        {
            throw new Exception($"User with ID {command.UserId} already has an active order that hasn't been picked up.");
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
            // Consider logging the exception here
            return null;
        }
    }
}