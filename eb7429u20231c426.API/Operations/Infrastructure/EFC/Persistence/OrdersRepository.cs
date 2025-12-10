using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Operations.Infrastructure.EFC.Persistence;

public class OrdersRepository (AppDbContext context) :  BaseRepository<Orders>(context), IOrdersRepository
{
    public bool ExistsByLockerIdAsync(int lockerId)
    {
        return Context.Set<Orders>().Any(orders => orders.LockerId == lockerId);
    }

    public Task<IEnumerable<Orders>> FindByUserIdAsync(int userId)
    {
        var orders = Context.Set<Orders>().Where(orders => orders.UserId == userId);
        return Task.FromResult(orders.AsEnumerable());
    }

    public Task<Orders?> FindByLockerIdAndOrderIdAsync(int lockerId, int orderId)
    {
        var order = Context.Set<Orders>()
            .Include(o => o.User) // Incluir User si es necesario
            .FirstOrDefaultAsync(orders => orders.LockerId == lockerId && orders.Id == orderId);
        return order;
    }

    public new async Task<Orders?> FindByIdAsync(int id)
    {
        return await Context.Set<Orders>()
            .Include(o => o.User) // Incluir User
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}