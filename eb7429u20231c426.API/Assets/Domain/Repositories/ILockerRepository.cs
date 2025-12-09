using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Assets.Domain.Repositories;

public interface ILockerRepository : IBaseRepository<Lockers>
{
    // <summary>
    // Check if a locker exist by its code
    // </summary>
    bool ExistsByLockerCodeAsync(string lockerCode);
}