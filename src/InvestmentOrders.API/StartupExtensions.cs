using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using InvestmentOrders.Application;
using InvestmentOrders.Application.Orders;
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

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CreteOrderRequestValidator)));

        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            var applicationXmlFile = "InvestmentOrders.Application.xml";
            var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
            options.IncludeXmlComments(applicationXmlPath);
        });

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