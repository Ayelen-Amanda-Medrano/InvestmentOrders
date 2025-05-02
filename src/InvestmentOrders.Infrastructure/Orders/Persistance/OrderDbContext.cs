using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    public DbSet<Activo> Activo => Set<Activo>();
    public DbSet<EstadoOrden> EstadoOrden => Set<EstadoOrden>();
    public DbSet<Orden> Orden => Set<Orden>();
    public DbSet<TipoActivo> TipoActivo => Set<TipoActivo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
