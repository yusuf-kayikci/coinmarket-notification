using CoinMarket.Consumer.Model;

namespace CoinMarket.Consumer.Services.Interface;

public interface INotificationSender
{
    Task SendAsync(Notification notification, CancellationToken cancellationToken = default);
}