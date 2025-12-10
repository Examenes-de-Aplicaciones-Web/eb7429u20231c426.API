namespace eb7429u20231c426.API.Operations.Interfaces.REST.Resources;

public record OrdersResource(
    int Id,
    int LockerId,
    DateTimeOffset? PlacedAt,
    DateTimeOffset? PickedUpAt,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt,
    UserResource User
    );