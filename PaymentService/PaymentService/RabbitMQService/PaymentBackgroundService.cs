
using PaymentService.DAL;
using PaymentService.Models;
using PaymentService.RabbitMQServer;
using PaymentService.RabbitMQService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;


public class PaymentBackgroundService : BackgroundService
{
    private readonly RabbitMQConnectionService _connectionService;
    private readonly ILogger<PaymentBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;


    public PaymentBackgroundService(
        RabbitMQConnectionService connectionService,
        ILogger<PaymentBackgroundService> logger, IServiceScopeFactory scopeFactory)
    {
        _connectionService = connectionService;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }


    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {

        var connection =
            await _connectionService.CreateConnectionAsync();


        var channel =
            await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync(
            queue: "order_created_queue",
            durable: true,
            exclusive: false,
            autoDelete: false
        );


        var consumer =
            new AsyncEventingBasicConsumer(channel);


        consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var body = args.Body.ToArray();

                var json =
                    Encoding.UTF8.GetString(body);


                var order =
                    JsonSerializer.Deserialize<OrderCreatedEvent>(json);


                if (order != null)
                {
                    await SendPaymentAsync(order);
                }


                // Message processed successfully
                await channel.BasicAckAsync(
                    args.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "payment processing failed");


                // Requeue message
                await channel.BasicNackAsync(
                    args.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        };


        await channel.BasicConsumeAsync(
            queue: "order_created_queue",
            autoAck: false,
            consumer: consumer
        );


        _logger.LogInformation(
            "Notification service started");


        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(
                1000,
                stoppingToken);
        }
    }



    private async Task SendPaymentAsync(
        OrderCreatedEvent order)
    {

        // Replace with Email/SMS/Push provider
        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var payment = new Payment
        {
            OrderId = order.OrderId,
            Amount = order.Amount,
            PaymentMethod = "COD",
            PaymentStatus = "Pending",
            PaymentDate = DateTime.Now,
            CreatedDate = DateTime.Now,
            TransactionId = "COD-"+order.OrderId,
            CreatedBy = "System"
        };

        dbContext.Payments.Add(payment);
        await dbContext.SaveChangesAsync();
        _logger.LogInformation(
            "Sending notification to {Email} for Order {OrderId}",
            order.Email,
            order.OrderId);


        await Task.Delay(500);

        _logger.LogInformation(
            "Notification sent successfully");
    }



    
}