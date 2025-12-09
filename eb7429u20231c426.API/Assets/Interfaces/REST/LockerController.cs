using eb7429u20231c426.API.Assets.Domain.Model.Queries;
using eb7429u20231c426.API.Assets.Domain.Services;
using eb7429u20231c426.API.Assets.Interfaces.REST.Resources;
using eb7429u20231c426.API.Assets.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eb7429u20231c426.API.Assets.Interfaces.REST;

[ApiController]
[Route("api/v1/lockers")]
[Produces("application/json")]
[SwaggerTag("Locker Management API")]
public class LockerController(ILockerCommandService lockerCommandService, ILockersQueryService lockersQueryService) :  ControllerBase
{
    // Endpoint to get a locker by its ID
    [HttpGet("{lockerId:int}")]
    [SwaggerOperation(Summary = "Get Locker by ID", Description = "Retrieves a locker by its unique identifier.")]
    [SwaggerResponse(200, "Locker by ID", typeof(LockerResource))]
    [SwaggerResponse(404, "Locker not found")]
    public async Task<IActionResult> GetLockerById([FromRoute] int lockerId)
    {
        var locker = await lockersQueryService.Handle(new GetLockerByIdQuery(lockerId));
        if (locker is null) return NotFound();
        var lockerResource = LockerResourceFromEntityAssembler.ToResourceFromEntity(locker);
        return Ok(lockerResource);
    }
    // Endpoint to create a new locker
    [HttpPost]
    [SwaggerOperation(Summary = "Create Locker", Description = "Creates a new locker with the provided details.")]
    [SwaggerResponse(201, "Locker created", typeof(LockerResource))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<IActionResult> CreateLocker([FromBody] CreateLockerResource createLockerResource)
    {
        var createLockerCommand = CreateLockerCommandFromResourceAssembler.ToCommandResource(createLockerResource);
        var locker = await lockerCommandService.Handle(createLockerCommand);
        if (locker is null) return BadRequest("Could not create locker.");
        var lockerResource = LockerResourceFromEntityAssembler.ToResourceFromEntity(locker);
        return CreatedAtAction(nameof(GetLockerById), new { lockerId = lockerResource.Id }, lockerResource);
    }
}