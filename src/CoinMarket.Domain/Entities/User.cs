using CoinMarket.Domain.Common;

namespace CoinMarket.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }

    public string Mail { get; set; }
    
    public List<BuyOrder> BuyOrders { get; set; }
}