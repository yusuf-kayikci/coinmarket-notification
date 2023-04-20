using CoinMarket.Domain.Common;
using CoinMarket.Domain.Enums;

namespace CoinMarket.Domain.Entities;

public class BuyOrder : BaseAuditableEntity
{
    public double Amount { get; set; }

    public int Day { get; set; }
    
    public int UserId { get; set; }

    public BuyOrderStatus Status { get; set; }

    public List<BuyOrderNotificationChannel> BuyOrderNotificationChannels { get; set; }

    public User User { get; set; }
}