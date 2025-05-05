using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;

/// <summary>
/// Interface for the repository of investment orders.
/// </summary>
/// <remarks>
/// Provides methods to perform CRUD operations and specific queries on the <see cref="Orden"/> entity.
/// </remarks>
public interface IOrderRepository : IBaseRepository<Orden, int>
{
    /// <summary>
    /// Retrieves an investment order by its identifier.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <returns>The corresponding order, or null if not found.</returns>
    Task<Orden?> GetOrderByIdAsync(int orderId);
}