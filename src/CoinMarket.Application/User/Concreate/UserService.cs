using AutoMapper;
using CoinMarket.Application.Common.Interfaces;
using CoinMarket.Application.User.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.User.Models;

namespace CoinMarket.Application.User.Concreate;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public UserService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }
}