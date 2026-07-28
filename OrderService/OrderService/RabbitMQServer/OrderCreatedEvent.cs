namespace OrderService.RabbitMQServer
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal Amount { get; set; }

        public string Email { get; set; }
    }
}
