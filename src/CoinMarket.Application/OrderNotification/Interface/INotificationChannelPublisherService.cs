using CoinMarket.Domain.Entities;
using CoinMarket.Domain.Events;

namespace CoinMarket.Application.OrderNotification.Interface;

public interface INotificationChannelPublisherService
{
    Task PublishAsync(BuyOrderNotificationCreated notificationChannel, CancellationToken cancellationToken = default);
}