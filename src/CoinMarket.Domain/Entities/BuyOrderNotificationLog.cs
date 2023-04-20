using CoinMarket.Domain.Common;

namespace CoinMarket.Domain.Entities;

public class BuyOrderNotificationLog : BaseAuditableEntity
{
    public int BuyOrderId { get; set; }

    public int BuyOrderNotificationChannelId { get; set; }
}