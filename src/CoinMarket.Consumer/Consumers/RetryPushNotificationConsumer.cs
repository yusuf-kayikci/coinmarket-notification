using System.Text;
using CoinMarket.Consumer.EventHandlers.Interface;
using CoinMarket.Domain.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoinMarket.Consumer.Consumers;

public class RetryPushNotificationConsumer : BackgroundService
{
    private readonly IServiceProvider _sp;
    private readonly IModel _channel;
    private const string QueueName = "retry-buyorder-push-created";

    public RetryPushNotificationConsumer(ConnectionFactory connectionFactory, IServiceProvider sp)
    {
        _sp = sp;
        IConnection connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested(); 
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                using (var scope = _sp.CreateScope())
                {
                    var notificationEventHandler = _sp.GetRequiredService<INotificationEventHandler>();
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var buyOrderNotification = JsonConvert.DeserializeObject<BuyOrderNotificationCreated>(message);

                    if (buyOrderNotification == null)
                    {
                        return;
                    }

                    await notificationEventHandler.CreatedAsync(buyOrderNotification, stoppingToken);

                    _channel.BasicAck(ea.DeliveryTag, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _channel.BasicNack(ea.DeliveryTag,false,false);
            }
        };
        _channel.BasicConsume(queue: QueueName,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }
}