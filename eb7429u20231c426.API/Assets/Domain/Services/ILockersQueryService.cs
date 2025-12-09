using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Domain.Model.Queries;

namespace eb7429u20231c426.API.Assets.Domain.Services;

public interface ILockersQueryService
{
    Task<Lockers?> Handle(GetLockerByIdQuery query);

}