using MediatR;
using RabbitMQ.Client;
using PurchaseAPI.CQRS.Notifications;
using System.Text;

namespace PurchaseAPI.CQRS.Handlers
{
    public class PurchaseDetailNotificationHandler : INotificationHandler<PurchaseDetailAddedNotification>
    {
        private string? exchange;
        private string? queue;
        private string? routingKey;
        private string? hostName;
        private int port;
        readonly IConfiguration configuration;

        public PurchaseDetailNotificationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
            exchange =  configuration.GetSection("RabbitMQ").GetSection("Exchange").Value;
            queue = configuration.GetSection("RabbitMQ").GetSection("Queue").Value;
            routingKey = configuration.GetSection("RabbitMQ").GetSection("RoutingKey").Value;
            hostName = configuration.GetSection("RabbitMQ").GetSection("HostName").Value;
            port = Convert.ToInt16(configuration.GetSection("RabbitMQ").GetSection("Port").Value);
        }

        public Task Handle(PurchaseDetailAddedNotification notification, CancellationToken cancellationToken)
        {

            var msg = notification.Message == null ? "" : notification.Message;

            var factory = new ConnectionFactory() { HostName = hostName, Port = port };
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);
                var result = channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
                var queue_name = result.QueueName;

                channel.QueueBind(queue: queue_name,
                  exchange: exchange,
                  routingKey: routingKey);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                var body1 = Encoding.UTF8.GetBytes(msg);

                Console.WriteLine("Purchase Detail Added Notification for RMQ with msg : " + notification.Message);

                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: properties, body: body1);
            }

            return Task.CompletedTask;
        }
    }
}
