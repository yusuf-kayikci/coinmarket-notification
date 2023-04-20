using CoinMarket.Consumer.Model;
using CoinMarket.Consumer.Services.Interface;

namespace CoinMarket.Consumer.Services.Concrete;

public class PushNotifiationStrategy : INotificationSender
{
    public Task SendAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Sms sended to push notification with message {notification.Message}");

        return Task.CompletedTask;
    }
}