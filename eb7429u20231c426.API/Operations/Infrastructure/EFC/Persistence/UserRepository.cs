using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb7429u20231c426.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace eb7429u20231c426.API.Operations.Infrastructure.EFC.Persistence;

public class UserRepository (AppDbContext context) :  BaseRepository<User>(context), IUserRepository
{
    public bool ExistsByEmailAsync(string email)
    {
        return Context.Set<User>().Any(u => u.Email == email);
    }
}