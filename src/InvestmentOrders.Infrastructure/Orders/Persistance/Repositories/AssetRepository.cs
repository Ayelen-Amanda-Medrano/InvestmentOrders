using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class AssetRepository : BaseRepository<Activo, int>, IAssetRepository
{
    public AssetRepository(OrderDbContext dbContext)
        : base(dbContext) { }

    public async Task<Activo?> GetAssetByTickerAsync(string ticker)
    {
        return await DbContext.Activos
            .Include(a => a.TipoActivo)
            .FirstOrDefaultAsync(a => a.Ticker == ticker);
    }
}
