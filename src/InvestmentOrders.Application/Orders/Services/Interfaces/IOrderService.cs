using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Response;

namespace InvestmentOrders.Application.Orders.Services.Interfaces;

public interface IOrderService
{
    Task<Result<OrderDto>> GetOrderByIdAsync(int id);
    Task<Result<CreateOrderResponse>> CreateOrderAsync(CreteOrderRequest request);
    Task<Result<OrderDto>> UpdateOrderAsync(UpdateOrderRequest request);
    Task<Result<bool>> DeleteOrderAsync(int id);
}
