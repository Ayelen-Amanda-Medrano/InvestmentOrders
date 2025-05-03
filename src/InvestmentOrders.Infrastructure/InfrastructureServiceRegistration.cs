using InvestmentOrders.Application.Orders.Repositories.Interfaces;
using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Infrastructure.Orders.Persistance;
using InvestmentOrders.Infrastructure.Orders.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentOrders.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    => services.AddPersistenceServices(connectionString);

    private static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBaseRepository<Orden, int>, BaseRepository<Orden, int>>();

        return services;
    }

}
