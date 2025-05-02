using InvestmentOrders.Application.Common;
using InvestmentOrders.Application.Orders.Dtos;
using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Application.Orders.Response;
using InvestmentOrders.Application.Orders.Services.Interfaces;

namespace InvestmentOrders.Application.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Result<CreateOrderResponse>> CreateOrderAsync(CreteOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDto>> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDto>> UpdateOrderAsync(UpdateOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteOrderAsync(int id)
    {
        throw new NotImplementedException();
    }
}
