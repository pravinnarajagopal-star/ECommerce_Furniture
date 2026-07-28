using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    public class Order
    {

        [Key] public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal TotalAmount { get; set; }
        [Required][MaxLength(50)] public string OrderStatus { get; set; } = string.Empty; 
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)] 
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)] 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
