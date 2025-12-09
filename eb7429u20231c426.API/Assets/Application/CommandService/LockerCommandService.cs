using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Domain.Model.Command;
using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Assets.Domain.Services;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Assets.Application.CommandService;

public class LockerCommandService
( ILockerRepository lockerRepository,
    IUnitOfWork unitOfWork
    ) : ILockerCommandService
{
    public async Task<Lockers?> Handle(CreateLockerCommand command)
    {
        var exists = lockerRepository.ExistsByLockerCodeAsync(command.LockerCode);
        if (exists)
            throw new Exception("Locker with the same code already exists.");
        var locker = new Lockers(command);
        try
        {
            await lockerRepository.AddAsync(locker);
            await unitOfWork.CompleteAsync();
            return locker;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    
}