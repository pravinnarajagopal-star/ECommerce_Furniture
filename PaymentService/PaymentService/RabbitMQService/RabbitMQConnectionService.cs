using RabbitMQ.Client;

namespace PaymentService.RabbitMQServer
{
    public class RabbitMQConnectionService
    {
        private readonly IConfiguration _configuration;

        public RabbitMQConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IConnection> CreateConnectionAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                VirtualHost = _configuration["RabbitMQ:VirtualHost"]
            };


            return await factory.CreateConnectionAsync();
        }
    }
}
