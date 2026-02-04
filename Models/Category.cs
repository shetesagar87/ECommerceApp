using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Product>? Products { get; set; }
    }
}
