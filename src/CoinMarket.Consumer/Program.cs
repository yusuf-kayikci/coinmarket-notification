using CoinMarket.Consumer.Consumers;
using CoinMarket.Consumer.EventHandlers.Concrete;
using CoinMarket.Consumer.EventHandlers.Interface;
using CoinMarket.Consumer.Services.Concrete;
using CoinMarket.Consumer.Services.Interface;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddInfrastructureServices();
        services.AddHostedService<SmsNotificationConsumer>();
        services.AddHostedService<MailNotificationConsumer>();
        services.AddHostedService<PushNotificationConsumer>();
        services.AddHostedService<RetrySmsNotificationConsumer>();
        services.AddHostedService<RetryMailNotificationConsumer>();
        services.AddHostedService<RetryPushNotificationConsumer>();
        services.AddScoped<INotificationEventHandler, NotificationEventHandler>();
        services.AddScoped<INotificationService, NotificationService>();
    })
    .Build();

await host.RunAsync();