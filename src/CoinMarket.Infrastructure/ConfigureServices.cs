using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Infrastructure.Persistence;
using CoinMarket.Infrastructure.Services;
using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddSingleton(x => new ConnectionFactory{ HostName = "rabbitmq" , DispatchConsumersAsync = true});
        services.AddScoped<IApplicationDbContext>(provider =>
        {
            var context =  provider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            return context;
        });
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
