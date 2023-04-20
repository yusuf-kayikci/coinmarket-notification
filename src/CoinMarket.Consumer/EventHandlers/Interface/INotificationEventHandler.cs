using CoinMarket.Domain.Events;

namespace CoinMarket.Consumer.EventHandlers.Interface;

public interface INotificationEventHandler
{
    Task CreatedAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken = default);
}