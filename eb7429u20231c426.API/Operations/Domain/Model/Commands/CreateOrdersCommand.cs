namespace eb7429u20231c426.API.Operations.Domain.Model.Commands;

public record CreateOrdersCommand(
    int LockerId,
    int UserId
    );