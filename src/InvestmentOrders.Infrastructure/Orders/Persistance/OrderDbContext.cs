using InvestmentOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Orders.Persistance;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
        ApplyMigrations();
    }

    private void ApplyMigrations()
    {
        try
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
        }
    }

    public DbSet<Activo> Activos => Set<Activo>();
    public DbSet<EstadoOrden> EstadoOrdenes => Set<EstadoOrden>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<TipoActivo> TiposActivo => Set<TipoActivo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
