using CoinMarket.Domain.Common;
using CoinMarket.Domain.Enums;

namespace CoinMarket.Domain.Entities;

public class BuyOrderNotificationChannel : BaseAuditableEntity
{
    public BuyOrderNotificationType BuyOrderNotificationType { get; set; }
 
    public int BuyOrderId { get; set; }
    
    public BuyOrder BuyOrder { get; set; }
}