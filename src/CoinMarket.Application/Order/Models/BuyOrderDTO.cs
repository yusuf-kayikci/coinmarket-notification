using CoinMarket.Application.Order.Enum;

namespace CoinMarket.Application.Order.Models;

public class BuyOrderDTO
{
    public int Id { get; set; }
    
    public int UserId { get; set; }

    public double Amount { get; set; }
    
    public int Day { get; set; }

    public BuyOrderStatus Status { get; set; }
}