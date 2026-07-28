using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class CartItem
    {
        [Key] public int CartItemId { get; set; }
        public int CartId { get; set; } // ProductId belongs to Product Service (No FK)
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)] 
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)] 
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } 
        public Cart? Cart { get; set; }
    }
}
