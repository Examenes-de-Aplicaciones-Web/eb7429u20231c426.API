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
    
    // An order is considered expired if it has been more than 48 hours since it was placed and it has not been picked up.
    public bool HasExpired => PlacedAt.HasValue && 
                              PlacedAt.Value.AddHours(48) < DateTimeOffset.UtcNow && 
                              PickedUpAt == null;
    
    // An order is active if it has been placed but not yet picked up and has not expired.
    public bool IsActive => PickedUpAt == null && !HasExpired;
    
    // Time since the order was placed
    public TimeSpan? TimeSincePlaced => PlacedAt.HasValue ? 
        DateTimeOffset.UtcNow - PlacedAt.Value : null;
    
    // Time until the order expires
    public TimeSpan? TimeUntilExpiry => PlacedAt.HasValue && PickedUpAt == null ? 
        PlacedAt.Value.AddHours(48) - DateTimeOffset.UtcNow : null;
    
    // Is the order expired
    public bool IsExpired => HasExpired;
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
    
    // Method to check if the order is expired
    public bool CheckIfExpired()
    {
        return HasExpired;
    }
}