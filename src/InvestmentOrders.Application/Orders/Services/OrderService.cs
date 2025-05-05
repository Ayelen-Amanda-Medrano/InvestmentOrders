using AutoMapper;
using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Application.Orders.Response;
using InvestmentOrders.Application.Orders.Services.Interfaces;
using InvestmentOrders.Domain.Entities;
using System.Net;

namespace InvestmentOrders.Application.Orders.Services;

/// <summary>
/// Service for managing investment orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="orderRepository">Repository for managing orders.</param>
    /// <param name="assetRepository">Repository for managing financial assets.</param>
    /// <param name="mapper">Mapper for transforming entities into DTOs.</param>
    public OrderService(IOrderRepository orderRepository, IAssetRepository assetRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new investment order.
    /// </summary>
    /// <param name="request">The details of the order to create.</param>
    /// <returns>A result containing the ID of the created order.</returns>
    public async Task<Result<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest request)
    {
        var asset = await _assetRepository.GetAssetByTickerAsync(request.Ticker);
        if (asset == null)
            return Result<CreateOrderResponse>.Fail($"El identificador '{request.Ticker}' del activo financiero no existe.", HttpStatusCode.BadRequest);

        if (asset.NeedsUnitPrice() && (request.PrecioUnitario is null || request.PrecioUnitario <= 0))
            return Result<CreateOrderResponse>
                .Fail($"El precio unitario es obligatorio y debe ser mayor a 0 para el activo de tipo '{asset.TipoActivo.Descripcion}'.", HttpStatusCode.BadRequest);

        var newOrder = Orden.CreateOrder(request.CuentaId, asset.Nombre, request.Cantidad, request.Operacion);

        newOrder.SetAsset(asset);
        newOrder.SetPrice(request.PrecioUnitario);

        newOrder.CalculateTotalAmount();

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        return Result<CreateOrderResponse>.Ok(new CreateOrderResponse { OrderId = newOrder.Id }, HttpStatusCode.Created);
    }

    /// <summary>
    /// Retrieves an order by its identifier.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <returns>A result containing the details of the order.</returns>
    public async Task<Result<OrderDto>> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order to update.</param>
    /// <param name="request">The details of the update request.</param>
    /// <returns>A result containing the updated order details.</returns>
    public async Task<Result<OrderDto>> UpdateOrderAsync(int orderId, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        order.UpdateStatus((int)request.Estado);
        await _orderRepository.SaveChangesAsync();

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    /// <summary>
    /// Deletes an existing order.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order to delete.</param>
    /// <returns>A result indicating whether the deletion was successful.</returns>
    public async Task<Result<object>> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order is null)
            return Result<object>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        await _orderRepository.DeleteAsync(order);

        return Result<object>.Ok(null, HttpStatusCode.NoContent);
    }
}