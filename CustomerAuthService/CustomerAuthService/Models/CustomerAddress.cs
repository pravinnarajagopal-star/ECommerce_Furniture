namespace CustomerAuthService.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        public Guid AddressId { get; set; }

        public Guid CustomerId { get; set; }

        public byte AddressType { get; set; }

        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public required string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public Customer Customer { get; set; }
    }
}
