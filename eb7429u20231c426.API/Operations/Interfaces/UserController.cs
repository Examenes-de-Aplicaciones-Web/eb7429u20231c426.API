using eb7429u20231c426.API.Operations.Domain.Model.Queries;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;
using eb7429u20231c426.API.Operations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eb7429u20231c426.API.Operations.Interfaces;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json")]
[SwaggerTag("Operations Management API")]
public class UserController (IUserCommandService userCommandService, IUserQueryService userQueryService) :  ControllerBase
{
    // Endpoint to get a user by its ID
    [HttpGet("{userId:int}")]
    [SwaggerOperation(Summary = "Get Users by ID", Description = "Retrieves a User by its unique identifier.")]
    [SwaggerResponse(200, "user by ID", typeof(UserResource))]
    [SwaggerResponse(404, "user not found")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        if (user is null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }
    // Endpoint to create a new user
    [HttpPost]
    [SwaggerOperation(Summary = "Create User", Description = "Creates a new User")]
    [SwaggerResponse(201, "User created", typeof(UserResource))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserResource createuserResource)
    {
        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(createuserResource);
        var user = await userCommandService.Handle(createUserCommand);
        if (user is null) return BadRequest("Could not create user.");
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return CreatedAtAction(nameof(GetUserById), new { userId = userResource.Id }, userResource);
    }
}