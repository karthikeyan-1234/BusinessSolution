using CommonLibrary.Models.DTOs;
using CommonLibrary.Models;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace InventoryAPI.Services
{
    public class InventoryBackGroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private string? exchange;
        private string? queue;
        private string? routingKey;
        private string? hostName;
        private int port;

        public InventoryBackGroundService(IConfiguration configuration,IServiceProvider _serviceProvider)
        {
            this._serviceProvider = _serviceProvider;
            exchange = configuration.GetSection("RabbitMQ").GetSection("Exchange").Value;
            queue = configuration.GetSection("RabbitMQ").GetSection("Queue").Value;
            routingKey = configuration.GetSection("RabbitMQ").GetSection("RoutingKey").Value;
            hostName = configuration.GetSection("RabbitMQ").GetSection("HostName").Value;
            port = Convert.ToInt16(configuration.GetSection("RabbitMQ").GetSection("Port").Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = port };

            Console.WriteLine($"Inventory Service Running outside Connection");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);
                var result = channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
                var queue_name = result.QueueName;
                channel.QueueBind(queue: queue_name, exchange: exchange, routingKey: routingKey);

                Console.WriteLine($"Inventory Service Running within Connection");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received +=  async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    System.Console.WriteLine($"Received message in Inventory Service");

                    
                    var purchDet = JsonSerializer.Deserialize<PurchaseDetailDTO>(message); 
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();
                        if (purchDet is not null)
                        {
                            await inventoryService.AddNewInventory(new Inventory { item_id = purchDet.ItemId.ToString(), last_edit_at = DateTime.Now, qty = (float)purchDet?.Qty });
                        }

                    }

                };

                channel.BasicConsume(queue: queue_name, autoAck: true, consumer: consumer);
                await Task.Delay(Timeout.Infinite, stoppingToken);

            }

        }
    }
}
