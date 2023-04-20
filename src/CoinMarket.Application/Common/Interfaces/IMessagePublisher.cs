namespace CoinMarket.Application.Common.Interfaces;

public interface IMessagePublisher
{
    void Publish<T>(string exchnageName, string routingKey, T message);
}