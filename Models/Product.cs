using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BarCode { get; set; }
        public double Price { get; set; }

        [Required]
        public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.EGP;
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpireData { get; set; }

        public int DayesToReminderBeforExpire { get; set; }
        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Market Market { get; set; }
        [Required]
        public int MarketId { get; set; }

        public Seller Seller { get; set; }
        [Required]
        public string SellerId { get; set; }
    }
}
