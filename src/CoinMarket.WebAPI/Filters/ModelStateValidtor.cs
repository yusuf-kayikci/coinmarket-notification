using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoinMarket.WebAPI.Filters;

public class ModelStateValidtor : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new ValidationException("Invalid model state");
        }

        base.OnActionExecuting(context);
    }
}