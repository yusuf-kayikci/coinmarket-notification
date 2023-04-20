using System.Linq.Expressions;
using AutoMapper;
using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.Order.Interface;
using CoinMarket.Application.Order.Models;
using CoinMarket.Application.OrderNotification.Interface;
using CoinMarket.Domain.Entities;
using CoinMarket.Domain.Enums;
using CoinMarket.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Application.Order.Concrete;

public class BuyOrderService : IBuyOrderService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationPublishOperation _notificationPublishOperation;
    
    public BuyOrderService(IApplicationDbContext context, IMapper mapper, INotificationPublishOperation notificationPublishOperation)
    {
        _context = context;
        _mapper = mapper;
        _notificationPublishOperation = notificationPublishOperation;
    }

    public async Task<IEnumerable<BuyOrderDTO>> GetBuyOrdersAsync(Expression<Func<BuyOrder, bool>> predicate, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<BuyOrderDTO>>(await _context.BuyOrders.Where(predicate).ToListAsync(cancellationToken));
    }

    public async Task<BuyOrderDTO> GetBuyOrderAsync(Expression<Func<BuyOrder, bool>> expression, CancellationToken cancellationToken)
    {
        return _mapper.Map<BuyOrderDTO>(await _context.BuyOrders.FirstOrDefaultAsync(expression, cancellationToken));
    }

    public async Task<int> CreateBuyOrderAsync(CreateBuyOrderDTO buyOrderDto, CancellationToken cancellationToken = default)
    {
        var buyOrderEntity = _mapper.Map<BuyOrder>(buyOrderDto);
        var buyOrderNotificationEntity = _mapper.Map<IEnumerable<BuyOrderNotificationChannel>>(buyOrderDto.BuyOrderNotifications);
        buyOrderEntity.Status = BuyOrderStatus.Active;

        var result = await _context.BuyOrders.AddAsync(buyOrderEntity, cancellationToken);

        var buyOrderId = result.Entity.Id;
        if (buyOrderNotificationEntity?.Any() == true)
        {
            foreach (var bon in buyOrderNotificationEntity)
            {
                bon.BuyOrderId = buyOrderId;
            }
            
            await _context.BuyOrderNotificationChannels.AddRangeAsync(buyOrderNotificationEntity, cancellationToken);    
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        if (buyOrderNotificationEntity != null)
        {
            foreach (var nc in buyOrderNotificationEntity)
            {
                var message = new BuyOrderNotificationCreated
                {
                    BuyOrderId = nc.BuyOrderId, BuyOrderNotificationChannelId = nc.Id
                };
                
                await _notificationPublishOperation.PublishAsync(nc.BuyOrderNotificationType, message, cancellationToken);
            }
        }

        return buyOrderId;
    }

    public async Task<BuyOrderDTO> UpdateBuyOrderAsync(BuyOrderDTO buyOrder, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<BuyOrder>(buyOrder);
        
        var result = _context.BuyOrders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BuyOrderDTO>(result.Entity);
    }

    public async Task<IEnumerable<BuyOrderNotificationChannelDTO>> GetBuyOrderNotificationChannelsAsync(int buyOrderId, CancellationToken cancellationToken = default)
    {
        var buyOrderNotificationChannels = await _context.BuyOrderNotificationChannels.Where(x => x.BuyOrderId == buyOrderId).ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<BuyOrderNotificationChannelDTO>>(buyOrderNotificationChannels);
    }
}