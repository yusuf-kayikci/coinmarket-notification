using CoinMarket.Application.Common.Interfaces;

namespace CoinMarket.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
