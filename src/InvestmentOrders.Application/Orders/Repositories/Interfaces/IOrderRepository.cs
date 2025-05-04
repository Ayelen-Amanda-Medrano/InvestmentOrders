using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;

public interface IOrderRepository : IBaseRepository<Orden, int>
{
}
