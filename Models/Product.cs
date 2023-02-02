using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Product
    {
        public Product() 
        {
            Markets = new HashSet<Market>();
        }
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        [Required]
        public CurrencyCode Currency { get; set; }
        public int Quantity { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public ICollection<Market> Markets { get; set; }
    }
}
