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
            await _connectionService.CreateConnectionAsync();

        await using var channel =
            await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: "order.exchange",
            type: ExchangeType.Topic,
            durable: true);

        var json = JsonSerializer.Serialize(order);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "order.exchange",
            routingKey: "order.created",
            body: body);
    }

}
