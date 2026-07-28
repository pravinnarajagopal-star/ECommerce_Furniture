namespace CustomerAuthService.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string MobileNumber { get; set; }

        public required string PasswordHash { get; set; }

        public int RoleId { get; set; } = 2;
        

        public string? Address { get; set; }

        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }
     

        //public ICollection<CustomerAddress> CustomerAddresses { get; set; }
        //= new List<CustomerAddress>();
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = "";
    }
}
