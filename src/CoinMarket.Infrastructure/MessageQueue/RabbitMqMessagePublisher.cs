using System.Text;
using CoinMarket.Application.Common.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CoinMarket.Infrastructure.MessageQueue;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly ConnectionFactory _connectionFactory;
    
    public RabbitMqMessagePublisher(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public void Publish<T>(string exchangeName, string routingKey,T message)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        channel.BasicPublish(exchangeName, routingKey, basicProperties: null, body);
    }
}