using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Response;
using InvestmentOrders.Application.Orders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentOrders.API.Order;

/// <summary>
/// Controlador para gestionar las órdenes de inversión.
/// </summary>
[ApiController]
[Route("/api/order")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor del controlador de órdenes.
    /// </summary>
    /// <param name="orderService">Servicio para gestionar las órdenes.</param>
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Obtiene una orden por su identificador.
    /// </summary>
    /// <param name="orderId">Identificador de la orden.</param>
    /// <returns>La orden solicitada.</returns>
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

    /// <summary>
    /// Crea una nueva orden.
    /// </summary>
    /// <param name="request">Datos de la orden a crear.</param>
    /// <returns>La respuesta con el identificador de la orden creada.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<CreateOrderResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
    {
        var result = await _orderService.CreateOrderAsync(request);

        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Actualiza una orden existente.
    /// </summary>
    /// <param name="orderId">Identificador de la orden a actualizar.</param>
    /// <param name="request">Datos para actualizar la orden.</param>
    /// <returns>La orden actualizada.</returns>
    [HttpPut("{orderId}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] int orderId, [FromQuery] UpdateOrderRequest request)
    {
        var result = await _orderService.UpdateOrderAsync(orderId, request);

        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Elimina una orden existente.
    /// </summary>
    /// <param name="orderId">Identificador de la orden a eliminar.</param>
    /// <returns>Respuesta sin contenido si la eliminación fue exitosa.</returns>
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