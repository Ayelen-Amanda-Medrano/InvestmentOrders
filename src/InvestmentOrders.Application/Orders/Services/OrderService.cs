using AutoMapper;
using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Application.Orders.Response;
using InvestmentOrders.Application.Orders.Services.Interfaces;
using InvestmentOrders.Domain.Entities;
using System.Net;

namespace InvestmentOrders.Application.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;
    public OrderService(IOrderRepository orderRepository, IAssetRepository assetRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _assetRepository = assetRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateOrderResponse>> CreateOrderAsync(CreteOrderRequest request)
    {
        var asset = await _assetRepository.GetAssetByNameAsync(request.NombreActivo);
        if (asset == null)
            return Result<CreateOrderResponse>.Fail($"The asset '{request.NombreActivo}' does not exist.", HttpStatusCode.BadRequest);

        var newOrder = Orden.CreateOrder(request.CuentaId, request.NombreActivo, request.Cantidad, request.Operacion);

        newOrder.SetAsset(asset);
        newOrder.SetPrice(request.PrecioUnitario);

        newOrder.CalculateTotalAmount();

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();

        return Result<CreateOrderResponse>.Ok(new CreateOrderResponse { OrderId = newOrder.Id }, HttpStatusCode.OK);
    }

    public async Task<Result<OrderDto>> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"Order with ID '{orderId}' was not found.", HttpStatusCode.NotFound);

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    public async Task<Result<OrderDto>> UpdateOrderAsync(int orderId, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<OrderDto>.Fail($"Order with ID '{orderId}' was not found.", HttpStatusCode.NotFound);

        order.UpdateStatus((int)request.Estado);
        await _orderRepository.SaveChangesAsync();

        var orderDto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Ok(orderDto, HttpStatusCode.OK);
    }

    public async Task<Result<object>> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return Result<object>.Fail($"Order with ID '{orderId}' was not found.", HttpStatusCode.NotFound);

        await _orderRepository.DeleteAsync(order);

        return Result<object>.Ok(null, HttpStatusCode.NoContent);
    }
    
}
