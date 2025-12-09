using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Application.CommandService;

public class UsersCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : IUserCommandService
{
    public async Task<User?> Handle(CreateUserCommand command)
    {
        var existEmail = userRepository.ExistsByEmailAsync(command.Email);
        if (existEmail)
        {
            throw new Exception("Email already exists");
        }
        var user = new User(command);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        } catch (Exception e)
        {
            return null;
        }
    }
}