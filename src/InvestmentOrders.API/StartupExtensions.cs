using System.Text.Json.Serialization;
using InvestmentOrders.Application;
using InvestmentOrders.Infrastructure;

namespace InvestmentOrders.API;

public static class StartupExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("OrdenesBd");

        builder.Services
            .AddApplicationServices()
            .AddInfrastructureServices(connectionString!);

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}