namespace CoinMarket.Application.Order.Models;

public class CreateBuyOrderDTO
{
    public double Amount { get; set; }

    public int Day { get; set; }
    
    public int UserId { get; set; }
    
    public List<BuyOrderNotificationChannelDTO> BuyOrderNotifications { get; set; }
}