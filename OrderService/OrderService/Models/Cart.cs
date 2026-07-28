using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Cart
    {
        [Key] public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)] public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
