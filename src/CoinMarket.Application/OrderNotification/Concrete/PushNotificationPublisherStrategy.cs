using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Concrete;

public class PushNotificationPublisherStrategy : INotificationPublisher
{
    private readonly IMessagePublisher _publisher;
    
    public PushNotificationPublisherStrategy(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }
    
    
    public Task PublishAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken)
    {
        _publisher.Publish(
            "buyorder-notification-created",
            "push",
            notificationCreated);

        return Task.CompletedTask;
    }
}