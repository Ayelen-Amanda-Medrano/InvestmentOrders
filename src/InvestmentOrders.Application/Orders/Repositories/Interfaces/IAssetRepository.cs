using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;


public interface IAssetRepository : IBaseRepository<Activo, int>
{
    Task<Activo?> GetAssetByNameAsync(string name);
}