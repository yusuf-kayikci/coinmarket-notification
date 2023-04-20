using AutoMapper;
using CoinMarket.Application.Order.Models;
using CoinMarket.Domain.Entities;
using CoinMarket.Domain.Enums;

namespace CoinMarket.Application.Common.Mappings;

public class BuyOrderProfile : Profile
{
    public BuyOrderProfile()
    {
        CreateMap<BuyOrderNotificationType, Order.Enum.BuyOrderNotificationType>().ConvertUsing((src, dest) => src switch {
            BuyOrderNotificationType.Mail => Order.Enum.BuyOrderNotificationType.Mail,
            BuyOrderNotificationType.Sms => Order.Enum.BuyOrderNotificationType.Sms,
            BuyOrderNotificationType.Push => Order.Enum.BuyOrderNotificationType.Push,
            _ => throw new ArgumentException("")
        });
        
        CreateMap<Order.Enum.BuyOrderNotificationType, BuyOrderNotificationType>().ConvertUsing((src, dest) => src switch {
            Order.Enum.BuyOrderNotificationType.Mail => BuyOrderNotificationType.Mail ,
            Order.Enum.BuyOrderNotificationType.Sms => BuyOrderNotificationType.Sms ,
            Order.Enum.BuyOrderNotificationType.Push => BuyOrderNotificationType.Push,
            _ => throw new ArgumentException("")
        });
        
        CreateMap<BuyOrderNotificationChannelDTO, BuyOrderNotificationChannel>()
            .ForMember(d => d.BuyOrderNotificationType, s => s.MapFrom(z => z.NotificationType));

        CreateMap<BuyOrderNotificationChannel, BuyOrderNotificationChannelDTO>()
            .ForMember(d => d.NotificationType,s => s.MapFrom(z => z.BuyOrderNotificationType));
        
        CreateMap<CreateBuyOrderDTO, BuyOrder>();

        CreateMap<BuyOrder, BuyOrderDTO>()
            .ForMember(d => d.Status, s => s.MapFrom(z => z.Status));

        CreateMap<BuyOrderDTO, BuyOrder>()
            .ForMember(d => d.Status, s => s.MapFrom(z => z.Status));
    }
}