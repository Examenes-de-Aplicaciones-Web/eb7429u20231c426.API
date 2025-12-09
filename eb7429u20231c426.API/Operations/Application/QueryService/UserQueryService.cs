using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Operations.Domain.Model.Queries;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;

namespace eb7429u20231c426.API.Operations.Application.QueryService;

public class UserQueryService (IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }
}