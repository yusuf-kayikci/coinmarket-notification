using CoinMarket.Consumer.Model;
using CoinMarket.Consumer.Services.Interface;
using CoinMarket.Domain.Enums;

namespace CoinMarket.Consumer.Services.Concrete;

public class NotificationService : INotificationService
{
    public async Task SendAsync(BuyOrderNotificationType notificationType, Notification notification, CancellationToken cancellationToken = default)
    {
        INotificationSender notificationSender = notificationType switch
        {
            BuyOrderNotificationType.Sms => new SmsNotificationStrategy(),
            BuyOrderNotificationType.Mail => new MailNotificationStrategy(),
            BuyOrderNotificationType.Push => new PushNotifiationStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
        };

        await notificationSender.SendAsync(notification, cancellationToken);
    }
}