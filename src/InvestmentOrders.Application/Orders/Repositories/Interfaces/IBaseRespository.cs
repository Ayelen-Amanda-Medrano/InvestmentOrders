namespace InvestmentOrders.Application.Orders.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TEntityId>
    where TEntity : class
    where TEntityId : struct
{
    Task<TEntity?> GetByIdAsync(TEntityId id);

    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);

    Task SaveChangesAsync();

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
