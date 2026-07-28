using OrderService.RabbitMQServer;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class OrderPublisher
{
    private readonly RabbitMQConnectionService _connectionService;

    public OrderPublisher(RabbitMQConnectionService connectionService)
    {
        _connectionService = connectionService;
    }


    public async Task PublishAsync(OrderCreatedEvent order)
    {
        await using var connection =
             _connectionService.CreateConnection();

        await using var channel =
            await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync(
            queue: "order_created_queue",
            durable: true,
            exclusive: false,
            autoDelete: false
        );


        var message = JsonSerializer.Serialize(order);

        var body = Encoding.UTF8.GetBytes(message);


        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "order_created_queue",
            body: body
        );
    }
}