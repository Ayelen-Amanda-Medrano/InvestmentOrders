using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Enums;

namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;

public interface IOrderRepository : IBaseRepository<Orden, int>
{
    Orden UpdateOrderStatus(int orderId, Estado status);
}
