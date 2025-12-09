namespace eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

public record CreateUserResource(
    string FirstName,
    string LastName,
    string Email,
    int StoreId
    );