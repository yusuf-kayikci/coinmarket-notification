using CoinMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.User> Users { get; }

    DbSet<BuyOrder> BuyOrders { get; }
    
    DbSet<BuyOrderNotificationChannel> BuyOrderNotificationChannels { get; }
    
    DbSet<BuyOrderNotificationLog> BuyOrderNotificationLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
