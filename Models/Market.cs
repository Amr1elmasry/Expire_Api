using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Market
    {

        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Seller Seller { get; set; }
        public string SellerId { get; set; }
    }
}
