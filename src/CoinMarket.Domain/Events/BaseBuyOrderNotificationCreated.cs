namespace CoinMarket.Domain.Events;

public class BuyOrderNotificationCreated
{
    public int BuyOrderId { get; set; }

    public int BuyOrderNotificationChannelId { get; set; }
}