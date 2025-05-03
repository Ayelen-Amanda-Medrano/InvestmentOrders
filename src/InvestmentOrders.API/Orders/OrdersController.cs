using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Response;
using InvestmentOrders.Application.Orders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentOrders.API.Order;

[ApiController]
[Route("/api/order")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{orderId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int orderId)
    {
        var result = await _orderService.GetOrderByIdAsync(orderId);

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<CreateOrderResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreteOrderRequest request)
    {
        var result = await _orderService.CreateOrderAsync(request);

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{orderId}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] int orderId, [FromBody] UpdateOrderRequest request)
    {
        var result = await _orderService.UpdateOrderAsync(orderId, request);

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderId)
    {
        var result = await _orderService.DeleteOrderAsync(orderId);

        if (result.Success)
            return NoContent();

        return StatusCode((int)result.StatusCode, result);
    }
}