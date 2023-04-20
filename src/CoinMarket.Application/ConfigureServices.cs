using System.Reflection;
using CoinMarket.Application.Order.Concrete;
using CoinMarket.Application.Order.Interface;
using CoinMarket.Application.Order.Validations;
using CoinMarket.Application.OrderNotification.Concrete;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Application.User.Concreate;
using CoinMarket.Application.User.Interface;
using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            options.RegisterValidatorsFromAssemblyContaining<CreateBuyOrderValidator>();
        });
        services.AddScoped<IUserService, UserService>()
            .AddScoped<IBuyOrderService, BuyOrderService>()
            .AddScoped<INotificationPublishOperation,NotificationPublishOperation>();
        
        return services;
    }
}
