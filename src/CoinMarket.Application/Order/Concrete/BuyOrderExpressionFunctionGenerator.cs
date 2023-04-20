using System.Linq.Expressions;
using CoinMarket.Application.Order.Enum;
using CoinMarket.Domain.Entities;
using LinqKit;

namespace CoinMarket.Application.Order.Concrete;

public static class BuyOrderExpressionFunctionGenerator
{
    public static Expression<Func<BuyOrder, bool>> GenerateExpression(
        int userId,
        BuyOrderStatus status,
        int day,
        int amount)
    {
        var predicate = PredicateBuilder.New<BuyOrder>(false);

        if (userId > 0)
        {
            predicate.And(x => x.UserId == userId);
        }
        else if (day > 0)
        {
            predicate.And(x => x.Day == day);
        }
        else if (amount > 0)
        {
            predicate.And(x => x.Amount == amount);
        }
        else if (status != 0)
        {
            predicate.And(x => x.Status == (Domain.Enums.BuyOrderStatus)status);
        }

        return predicate;
    }
}