using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Infrastructure.Persistence;
using CoinMarket.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CoinMarket.Application.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
        });

        builder.ConfigureServices((builder, services) =>
        {
            services.AddApplicationServices();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IApplicationDbContext>(provider =>
            {
                var context =  provider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                return context;
            });
            services.AddTransient<IDateTime, DateTimeService>();
        });
    }
}
