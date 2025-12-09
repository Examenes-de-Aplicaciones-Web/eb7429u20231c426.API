using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb7429u20231c426.API.Assets.Infrastructure.EFC.Persistence;

public class LockerRepository (AppDbContext context) : BaseRepository<Lockers>(context), ILockerRepository
{
    public bool ExistsByLockerCodeAsync(string lockerCode)
    {
        return Context.Set<Lockers>().Any(lockers => lockers.LockerCode.Equals(lockerCode));
    }

    public new async Task<Lockers?> FindByIdAsync(int id)
    {
        return await Context.Set<Lockers>()
            .FirstOrDefaultAsync(lockers => lockers.Id == id);
    }
}