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
    public async Task<Orders?> Handle(CreateOrdersCommand command) {
        // Buscar pedido activo en el locker (incluyendo chequeo de expiración)
        var activeOrder = await ordersRepository.FindActiveOrderByLockerIdAsync(command.LockerId);
        
        if (activeOrder != null)
        {
            // Verificar si el pedido activo ha expirado (más de 48 horas)
            if (activeOrder.PlacedAt.HasValue && 
                activeOrder.PlacedAt.Value.AddHours(48) < DateTimeOffset.UtcNow)
            {
                // El pedido ha expirado, liberar el locker
                var expiredLocker = await lockerRepository.FindByIdAsync(command.LockerId); // Cambiado a expiredLocker
                if (expiredLocker != null)
                {
                    expiredLocker.UpdateReleased();
                    lockerRepository.Update(expiredLocker); // Usar expiredLocker aquí
                    await unitOfWork.CompleteAsync();
                    // Continuar con la creación del nuevo pedido
                }
            }
            else
            {
                throw new Exception($"Locker with ID {command.LockerId} is already occupied by another active order.");
            }
        }

        // Check if user exists
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception($"User with ID {command.UserId} does not exist.");
        }

        // Check if user has existing orders (await the async method)
        var userOrders = await ordersRepository.FindByUserIdAsync(command.UserId);
        
        // Check if user has any orders that haven't been picked up yet AND haven't expired
        var activeUserOrders = userOrders.Any(o => o.PickedUpAt == null && 
                                                  !(o.PlacedAt.HasValue && 
                                                    o.PlacedAt.Value.AddHours(48) < DateTimeOffset.UtcNow));
        if (activeUserOrders)
        {
            throw new Exception($"User with ID {command.UserId} already has an active order that hasn't been picked up.");
        }
        
        // check if locker exists
        var locker = await lockerRepository.FindByIdAsync(command.LockerId); // Este es el segundo locker
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
        
        // check if order has expired (más de 48 horas)
        if (orders.PlacedAt.HasValue && 
            orders.PlacedAt.Value.AddHours(48) < DateTimeOffset.UtcNow)
        {
            // Orden expirada, liberar locker pero no permitir recogida
            var expiredLocker = await lockerRepository.FindByIdAsync(orders.LockerId); // Cambiado a expiredLocker
            if (expiredLocker != null)
            {
                expiredLocker.UpdateReleased();
                lockerRepository.Update(expiredLocker); // Usar expiredLocker aquí
                await unitOfWork.CompleteAsync();
            }
            throw new Exception($"Order with ID {command.OrderId} has expired (more than 48 hours) and cannot be picked up.");
        }
        
        // check if user exists
        var user = await userRepository.FindByIdAsync(command.UserId);
        // if not, throw error
        if (user == null)
        {
            throw new Exception($"User with ID {command.UserId} does not exist.");
        }
        
        // check if locker exists
        var locker = await lockerRepository.FindByIdAsync(orders.LockerId); // Este es el segundo locker
        
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