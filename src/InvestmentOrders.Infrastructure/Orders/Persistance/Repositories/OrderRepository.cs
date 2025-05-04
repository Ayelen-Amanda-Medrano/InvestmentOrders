using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class OrderRepository : BaseRepository<Orden, int>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext)
        : base(dbContext) { }
}
