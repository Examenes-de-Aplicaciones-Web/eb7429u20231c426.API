using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;

namespace eb7429u20231c426.API.Assets.Domain.Model.Command;

public record CreateLockerCommand(
    string LockerCode,
    EStore StoreId
    );