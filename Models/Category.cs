using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
        public byte[]? Image { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
