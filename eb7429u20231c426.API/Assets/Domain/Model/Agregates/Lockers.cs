using eb7429u20231c426.API.Assets.Domain.Model.Command;
using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;

namespace eb7429u20231c426.API.Assets.Domain.Model.Agregates;

public partial class Lockers
{
    public int Id { get; set; }

    public string LockerCode { get; set; }

    public bool isAvailable { get; set; }

    public EStore StoreId { get; set; }

    protected Lockers()
    {
        LockerCode = string.Empty;
        isAvailable = true;
        StoreId = default;
        
    }

    public Lockers(string lockerCode, EStore storeId)
    {
        LockerCode = lockerCode;
        StoreId = storeId;
    }
    
    public Lockers(CreateLockerCommand command)
        : this(command.LockerCode, command.StoreId )
    {
    }
}