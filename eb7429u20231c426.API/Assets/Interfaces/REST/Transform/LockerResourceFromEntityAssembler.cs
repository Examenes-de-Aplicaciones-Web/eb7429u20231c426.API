using eb7429u20231c426.API.Assets.Domain.Model.Agregates;
using eb7429u20231c426.API.Assets.Interfaces.REST.Resources;

namespace eb7429u20231c426.API.Assets.Interfaces.REST.Transform;

public class LockerResourceFromEntityAssembler
{
    // <summary>
    // Assembles a LockerResource from a Locker entity.
    // </summary>
    // <param name="entity">The Locker entity.</param>
    // <returns>A LockerResource representing the Locker entity.</returns>
    public static LockerResource ToResourceFromEntity(Lockers entity)
    {
        return new LockerResource(
            entity.Id,
            entity.LockerCode,
            entity.IsAvailable,
            (int)entity.StoreId
        );
    }
}
