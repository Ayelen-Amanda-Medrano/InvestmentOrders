using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;

public class BaseRepository<TEntity, TEntityId> : IBaseRepository<TEntity, TEntityId>
 where TEntity : class
 where TEntityId : struct
{
    public BaseRepository(OrderDbContext dbContext)
   => DbContext = dbContext;

    protected OrderDbContext DbContext { get; }

    public virtual async Task<TEntity?> GetByIdAsync(TEntityId id)
        => await DbContext.Set<TEntity>().FindAsync(id);


    public virtual async Task AddAsync(TEntity entity)
        => await DbContext.Set<TEntity>().AddAsync(entity);

    public async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();

    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbContext.ChangeTracker.Clear();
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }
}
