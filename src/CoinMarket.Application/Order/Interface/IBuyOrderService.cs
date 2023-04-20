using System.Linq.Expressions;
using CoinMarket.Application.Order.Models;
using CoinMarket.Domain.Entities;

namespace CoinMarket.Application.Order.Interface;

public interface IBuyOrderService
{
    Task<BuyOrderDTO> GetBuyOrderAsync(Expression<Func<BuyOrder, bool>> expression, CancellationToken cancellationToken);
    
    Task<IEnumerable<BuyOrderDTO>> GetBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate, CancellationToken cancellationToken);

    Task<int> CreateBuyOrderAsync(CreateBuyOrderDTO buyOrderDto, CancellationToken cancellationToken = default);
    
    Task<BuyOrderDTO> UpdateBuyOrderAsync(BuyOrderDTO buyOrder, CancellationToken cancellationToken);

    Task<IEnumerable<BuyOrderNotificationChannelDTO>> GetBuyOrderNotificationChannelsAsync(int buyOrderId, CancellationToken cancellationToken = default);
}