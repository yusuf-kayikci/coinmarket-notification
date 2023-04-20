using CoinMarket.Application.Order.Models;
using FluentValidation;

namespace CoinMarket.Application.Order.Validations;

public class CreateBuyOrderValidator : AbstractValidator<CreateBuyOrderDTO>
{
    public CreateBuyOrderValidator()
    {
        RuleFor(bo => bo.Amount)
            .InclusiveBetween(100,20000)
            .WithMessage("Amount must be between 100 and 20000");

        RuleFor(bo => bo.Day)
            .InclusiveBetween(1,28)
            .WithMessage("Day must be between 1 and 28");
    }
}