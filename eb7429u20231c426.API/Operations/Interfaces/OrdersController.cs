using eb7429u20231c426.API.Operations.Domain.Model.Queries;
using eb7429u20231c426.API.Operations.Domain.Services;
using eb7429u20231c426.API.Operations.Interfaces.REST.Resources;
using eb7429u20231c426.API.Operations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eb7429u20231c426.API.Operations.Interfaces;

[ApiController]
[Route("api/v1/")]
[Produces("application/json")]
[SwaggerTag("Operations Management API")]
public class OrdersController (IOrdersCommandService ordersCommandService, IOrdersQueryService ordersQueryService) :  ControllerBase
{
    // Endpoint to get a order by its ID
    [HttpGet("lockers/{lockerId:int}/orders/{orderId:int}")]
    [SwaggerOperation(Summary = "Get Order by ID", Description = "Retrieves a Order by its unique identifier.")]
    [SwaggerResponse(200, "order by ID", typeof(OrdersResource))]
    [SwaggerResponse(404, "order not found")]
    public async Task<IActionResult> GetOrderById([FromRoute] int orderId, [FromRoute] int lockerId)
    {
        var getOrderByIdQuery = new GetOrdersByLockerIdAndOrderIdQuery(lockerId, orderId);
        var orders = await ordersQueryService.Handle(getOrderByIdQuery);
        if (orders is null) return NotFound();
        var ordersResource = OrdersResourceFromEntityAssembler.ToResourceFromEntity(orders);
        return Ok(ordersResource);
    }
    // Endpoint to create a new order
    [HttpPost]
    [Route("orders")]
    [SwaggerOperation(Summary = "Create Orders", Description = "Creates a new Orders")]
    [SwaggerResponse(201, "Orders created", typeof(OrdersResource))]
    [SwaggerResponse(400, "Invalid input")]
    public async Task<IActionResult> CreateLocker([FromBody] CreateOrdersResource createOrdersResource)
    {
        var createOrdersCommand = CreateOrdersCommandFromResourceAssembler.ToCommandFromResource(createOrdersResource);
        var orders = await ordersCommandService.Handle(createOrdersCommand);
        if (orders is null) return BadRequest("Could not create locker.");
        var ordersResource = OrdersResourceFromEntityAssembler.ToResourceFromEntity(orders);
        return CreatedAtAction(nameof(GetOrderById), new { lockerId  = ordersResource.LockerId, orderId = ordersResource.Id}, ordersResource);
    }
}