using CoinMarket.Application.Order.Enum;

namespace CoinMarket.Application.Order.Models;

public class BuyOrderNotificationChannelDTO
{
    public BuyOrderNotificationType NotificationType { get; set; }
}