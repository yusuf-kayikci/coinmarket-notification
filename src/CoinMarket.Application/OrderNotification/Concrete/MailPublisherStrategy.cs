using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Concrete;

public class MailPublisherStrategy : INotificationPublisher
{
    private readonly IMessagePublisher _publisher;
    
    public MailPublisherStrategy(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }
    
    public Task PublishAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken)
    {
        _publisher.Publish(
            "buyorder-notification-created",
            "mail",
            notificationCreated);

        return Task.CompletedTask;
    }
}