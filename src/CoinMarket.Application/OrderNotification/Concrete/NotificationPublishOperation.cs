using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Domain.Enums;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Concrete;

public class NotificationPublishOperation : INotificationPublishOperation
{
    private readonly IMessagePublisher _messagePublisher;
    
    public NotificationPublishOperation(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }
    
    public async Task PublishAsync(BuyOrderNotificationType type, BuyOrderNotificationCreated buyOrderNotificationCreated, CancellationToken cancellationToken)
    {
        INotificationPublisher publisher = type switch
        {
            BuyOrderNotificationType.Sms => new SmsPublisherStrategy(_messagePublisher),
            BuyOrderNotificationType.Mail => new MailPublisherStrategy(_messagePublisher),
            BuyOrderNotificationType.Push => new PushNotificationPublisherStrategy(_messagePublisher),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        await publisher.PublishAsync(buyOrderNotificationCreated, cancellationToken);
    }
}