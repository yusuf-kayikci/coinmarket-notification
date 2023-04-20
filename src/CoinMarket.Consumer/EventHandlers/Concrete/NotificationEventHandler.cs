using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Consumer.EventHandlers.Interface;
using CoinMarket.Consumer.Model;
using CoinMarket.Consumer.Services.Interface;
using CoinMarket.Domain.Entities;
using CoinMarket.Domain.Enums;
using CoinMarket.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Consumer.EventHandlers.Concrete;

public class NotificationEventHandler : INotificationEventHandler
{
    private readonly IServiceProvider _sp;
    private readonly IDateTime _date;
    private readonly INotificationService _notificationService;

    public NotificationEventHandler(IServiceProvider sp, IDateTime date, INotificationService notificationService)
    {
        _sp = sp;
        _date = date;
        _notificationService = notificationService;
    }

    public async Task CreatedAsync(BuyOrderNotificationCreated notificationCreated, CancellationToken cancellationToken = default)
    {
        using (var scope = _sp.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var buyOrder = await context.BuyOrders.FirstOrDefaultAsync(x => x.Status == BuyOrderStatus.Active && x.Id == notificationCreated.BuyOrderId, cancellationToken);
        
            if(buyOrder == null)
            {
                return;
            }

            var notificationChannel = await context.BuyOrderNotificationChannels.FirstOrDefaultAsync(x => x.Id == notificationCreated.BuyOrderNotificationChannelId, cancellationToken);

            if (notificationChannel is null)
            {
                return;
            }
        
            var user = buyOrder.User;
            var date = _date.Now;
            var notification = new Notification
            {
                Message = $"Hello {user.Name} , You created an order to buy coin with amount {buyOrder.Amount} on each {buyOrder.Day}th day of month"
            };

            await _notificationService.SendAsync(notificationChannel.BuyOrderNotificationType, notification, cancellationToken);
        
            context.BuyOrderNotificationLogs.Add(new BuyOrderNotificationLog
            {
                Created = date, 
                BuyOrderId = notificationCreated.BuyOrderId,
                BuyOrderNotificationChannelId = notificationCreated.BuyOrderNotificationChannelId
            });

            await context.SaveChangesAsync(cancellationToken);
            // do something with context
        }
    }
}