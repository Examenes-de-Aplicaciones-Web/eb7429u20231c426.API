using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Domain.Model.Command;

namespace eb7429u20231c426.API.Assets.Domain.Services;

public interface ILockerCommandService
{
    Task<Lockers?> Handle(CreateLockerCommand command);

}