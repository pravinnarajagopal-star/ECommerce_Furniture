using NotificationService.RabbitMQServer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace NotificationService.RabbitMQService
{
    public class NotificationWorker : BackgroundService
    {
        private readonly RabbitMQConnectionService _connectionService;
        private readonly ILogger<NotificationWorker> _logger;
        private readonly IConfiguration _configuration;

        private IConnection? _connection;
        private IChannel? _channel;

        public NotificationWorker(
            RabbitMQConnectionService connectionService,
            ILogger<NotificationWorker> logger,
            IConfiguration configuration)
        {
            _connectionService = connectionService;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _connection = await _connectionService.CreateConnectionAsync();

            _channel = await _connection.CreateChannelAsync();

            // Create Exchange
            await _channel.ExchangeDeclareAsync(
                exchange: "order.exchange",
                type: ExchangeType.Topic,
                durable: true);

            // Create Queue
            await _channel.QueueDeclareAsync(
                queue: "notification.queue",
                durable: true,
                exclusive: false,
                autoDelete: false);

            // Bind Queue
            await _channel.QueueBindAsync(
                queue: "notification.queue",
                exchange: "order.exchange",
                routingKey: "order.created");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += ProcessMessageAsync;

            await _channel.BasicConsumeAsync(
                queue: "notification.queue",
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation("Notification Service Started");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task ProcessMessageAsync(
            object sender,
            BasicDeliverEventArgs args)
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());

                _logger.LogInformation("Received : {Message}", json);

                var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                if (order != null)
                {
                    await SendNotificationAsync(order);
                }

                await _channel!.BasicAckAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Notification Failed");

                await _channel!.BasicNackAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        }

        private async Task SendNotificationAsync(OrderCreatedEvent order)
        {
            await SendEmail(order);

            _logger.LogInformation(
                "Email sent successfully to {Email}",
                order.Email);
        }

        private async Task SendEmail(OrderCreatedEvent order)
        {
            using var smtp = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]!),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _configuration["Smtp:Username"],
                    _configuration["Smtp:Password"])
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:Username"]!),
                Subject = "Order Confirmation",
                Body = $@"
                    <h2>Order Placed Successfully</h2>

                    <p>Hello <b>{order.CustomerName}</b>,</p>

                    <p>Your order has been placed successfully.</p>

                    <table border='1' cellpadding='5'>
                        <tr>
                            <td><b>Order Id</b></td>
                            <td>{order.OrderId}</td>
                        </tr>

                        <tr>
                            <td><b>Product</b></td>
                            <td>{order.CustomerName}</td>
                        </tr>

                        <tr>
                            <td><b>Amount</b></td>
                            <td>{order.Amount}</td>
                        </tr>
                    </table>

                    <br/>

                    <p>Thank you for shopping with us.</p>",
                IsBodyHtml = true
            };

            mail.To.Add(order.Email);

            await smtp.SendMailAsync(mail);
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
