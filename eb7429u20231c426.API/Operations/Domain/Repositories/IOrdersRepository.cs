using eb7429u20231c426.API.Operations.Domain.Model.Agregates;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Domain.Repositories;

public interface IOrdersRepository : IBaseRepository<Orders>
{
    bool ExistsByLockerIdAsync(int lockerId);
    Task<IEnumerable<Orders>> FindByUserIdAsync(int userId);
}