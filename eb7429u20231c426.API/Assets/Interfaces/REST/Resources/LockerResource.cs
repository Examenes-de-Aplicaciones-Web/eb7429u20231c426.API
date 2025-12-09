namespace eb7429u20231c426.API.Assets.Interfaces.REST.Resources;

public record LockerResource(
    int Id,
    string LockerCode,
    bool IsAvailable,
    int StoreId
    );