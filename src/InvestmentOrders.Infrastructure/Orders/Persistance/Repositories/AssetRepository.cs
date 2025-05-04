using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class AssetRepository : BaseRepository<Activo, int>, IAssetRepository
{
    public AssetRepository(OrderDbContext dbContext)
        : base(dbContext) { }

    public async Task<Activo?> GetAssetByNameAsync(string name)
    {
        return await DbContext.Activos.SingleOrDefaultAsync(asset => asset.Nombre == name);
    }
}
