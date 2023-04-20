using CoinMarket.Consumer.Model;
using CoinMarket.Domain.Enums;

namespace CoinMarket.Consumer.Services.Interface;

public interface INotificationService
{
    Task SendAsync(BuyOrderNotificationType notificationType, Notification notification, CancellationToken cancellationToken = default);
}