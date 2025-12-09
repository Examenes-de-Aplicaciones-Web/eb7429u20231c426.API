using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Model.Entities;

namespace eb7429u20231c426.API.Operations.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);
}