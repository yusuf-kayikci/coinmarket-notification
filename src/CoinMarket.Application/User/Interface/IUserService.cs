using Microsoft.Extensions.DependencyInjection.User.Models;

namespace CoinMarket.Application.User.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken);
}