using CoinMarket.Application.Order.Concrete;
using CoinMarket.Application.Order.Enum;
using CoinMarket.Application.Order.Interface;
using CoinMarket.Application.Order.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoinMarket.WebAPI.Controller;

[ApiController]
[Route("api/buyorders")]
public class BuyOrdersController : ControllerBase
{
    private readonly IBuyOrderService _buyOrderService;

    public BuyOrdersController(IBuyOrderService buyOrderService)
    {
        _buyOrderService = buyOrderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuyOrderDTO>>> GetBuyOrdersAsync(
        [FromQuery]int userId,
        [FromQuery]BuyOrderStatus status,
        [FromQuery]int day,
        [FromQuery]int amount,
        CancellationToken cancellationToken)
    {
        var expression = BuyOrderExpressionFunctionGenerator.GenerateExpression(userId, status, day, amount);
        var buyOrders = await _buyOrderService.GetBuyOrdersAsync(expression, cancellationToken);

        if (buyOrders == null)
        {
            return NotFound();
        }

        return Ok(buyOrders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BuyOrderDTO>> GetBuyOrderByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var buyOrder = await _buyOrderService.GetBuyOrderAsync(x => x.Id == id, cancellationToken);

        if (buyOrder is null)
        {
            return NotFound();
        }
        
        return Ok(buyOrder);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBuyOrderAsync([FromBody]CreateBuyOrderDTO buyOrder, CancellationToken cancellationToken = default)
    {
        var buyOrderId = await _buyOrderService.CreateBuyOrderAsync(buyOrder, cancellationToken);

        if (buyOrderId < 1)
        {
            return BadRequest();
        }
        
        return Created("/buyorders", buyOrderId);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchBuyOrderAsync(int id, [FromBody] JsonPatchDocument<BuyOrderDTO> document, CancellationToken cancellationToken = default)
    {
        var buyOrder = await _buyOrderService.GetBuyOrderAsync(x => x.Id == id, cancellationToken);

        if (buyOrder is null)
        {
            return NotFound();
        }
        
        document.ApplyTo(buyOrder);

        var updatedBuyOrder = await _buyOrderService.UpdateBuyOrderAsync(buyOrder, cancellationToken);
        
        return Ok(updatedBuyOrder);
    }

    [HttpGet("{id}/notifications")]
    public async Task<ActionResult<IEnumerable<BuyOrderNotificationChannelDTO>>> GetBuyOrderNotificationsAsync(int id, CancellationToken cancellationToken = default)
    {
        var notificationChannels = await _buyOrderService.GetBuyOrderNotificationChannelsAsync(id, cancellationToken);

        if (notificationChannels is null)
        {
            return NotFound();
        }

        return Ok(notificationChannels);
    }


}