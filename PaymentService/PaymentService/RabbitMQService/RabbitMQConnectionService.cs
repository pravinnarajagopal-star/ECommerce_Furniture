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
            var host = _configuration["RabbitMQ:HostName"];
            var port = _configuration["RabbitMQ:Port"];
            var user = _configuration["RabbitMQ:UserName"];
            var password = _configuration["RabbitMQ:Password"];
            var vhost = _configuration["RabbitMQ:VirtualHost"];

            Console.WriteLine($"RabbitMQ Host: {host}");
            Console.WriteLine($"RabbitMQ Port: {port}");
            Console.WriteLine($"RabbitMQ User: {user}");
            Console.WriteLine($"RabbitMQ VHost: {vhost}");

            var factory = new ConnectionFactory
            {
                HostName = host,
                Port = int.Parse(port!),
                UserName = user,
                Password = password,
                VirtualHost = vhost
            };

            return await factory.CreateConnectionAsync();
        }
    }
}
