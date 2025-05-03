using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Enums;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class OrderRepository : BaseRepository<Orden, int>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext)
        : base(dbContext) { }

    public Orden UpdateOrderStatus(int orderId, Estado status)
    {
        throw new NotImplementedException();
    }
}
