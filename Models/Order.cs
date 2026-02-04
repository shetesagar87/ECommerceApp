using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // FK to User
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
