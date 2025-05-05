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
/// Servicio para gestionar las órdenes de inversión.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor del servicio de órdenes.
    /// </summary>
    /// <param name="orderRepository">Repositorio de órdenes.</param>
    /// <param name="assetRepository">Repositorio de activos.</param>
    /// <param name="mapper">Mapper para transformar entidades en DTOs.</param>
    public OrderService(IOrderRepository orderRepository, IAssetRepository assetRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Crea una nueva orden de inversión.
    /// </summary>
    /// <param name="request">Datos de la orden a crear.</param>
    /// <returns>El resultado con el identificador de la orden creada.</returns>
    public async Task<Result<CreateOrderResponse>> CreateOrderAsync(CreteOrderRequest request)
    {
        var asset = await _assetRepository.GetAssetByNameAsync(request.NombreActivo);
        if (asset == null)
            return Result<CreateOrderResponse>.Fail($"El activo '{request.NombreActivo}' no existe.", HttpStatusCode.BadRequest);

        var newOrder = Orden.CreateOrder(request.CuentaId, request.NombreActivo, request.Cantidad, request.Operacion);

        newOrder.SetAsset(asset);
        newOrder.SetPrice(request.PrecioUnitario);

        newOrder.CalculateTotalAmount();

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        return Result<CreateOrderResponse>.Ok(new CreateOrderResponse { OrderId = newOrder.Id }, HttpStatusCode.OK);
    }

    /// <summary>
    /// Obtiene una orden por su identificador.
    /// </summary>
    /// <param name="orderId">Identificador de la orden.</param>
    /// <returns>El resultado con los datos de la orden.</returns>
    public async Task<Result<OrderDto>> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    /// <summary>
    /// Actualiza el estado de una orden existente.
    /// </summary>
    /// <param name="orderId">Identificador de la orden a actualizar.</param>
    /// <param name="request">Datos para actualizar la orden.</param>
    /// <returns>El resultado con los datos de la orden actualizada.</returns>
    public async Task<Result<OrderDto>> UpdateOrderAsync(int orderId, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        order.UpdateStatus((int)request.Estado);
        await _orderRepository.SaveChangesAsync();

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    /// <summary>
    /// Elimina una orden existente.
    /// </summary>
    /// <param name="orderId">Identificador de la orden a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    public async Task<Result<object>> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<object>.Fail($"La orden con ID '{orderId}' no fue encontrada.", HttpStatusCode.NotFound);

        await _orderRepository.DeleteAsync(order);

        return Result<object>.Ok(null, HttpStatusCode.NoContent);
    }
}