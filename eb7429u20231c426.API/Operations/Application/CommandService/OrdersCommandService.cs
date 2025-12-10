using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Operations.Application.CommandService;

public class OrdersCommandService 
(
    IOrdersRepository ordersRepository,
    IUserRepository userRepository,
    ILockerRepository lockerRepository,
    IUnitOfWork unitOfWork
) : IOrdersCommandService
{
    public async Task<Orders?> Handle(CreateOrdersCommand command)
    {
        // Check if this specific locker is already occupied by an order
        var lockerOccupied = ordersRepository.ExistsByLockerIdAsync(command.LockerId);
        
        // Check if user exists
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
        
        // Check if user has any orders that haven't been picked up yet
        var activeUserOrders = userOrders.Any(o => o.PickedUpAt == null);
        if (activeUserOrders)
        {
            throw new Exception($"User with ID {command.UserId} already has an active order that hasn't been picked up.");
        }
        
        // check if locker exists
        var locker = await lockerRepository.FindByIdAsync(command.LockerId);
        // if not, throw error
        if (locker == null)
            throw new Exception($"Locker with ID {command.LockerId} does not exist.");
        
        // Update locker status to occupied
        locker.UpdateOccupied();
        
        // Create new order
        var orders = new Orders(command);
        
        // Set the User navigation property
        orders.User = user;

        try
        {
            await ordersRepository.AddAsync(orders);
            lockerRepository.Update(locker);
            await unitOfWork.CompleteAsync();
            return orders;
        }
        catch (Exception e)
        {
            // Consider logging the exception here
            return null;
        }
    }

    public async Task<Orders?> Handle(PickUpOrdersCommand command)
    {
        // check if order exists
        var orders = await ordersRepository.FindByIdAsync(command.OrderId);
        // if not, throw error
        if (orders == null)
        {
            throw new Exception($"Order with ID {command.OrderId} does not exist.");
        }

        // check if order has already been picked up
        if (orders.PickedUpAt != null)
        {
            throw new Exception($"Order with ID {command.OrderId} has already been picked up.");
        }
        // check if user exists
        var user = await userRepository.FindByIdAsync(command.UserId);
        // if not, throw error
        if (user == null)
        {
            throw new Exception($"User with ID {command.UserId} does not exist.");
        }
        
        // check if locker exists
        var locker = await lockerRepository.FindByIdAsync(orders.LockerId);
        
        // if not, throw error
        if (locker == null)
            throw new Exception($"Locker not found with id {orders.LockerId}.");
        
        locker.UpdateReleased();
        
        // set picked up at to current time
        orders.PickedUpAt = DateTimeOffset.UtcNow;
        
        try
        {
            ordersRepository.Update(orders);
            lockerRepository.Update(locker);
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