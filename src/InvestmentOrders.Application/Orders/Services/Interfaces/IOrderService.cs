using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Response;

namespace InvestmentOrders.Application.Orders.Services.Interfaces;

public interface IOrderService
{
    Task<Result<OrderDto>> GetOrderByIdAsync(int orderId);
    Task<Result<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest request);
    Task<Result<OrderDto>> UpdateOrderAsync(int orderId, UpdateOrderRequest request);
    Task<Result<object>> DeleteOrderAsync(int orderId);
}
