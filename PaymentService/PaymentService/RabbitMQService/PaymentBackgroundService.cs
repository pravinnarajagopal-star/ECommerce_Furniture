using Microsoft.EntityFrameworkCore;
using PaymentService.DAL;
using PaymentService.Models;
using PaymentService.RabbitMQServer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PaymentService.RabbitMQService
{
    public class PaymentBackgroundService : BackgroundService
    {
        private readonly RabbitMQConnectionService _connectionService;
        private readonly ILogger<PaymentBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private IConnection? _connection;
        private IChannel? _channel;

        public PaymentBackgroundService(
            RabbitMQConnectionService connectionService,
            ILogger<PaymentBackgroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _connectionService = connectionService;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _connection = await _connectionService.CreateConnectionAsync();
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "RabbitMQ not ready. Retrying in 5 seconds...");
                    await Task.Delay(5000, stoppingToken);
                }
            }

            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: "order.exchange",
                type: ExchangeType.Topic,
                durable: true);

            await _channel.QueueDeclareAsync(
                queue: "payment.queue",
                durable: true,
                exclusive: false,
                autoDelete: false);

            await _channel.QueueBindAsync(
                queue: "payment.queue",
                exchange: "order.exchange",
                routingKey: "order.created");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += ProcessMessageAsync;

            await _channel.BasicConsumeAsync(
                queue: "payment.queue",
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation("Payment Service Started...");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task ProcessMessageAsync(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());

                _logger.LogInformation("Received Message : {Message}", json);

                var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                if (order != null)
                {
                    await SavePaymentAsync(order);
                }

                await _channel!.BasicAckAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment");

                await _channel!.BasicNackAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        }

        private async Task SavePaymentAsync(OrderCreatedEvent order)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                PaymentMethod = "COD",
                PaymentStatus = "Pending",
                TransactionId = $"COD-{order.OrderId}",
                PaymentDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System"
            };

            context.Payments.Add(payment);

            await context.SaveChangesAsync();

            _logger.LogInformation(
                "Payment Saved Successfully. OrderId : {OrderId}",
                order.OrderId);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null)
                await _channel.DisposeAsync();

            if (_connection != null)
                await _connection.DisposeAsync();

            await base.StopAsync(cancellationToken);
        }
    }
}
