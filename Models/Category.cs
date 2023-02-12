using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Category
    {

        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public int DayesToReminderBeforExpire { get; set; }
        public ICollection<Product> Products { get; set; }

        public Market Market { get; set; }
        public int MarketId { get; set; }
        public Seller Seller { get; set; }
        public string SellerId { get; set; }

    }
}
