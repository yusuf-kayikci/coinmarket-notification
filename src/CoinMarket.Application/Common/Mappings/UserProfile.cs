using AutoMapper;
using Microsoft.Extensions.DependencyInjection.User.Models;

namespace CoinMarket.Application.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Entities.User, UserDTO>();
    }
}