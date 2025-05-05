using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class OrderRepository : BaseRepository<Orden, int>, IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext)
        : base(dbContext) { }

    public async Task<Orden?> GetOrderByIdAsync(int id)
    {
        return await DbContext.Ordenes
            .Include(o => o.Activo)
            .ThenInclude(a => a.TipoActivo)
            .Include(o => o.Estado)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
