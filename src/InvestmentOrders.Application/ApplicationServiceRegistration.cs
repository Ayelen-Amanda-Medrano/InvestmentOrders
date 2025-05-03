using InvestmentOrders.Application.Orders.AutoMappers;
using InvestmentOrders.Application.Orders.Services;
using InvestmentOrders.Application.Orders.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentOrders.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IOrderService, OrderService>();

        services.AddAutoMapper(typeof(OrderMapper).Assembly);

        return services;
    }
}
