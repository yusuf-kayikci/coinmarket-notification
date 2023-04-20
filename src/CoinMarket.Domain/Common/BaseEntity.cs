using System.ComponentModel.DataAnnotations.Schema;

namespace CoinMarket.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
}
