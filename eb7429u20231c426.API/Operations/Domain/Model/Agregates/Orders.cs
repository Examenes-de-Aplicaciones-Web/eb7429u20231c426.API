using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;
using eb7429u20231c426.API.Operations.Domain.Model.Entities;

namespace eb7429u20231c426.API.Operations.Domain.Model.Agregates;

public partial class Orders
{
    public int Id { get; set; }
    public int LockerId { get; private set; }
    public int UserId { get; private set; }
    public DateTimeOffset? PlacedAt { get; private set; }
    public DateTimeOffset? PickedUpAt { get; set; }

    public User User { get; internal set; }
    
    public Lockers Locker { get; set; }
    public Orders(int lockerId, int userId) {
        LockerId = lockerId;
        UserId = userId;
        PlacedAt = DateTimeOffset.UtcNow;
        PickedUpAt = default;
    }
    
    public Orders(CreateOrdersCommand command)
        : this(command.LockerId, command.UserId)
    {
    }
}