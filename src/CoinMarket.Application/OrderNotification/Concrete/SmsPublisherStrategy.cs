using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Concrete;

public class SmsPublisherStrategy : INotificationPublisher
{
    private readonly IMessagePublisher _publisher;
    
    public SmsPublisherStrategy(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }
    
    
    public Task PublishAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken)
    {
        _publisher.Publish(
            "buyorder-notification-created",
            "sms",
            notificationCreated);

        return Task.CompletedTask;
    }
}