using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;

namespace eb7429u20231c426.API.Operations.Domain.Model.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    EStore StoreId
    );