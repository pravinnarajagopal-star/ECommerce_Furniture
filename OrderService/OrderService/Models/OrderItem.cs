using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderService.Models
{
    public class OrderItem
    {
        [Key] public int OrderItemId { get; set; }
        public int OrderId { get; set; } // ProductId belongs to Product Service (No FK)
                                          public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column("UnitPrice",TypeName = "decimal(18,2)")]
       
        public decimal Price { get; set; } 
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)] 
        public string CreatedBy { get; set; } = string.Empty; 
        [MaxLength(100)] 
        public string? UpdatedBy { get; set; } 
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string ProductName { get; set; } = string.Empty;

        [NotMapped]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
