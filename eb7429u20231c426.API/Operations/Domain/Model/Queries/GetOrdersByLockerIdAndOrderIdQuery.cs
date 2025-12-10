namespace eb7429u20231c426.API.Operations.Domain.Model.Queries;

public record GetOrdersByLockerIdAndOrderIdQuery(
    int LockerId,
    int OrderId
    );