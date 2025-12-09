using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Domain.Model.Queries;
using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Assets.Domain.Services;

namespace eb7429u20231c426.API.Assets.Application.QueryService;

public class LockerQueryService(ILockerRepository lockerRepository) : ILockersQueryService
{
    public async Task<Lockers?> Handle(GetLockerByIdQuery query)
    {
        return await lockerRepository.FindByIdAsync(query.Id);
    }
}