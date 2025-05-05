using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;

/// <summary>
/// Interface for the repository of financial assets.
/// </summary>
/// <remarks>
/// Provides methods to perform CRUD operations and specific queries on the <see cref="Activo"/> entity.
/// </remarks>
public interface IAssetRepository : IBaseRepository<Activo, int>
{
    /// <summary>
    /// Retrieves a financial asset by its name.
    /// </summary>
    /// <param name="name">The name of the financial asset.</param>
    /// <returns>The corresponding financial asset, or null if not found.</returns>
    Task<Activo?> GetAssetByTickerAsync(string ticker);
}