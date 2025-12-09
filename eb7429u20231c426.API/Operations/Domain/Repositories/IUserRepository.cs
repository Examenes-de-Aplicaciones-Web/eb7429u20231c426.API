using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    bool ExistsByEmailAsync(string email);
    
}