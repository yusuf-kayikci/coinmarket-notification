using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Domain.Entities;
using CoinMarket.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
        : base(options)
    {
        _dateTime = dateTime;
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<BuyOrder> BuyOrders => Set<BuyOrder>();

    public DbSet<BuyOrderNotificationChannel> BuyOrderNotificationChannels => Set<BuyOrderNotificationChannel>();

    public DbSet<BuyOrderNotificationLog> BuyOrderNotificationLogs => Set<BuyOrderNotificationLog>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(x => x.BuyOrders);

        builder.Entity<BuyOrder>()
            .HasOne<User>(x => x.User);

        builder.Entity<BuyOrderNotificationChannel>()
            .HasOne(x => x.BuyOrder);
        
        

        builder.Entity<User>().HasData(new User
        {
            Id = 1,
            Created = _dateTime.Now,
            Mail = "user1@gmail.com",
            Name = "user1",
            CreatedBy = "user1",
            LastModified = _dateTime.Now,
        });
        
        builder.Entity<User>().HasData(new User
        {
            Id = 2,
            Created = _dateTime.Now,
            Mail = "user2@gmail.com",
            Name = "user2",
            CreatedBy = "user2",
            LastModified = _dateTime.Now,
        });

        builder.Entity<BuyOrder>().HasData(new BuyOrder
        {
            Id = 1,
            Amount = 300,
            Day = 20,
            Status = BuyOrderStatus.Active,
            UserId = 1
        });
        
        builder.Entity<BuyOrderNotificationChannel>().HasData(new List<BuyOrderNotificationChannel>
        {
            new BuyOrderNotificationChannel { Id = 1, BuyOrderId = 1, BuyOrderNotificationType = BuyOrderNotificationType.Sms },
            new BuyOrderNotificationChannel { Id = 2, BuyOrderId = 1, BuyOrderNotificationType = BuyOrderNotificationType.Mail }
        });

        // db constraint for each user could not have more than 1 active buy order
        builder.Entity<BuyOrder>()
            .HasIndex(x => new { x.UserId, x.Status, x.Deleted })
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("CoinMarket")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
