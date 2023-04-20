using CoinMarket.Domain.Enums;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Interface;

public interface INotificationPublishOperation
{
    Task PublishAsync(BuyOrderNotificationType type, BuyOrderNotificationCreated buyOrderNotificationCreated, CancellationToken cancellationToken);
}