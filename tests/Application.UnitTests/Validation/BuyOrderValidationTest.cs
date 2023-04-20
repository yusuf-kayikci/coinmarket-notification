using CoinMarket.Application.Order.Models;
using CoinMarket.Application.Order.Validations;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace CoinMarket.Application.UnitTests.Validation
{
    [TestFixture]
    public class BuyOrderValidatorTests
    {
        [Test]
        public void BuyOrderAmount_Must_Between_100and20000()
        {
            var validator = new CreateBuyOrderValidator();
            var model = new CreateBuyOrderDTO
            {
                Amount = 50, Day = 2, UserId = 1, BuyOrderNotifications = new List<BuyOrderNotificationChannelDTO>()
            };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Amount);
        }

        [Test]
        public void BuyOrderDay_Must_Between_1and28()
        {
            var validator = new CreateBuyOrderValidator();
            var model = new CreateBuyOrderDTO
            {
                Amount = 200, Day = 30, UserId = 1, BuyOrderNotifications = new List<BuyOrderNotificationChannelDTO>()
            };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Day);
        }
    }
}