using RabbitMQ.Client;

namespace CoinMarket.Infrastructure.MessageQueue;

public class QueueInitializerService
{
    private readonly ConnectionFactory _connectionFactory;
    
    public QueueInitializerService(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public void Initialize()
    {
        var mailrouting = "mail";
        var pushrouting = "push";
        var smsrouting = "sms";
        var directrouting = "direct";

        var buyordermailcreatedqueue = "buyorder-mail-created";
        var retrybuyordermailcreatedqueue = "retry-buyorder-mail-created";

        var buyordersmscreatedqueue = "buyorder-sms-created";
        var retrybuyordersmscreatedqueue = "retry-buyorder-sms-created";

        var buyorderpushcreatedqueue = "buyorder-push-created";
        var retrybuyorderpushcreatedqueue = "retry-buyorder-push-created";

        var buyordernotificationcreatedexchange = "buyorder-notification-created";
        var retrybuyordernotificationcreatedexchange = "retry-buyorder-notification-created";

        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(buyordernotificationcreatedexchange,
            directrouting,
            durable: false,
            autoDelete: false,
            arguments: null);

        channel.ExchangeDeclare(retrybuyordernotificationcreatedexchange,
            directrouting,
            durable: false,
            autoDelete: false,
            arguments: null);

        IDictionary<string, object> smsArgs = new Dictionary<string, object>();
        smsArgs.Add("x-dead-letter-exchange", retrybuyordernotificationcreatedexchange);
        smsArgs.Add("x-dead-letter-routing-key", smsrouting);

        IDictionary<string, object> mailArgs = new Dictionary<string, object>();
        mailArgs.Add("x-dead-letter-exchange", retrybuyordernotificationcreatedexchange);
        mailArgs.Add("x-dead-letter-routing-key", mailrouting);

        IDictionary<string, object> pushArgs = new Dictionary<string, object>();
        pushArgs.Add("x-dead-letter-exchange", retrybuyordernotificationcreatedexchange);
        pushArgs.Add("x-dead-letter-routing-key", pushrouting);


        IDictionary<string, object> retryArgs = new Dictionary<string, object>();
        retryArgs.Add("x-message-ttl", 10000);

        channel.QueueDeclare(buyordersmscreatedqueue, false, false, false, smsArgs);
        channel.QueueDeclare(buyordermailcreatedqueue, false, false, false, mailArgs);
        channel.QueueDeclare(buyorderpushcreatedqueue, false, false, false, pushArgs);

        channel.QueueBind(buyordermailcreatedqueue, buyordernotificationcreatedexchange, mailrouting, null);
        channel.QueueBind(buyordersmscreatedqueue, buyordernotificationcreatedexchange, smsrouting, null);
        channel.QueueBind(buyorderpushcreatedqueue, buyordernotificationcreatedexchange, pushrouting, null);
        
        channel.QueueDeclare(retrybuyordersmscreatedqueue, false, false, false, retryArgs);
        channel.QueueDeclare(retrybuyordermailcreatedqueue, false, false, false, retryArgs);
        channel.QueueDeclare(retrybuyorderpushcreatedqueue, false, false, false, retryArgs);

        channel.QueueBind(retrybuyordermailcreatedqueue, retrybuyordernotificationcreatedexchange, mailrouting, null);
        channel.QueueBind(retrybuyordersmscreatedqueue, retrybuyordernotificationcreatedexchange, smsrouting, null);
        channel.QueueBind(retrybuyorderpushcreatedqueue, retrybuyordernotificationcreatedexchange, pushrouting, null);
    }
}