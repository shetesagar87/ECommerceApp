using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "Customer";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Order>? Orders { get; set; }
    }
}
