using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Interface;

public interface INotificationPublisher
{
    Task PublishAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken);
}